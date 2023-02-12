using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LayersController : Singleton<LayersController>
{
    public static event System.Action OnSceneLoaded = () => { };
    public WindowFrame CurrentScreen { get; private set; }
    public WindowFrame CurrentDialog { get; private set; }

    private Dictionary<string, Layer> _loadedLayers = new Dictionary<string, Layer>();
    private Layer _fader;

    public void BlockInput(bool isBlocked)
    {
        foreach (var layer in _loadedLayers.Values)
        {
            layer.BlockInput(isBlocked);
        }
    }

    public void RegisterLayer(Layer layer)
    {
        if (_loadedLayers.TryGetValue(layer.Name, out var existingScene))
            Debug.LogError($"Scene {layer.Name} already exist. Multiple scenes is not supported");
        else
            _loadedLayers.Add(layer.Name, layer);
    }

    public void UnregisterLayer(Layer layer)
    {
        if (!_loadedLayers.TryGetValue(layer.Name, out var existingScene))
            Debug.LogError($"Scene {layer.Name} does not exist and can't be removed.");
        else
            _loadedLayers.Remove(layer.Name);
    }

    public Layer GetLayer(string layer)
    {
        if (_loadedLayers.TryGetValue(layer, out var existingScene))
            return existingScene;
        Debug.LogError($"Scene {layer} does not exist.");
        return null;
    }

    public WindowFrame Show<T>() where T : Component
    {
        BlockInput(true);
        var prefab = Resources.Load("Windows/" + typeof(T).Name);
        var screenGO = Instantiate(prefab) as GameObject;
        screenGO.SetActive(false);
        var screen = screenGO.GetComponent<T>() as WindowFrame;
        switch (screen.Type)
        {
            case WindowType.Screen:
                StartCoroutine(DoShow(screenGO, CurrentScreen?.gameObject));
                CurrentScreen = screen;
                break;
            case WindowType.Dialog:
                CurrentDialog = screen;
                StartCoroutine(DoShowDialog(screenGO));
                break;
            default:
                throw new System.NotImplementedException();
        }
        
        return screen;
    }

    public void Hide<T>() where T : Component
    {
        BlockInput(true);
        StartCoroutine(DoHide());
    }

    private IEnumerator DoHide()
    {
        var fader = GetLayer(WindowMode.ModalWindow);
        var sceneController = GetLayer(WindowMode.ModalWindow);
        sceneController.RemoveAllScreens();
        yield return new WaitForSeconds(fader.FadeOut());
        StartCoroutine(UnloadSceneAsync(WindowMode.ModalWindow));
        BlockInput(false);
    }

    private IEnumerator DoShow(GameObject screen, GameObject oldScreen)
    {
        StartCoroutine(LoadFader());
        yield return new WaitUntil(()=>_fader != null);
        yield return new WaitForSeconds(_fader.FadeIn());
        StartCoroutine(UnloadSceneAsync(WindowMode.ModalWindow));
        yield return StartCoroutine(LoadSceneAsync(WindowMode.MainWindow, screen));
        if (oldScreen != null)
            Destroy(oldScreen.gameObject);
        _fader.FadeOut();
        BlockInput(false);
    }

    private IEnumerator DoShowDialog(GameObject screen)
    {
        yield return StartCoroutine(LoadSceneAsync(WindowMode.ModalWindow, screen));
        var fader = GetLayer(WindowMode.ModalWindow);
        yield return new WaitForSeconds(fader.FadeIn());
        BlockInput(false);
    }

    private IEnumerator LoadSceneAsync(string sceneName, GameObject screen = null)
    {
        Scene scene;
        if (Exists())
        {
            AttachScreen();
            yield break;
        }
        else
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            if (Exists())
            { 
                AttachScreen();
            }
        }

        bool Exists()
        {
            scene = SceneManager.GetSceneByName(sceneName);
            return scene.IsValid() && scene.isLoaded;
        }

        void AttachScreen()
        {
            //SceneManager.SetActiveScene(scene);
            if(screen != null)
                GetLayer(sceneName).AttachScreen(screen);
            OnSceneLoaded();
        }
    }

    private IEnumerator LoadFader()
    {
        if (_fader != null)
            yield break;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(WindowMode.Fader, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        _fader = GetLayer(WindowMode.Fader);
    }

    private IEnumerator UnloadSceneAsync(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).IsValid())
            yield break;
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        var scene = SceneManager.GetSceneByName(WindowMode.MainWindow);
        if (scene.IsValid() && scene.isLoaded)
        {
            //SceneManager.SetActiveScene(scene);
        }
        else
            Debug.LogError($"scene {scene.name} is not valid");
    }

    internal void LoadEquipment()
    {
        StartCoroutine(LoadSceneAsync(WindowMode.Equipment));
    }

    internal void UnloadEquipment()
    {
        StartCoroutine(UnloadSceneAsync(WindowMode.Equipment));
    }

    //scenes as ui layers. should match scene name
    public static class WindowMode
    {
        public static readonly string MainWindow = "MainLayer";
        public static readonly string ModalWindow = "ModalLayer"; //will block main window
        public static readonly string Fader = "Fader";
        public static readonly string Equipment = "Equipment";
    }

}
    public enum WindowType
    {
        Screen,
        Dialog
    }
