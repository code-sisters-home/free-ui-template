using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : Screen
{
    public void GoToShop()
    {
        TransitionsController.Instance.GoTo_Shop();
    }

    public void GoToEquipment()
    {
        TransitionsController.Instance.GoTo_Equipment();
    }

    public void GoToSettings()
    {
        TransitionsController.Instance.GoTo_Settings();
    }
}
