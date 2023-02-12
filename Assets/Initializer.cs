using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public void Start()
    {
        GameObject transitionManager = new GameObject("TransitionManager");
        transitionManager.AddComponent<TransitionsController>();
        GameObject layersController = new GameObject("LayersController");
        layersController.AddComponent<LayersController>();

        TransitionsController.Instance.GoTo_Menu();
        Destroy(gameObject);
    }
}
