using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : UIButton
{
    [SerializeField]
    private UIManager _uIManager;

    public override void Action()
    {
        _uIManager.OpenMenu ();
    }
}
