using UnityEngine;

public class ExitWzardViewButton : UIButton
{
    [SerializeField]
    private UIManager _uIManager;

    public override void Action()
    {
        _uIManager.CloseWizardView ();
    }
}
