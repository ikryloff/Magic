using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    protected Button _button;
    protected UIManager _uiManager;

    public virtual void Action() { }

    protected void Awake()
    {
        _uiManager = FindObjectOfType<UIManager> ();
        _button = GetComponent<Button> ();
        _button.onClick.AddListener (Action);
    }
}
