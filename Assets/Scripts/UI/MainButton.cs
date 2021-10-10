public class MainButton : UIButton
{
    
    private MainButtonState _state;


    private void Start()
    {
        CloseSpellsPanel ();
    }

    public override void Action()
    {
        switch ( _state )
        {
            case MainButtonState.PanelOpener:
                OpenSpellsPanel ();
                break;
            case MainButtonState.PanelCloser:
                CloseSpellsPanel ();
                break;
        }

    }

    private void OpenSpellsPanel()
    {
        _uiManager.OpenSpellsPanel ();
        _state = MainButtonState.PanelCloser;
    }

    private void CloseSpellsPanel()
    {
        _uiManager.CloseSpellsPanel ();
        _state = MainButtonState.PanelOpener;
    }

    public enum MainButtonState
    {
        PanelOpener,
        PanelCloser
    }
}
