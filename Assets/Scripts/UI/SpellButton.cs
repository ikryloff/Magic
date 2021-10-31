using UnityEngine;

public class SpellButton : UIButton
{
    [SerializeField]
    private UIManager _uIManager;

    public override void Action()
    {
        _uIManager.OpenSpellsPanel ();
    }

}
