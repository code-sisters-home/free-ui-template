using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToShop : Transition
{
    public override WindowFrame Enter()
    {
        base.Enter();
        return LayersController.Instance.Show<Shop>();
    }

    public override void Exit()
    {
        base.Exit();
        TransitionsController.Instance.GoTo_Menu();
    }
}
