using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToGreetings : Transition
{
    public override WindowFrame Enter()
    {
        base.Enter();
        return LayersController.Instance.Show<DialogBox>();
    }

    public override void Exit()
    {
        base.Exit();
        LayersController.Instance.Hide<DialogBox>();
    }
}
