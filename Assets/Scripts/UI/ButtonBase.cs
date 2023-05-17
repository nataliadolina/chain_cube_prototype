using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class ButtonBase : MonoBehaviour
{
    [Inject]
    protected TouchController touchController;

    protected Button button;

    [Inject]
    private void Construct()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        AwakeInternal();
    }

    protected virtual void AwakeInternal() { }

    private void OnClick()
    {
        touchController.IsLastMouseOperationDown = false;
        OnClickInternal();
    }
    protected abstract void OnClickInternal();
}
