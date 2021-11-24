using UnityEngine;

public class AppQuitButton : UIButton
{
    public override void Action()
    {
        Application.Quit ();
    }
}
