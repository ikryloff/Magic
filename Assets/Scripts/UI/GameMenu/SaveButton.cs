using System;
using UnityEngine;


public class SaveButton : UIButton
{
    [SerializeField] private GameManager manager;


    public override void Action()
    {
        manager.SaveGame ();
    }
}
