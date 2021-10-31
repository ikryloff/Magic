using UnityEngine;

public class ExitButton : UIButton
{
    [SerializeField]
    private UIManager _uIManager;

    public override void Action()
    {
        _uIManager.CloseSpellsPanel ();
    }
}
