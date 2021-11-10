using UnityEngine;

public class ReturnToSpellsPanelButton : UIButton
{
    [SerializeField]
    private UIManager _uIManager;

    public override void Action()
    {
        _uIManager.OpenSpellsPanel ();
    }
}
