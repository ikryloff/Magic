﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeller : MonoBehaviour
{
    
    public Wizard wizard;
    public Human targetEnemy;
    public List<Transform> targetsTransforms;

    private SpawnPoints _spawnPoints;
    private EnemyController ec;
    private SpellCaster castManager;
    private UIManager ui;

    private SpellShotFabric _shotFabric;

    private void Awake()
    {
        _spawnPoints = FindObjectOfType<SpawnPoints> ();
        _shotFabric = GetComponent<SpellShotFabric> ();
    }

    private void Start()
    {
        wizard = ObjectsHolder.Instance.wizard;
        ui = ObjectsHolder.Instance.uIManager;
        ec = ObjectsHolder.Instance.enemyController;
        castManager = ObjectsHolder.Instance.castManager;
    }

    public void MakeSpelling( UnitTemplate spellTemplate, Cell [] cells )
    {
        _shotFabric.Init (spellTemplate, cells);

        if ( !_shotFabric.IsValidSpellCall (spellTemplate, cells) ) //is enough mana and is any tagret 
            return;
        StartCoroutine (PrepareSpellRoutine (spellTemplate, _shotFabric));
        GameEvents.current.NewGameMessage (spellTemplate.unitName);
    }


    IEnumerator PrepareSpellRoutine( UnitTemplate spellTemplate, SpellShotFabric shotFabric)
    {
        Wizard.IsStopCasting = true;
        //ui.SetPrepareIcon (spell.entityID);
        float time = spellTemplate.prepareTime;
        float perc = Time.deltaTime / time * 100;
        float value = 100; ;
        while ( time > 0 )
        {
            ui.SetPrepareValue (value);
            time -= Time.deltaTime;
            value -= perc;
            yield return null;
        }
        ui.SetPrepareValue (0);
        shotFabric.CreateSpellShot (spellTemplate);
        GameEvents.current.StopCastingAction ();
    }

    
    

}