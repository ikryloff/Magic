using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToGameButton : UIButton
{
    [SerializeField]
    private UIManager _uIManager;

    public override void Action()
    {
        _uIManager.CloseMenu ();
    }
}
