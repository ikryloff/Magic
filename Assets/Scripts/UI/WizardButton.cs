using UnityEngine;

public class WizardButton : UIButton
{
    [SerializeField]
    private UIManager _uIManager;

    public override void Action()
    {
        _uIManager.OpenWizardView ();
    }


}
