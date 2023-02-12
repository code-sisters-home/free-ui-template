using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBar : MonoBehaviour
{
    private void Awake()
    {
        TransitionsController.Instance.TopBar = this;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void GoToGreetings()
    {
        TransitionsController.Instance.GoTo_Greetings();
    }
}
