using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Layer : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private BackButton _backButton;
    [SerializeField] private Animator _fader;

    private List<GameObject> _screens = new List<GameObject>();
    private GraphicRaycaster _graphicRaycaster;

    public string Name => gameObject.scene.name;

    private void Awake()
    {
        LayersController.Instance.RegisterLayer(this);
        _graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();
    }

    private void OnDestroy()
    {
        LayersController.Instance.UnregisterLayer(this);
    }

    public void AttachScreen(GameObject screen)
    {
        screen.transform.SetParent(_canvas.transform, false);
        screen.SetActive(true);
        _screens.Add(screen);
    }

    public void RemoveAllScreens()
    {
        foreach (var screen in _screens)
        {
            Destroy(screen);
        }
        _screens.Clear();
    }

    public float FadeOut()
    {
        _fader.Play("FadeOut");
        return _fader.GetClipLength("FadeOut");
    }

    public float FadeIn()
    {
        _fader.Play("FadeIn");
        return _fader.GetClipLength("FadeIn");
    }

    public void BlockInput(bool isBlocked)
    {
        _graphicRaycaster.enabled = !isBlocked;
    }
}
