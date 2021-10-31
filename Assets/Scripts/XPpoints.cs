﻿using UnityEngine;

public class XPpoints : MonoBehaviour
{
    private UIManager uI;
    public static int XP = 0;

    private void Start()
    {
        uI = ObjectsHolder.Instance.uIManager;
        Player.SetPlayerXP (XP);
    }

    public void AddPoints( float xp, float xPos )
    {
        float koef = (Constants.PATH_START_X - xPos) / Constants.PATH_LENGHT;
        float addXP = (1 - koef) * xp / 5;
        XP += (int)xp + (int)addXP;
        Player.SetPlayerXP (XP);
        print ("XP - " + (int)xp + "  Bonus - " + (int)addXP);
    }
}
