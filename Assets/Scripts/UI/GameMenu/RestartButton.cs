using UnityEngine;

public class RestartButton : UIButton
{
    [SerializeField] private GameManager manager;

    public override void Action()
    {
        manager.ReloadGame ();
    }
}
