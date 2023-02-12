using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Screen
{
    public void GoTo_Shop()
    {
        TransitionsController.Instance.GoTo_Shop_FromEquipment();
    }
}
