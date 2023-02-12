using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class TransitionsController : Singleton<TransitionsController>
{
    [NonSerialized] public BackButton BackButton;
    [NonSerialized] public TopBar TopBar;

    private Transition _transition;
    private Stack<Transition> _dialogs = new Stack<Transition>();

    public void Start()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = true; //hack for editor eating up all cpu while in idle and lost focus

        LayersController.OnSceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        LayersController.OnSceneLoaded -= OnSceneLoaded;
    }

    
    private void Go(Transition transition)
    {
        var window = transition.Enter();
        if (window is Screen)
            _transition = transition;
        else
            _dialogs.Push(transition);
    }

    private void OnSceneLoaded()
    {
        //Debug.Log("OnSceneLoaded");
        //Debug.Log($"{LayersController.Instance.CurrentScreen.name} window type {LayersController.Instance.CurrentScreen.Type}");

        if (LayersController.Instance.CurrentScreen.Type == WindowType.Screen)
        {
            if (LayersController.Instance.CurrentScreen is Menu)
                BackButton.Hide();
            else
                BackButton.Show();

            if (LayersController.Instance.CurrentScreen is Equipment)
                TopBar.Hide();
            else
                TopBar.Show();
        }
    }

    private void DoGoBack()
    {
        if (_dialogs.Count > 0)
        {
            var dialog = _dialogs.Pop();
            dialog.Exit();
            return;
        }
        _transition.Exit();
    }

    #region Commands

    public void GoTo_Menu() => Go(new ToMenu());
    public void GoTo_Settings() => Go(new ToSettings());
    public void GoTo_Greetings() => Go(new ToGreetings());
    public void GoTo_Shop() => Go(new ToShop());
    public void GoTo_Shop_FromEquipment() => Go(new ToShop_FromEquipment());
    public void GoTo_Equipment() => Go(new ToEquipment());
    public void GoBack() => DoGoBack();

    #endregion
}
