using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition
{
    public virtual WindowFrame Enter()
    {
        //Debug.Log("Show screen");
        //LayersController Show()
        return null;
    }

    public virtual void Exit()
    {
        //Debug.Log("Go back");
        //TransitionsController Goto()
    }
}
