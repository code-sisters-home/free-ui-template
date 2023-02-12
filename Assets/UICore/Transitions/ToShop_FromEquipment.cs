using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToShop_FromEquipment : Transition
{
    public override WindowFrame Enter()
    {
        base.Enter();
        LayersController.Instance.UnloadEquipment();
        return LayersController.Instance.Show<Shop>();
    }

    public override void Exit()
    {
        base.Exit();
        TransitionsController.Instance.GoTo_Equipment();
        //TransitionsController.Instance.GoTo_Menu();
    }
}
