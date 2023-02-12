using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void Click()
    {
        Debug.Log("Close");
        TransitionsController.Instance.GoBack();
    }
}
