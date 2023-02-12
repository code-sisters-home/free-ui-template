using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    private void Awake()
    {
        TransitionsController.Instance.BackButton = this;
    }

    public void GoBack()
    {
        TransitionsController.Instance.GoBack();
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
}
