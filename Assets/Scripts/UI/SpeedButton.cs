public class SpeedButton : UIButton
{
    public override void Action()
    {
        GameEvents.current.GameStateChangedAction (GameManager.GameState.FastGame);
    }
}
