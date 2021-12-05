public class ResetSkillsButton : UIButton
{

    public override void Action()
    {
        LevelBook.ResetSkillLevelsAndPoints ();
    }
}
