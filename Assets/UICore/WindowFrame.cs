using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WindowFrame : MonoBehaviour
{
    [SerializeField] protected WindowType _windowType;
    public WindowType Type => _windowType;
}

public class Screen : WindowFrame
{

}

public class Dialog : WindowFrame
{

}