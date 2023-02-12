using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSettings : Transition
{
    public override WindowFrame Enter()
    {
        base.Enter();
        return LayersController.Instance.Show<Settings>();
    }

    public override void Exit()
    {
        base.Exit();
        LayersController.Instance.Hide<Settings>();
    }
}
