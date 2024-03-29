﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public static int CellsCount = 0;
    public List<Cell> CastLine;
    public SpellDecoder spellDecoder;
    // private UIManager ui;

    private void Awake()
    {
        CastLine = new List<Cell> ();
        spellDecoder = FindObjectOfType<SpellDecoder> ();
    }

    private void OnEnable()
    {
        GameEvents.current.OnStopCastingEvent += StopCasting;
    }

    private void OnDisable()
    {
        GameEvents.current.OnStopCastingEvent -= StopCasting;
    }

    public void CastSpell()
    {
        spellDecoder.MakeSpell (CastLine);
    }

    public void MakeCast()
    {
        StartCoroutine (MakeCastWithDelay ());
    }

    IEnumerator MakeCastWithDelay()
    {
        yield return new WaitForSeconds (.1f);
        CastSpell ();
        ColorizeSpell (CastLine);
    }

    private void ColorizeSpell( List<Cell> castLine )
    {
        if ( castLine == null )
            return;
        for ( int i = 0; i < castLine.Count; i++ )
        {
            castLine [i].ColorCellToPrepare ();
        }
    }

    private void StopCasting()
    {
        CastLine.Clear ();
        CellsCount = 0;
        GameEvents.current.CastResetAction ();
        GameEvents.current.PrepareTimeValueChangedAction (1f);
    }
}
