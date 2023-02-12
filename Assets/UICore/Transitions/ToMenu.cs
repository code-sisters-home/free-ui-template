using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMenu : Transition
{
    public override WindowFrame Enter()
    {
        base.Enter();
        return LayersController.Instance.Show<Menu>();        
    }

    public override void Exit()
    {
        base.Exit();
        //show AppQuit dialog
    }
}
