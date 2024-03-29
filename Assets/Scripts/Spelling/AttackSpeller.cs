﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeller : MonoBehaviour
{
    public static int count = 0;
    public Human targetEnemy;
    public List<Transform> targetsTransforms;

    private SpawnPoints _spawnPoints;

    private SpellShotFabric _shotFabric;

    private void Awake()
    {
        _spawnPoints = FindObjectOfType<SpawnPoints> ();
        _shotFabric = GetComponent<SpellShotFabric> ();
    }


    public void MakeSpelling( UnitTemplate spellTemplate, Cell [] cells )
    {
        _shotFabric.Init (spellTemplate, cells);

        if ( !_shotFabric.IsValidSpellCall (spellTemplate, cells) ) //is enough mana and is any tagret 
        {
            GameEvents.current.GameStateChangedAction (GameManager.GameState.BoardActive);
            return;
        }
        StartCoroutine (PrepareSpellRoutine (spellTemplate, _shotFabric));
    }


    IEnumerator PrepareSpellRoutine( UnitTemplate spellTemplate, SpellShotFabric shotFabric)
    {
        GameEvents.current.ManaWasteAction (spellTemplate.cost);
        
        float time = spellTemplate.prepareTime;
        float perc = Time.deltaTime / time;
        float value = 1; ;
        while ( time > 0 )
        {
            GameEvents.current.PrepareTimeValueChangedAction (value);
            time -= Time.deltaTime;
            value -= perc;
            yield return null;
        }
        GameEvents.current.PrepareTimeValueChangedAction (100);
        shotFabric.CreateSpellShot (spellTemplate);
        GameEvents.current.GameStateChangedAction (GameManager.GameState.BoardActive);
    }

    
    

}
