using UnityEngine;
using UnityEngine.UI;

public abstract class UIButton : MonoBehaviour
{
    protected Button _button;

    public abstract void Action();

    protected void Awake()
    {
        _button = GetComponent<Button> ();
        _button.onClick.AddListener (Action);
    }
}
