using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Human> humans;
    public Line [] lines;
    public Transform [] spawns;
    SpawnPoints [] sp;
    

    public static float displace = 0f;

    private void Awake()
    {
        lines = FindObjectsOfType<Line> ();
        sp = FindObjectsOfType<SpawnPoints> ();
        SortLines (lines);
                
    }

   

    

    private void SortLines( Line [] _lines )
    {
        Line [] ls = new Line [9];
        for ( int i = 0; i < _lines.Length; i++ )
        {
            ls [_lines [i].GetLineNumber()] = _lines [i];
        }
        lines = ls;
    }
    /*
    public void AddTowerToEnemyList( TowerTemp tower )
    {
        lines [tower.LinePosition].lineTowers.Add (tower);
        GameEvents.current.TowerAppear ();
    }

    public void AddTowerToEnemyList( TrapTemp trap )
    {
        lines [trap.LinePosition].lineTraps.Add (trap);
    }

    public void RemoveTowerFromEnemyList( TowerTemp tower )
    {
        lines [tower.LinePosition].lineTowers.Remove (tower);
        GameEvents.current.TowerAppear ();
    }

    public void RemoveTowerFromEnemyList( TrapTemp trap )
    {
        lines [trap.LinePosition].lineTraps.Remove (trap);
    }

    public void AddCreepToEnemyList( Creep creep )
    {
        lines [creep.GetLinePosition ()].lineCreeps.Add (creep);
        GameEvents.current.EnemyAppear ();
    }

    public void RemoveCreepFromEnemyList( Creep creep )
    {
        lines [creep.GetLinePosition ()].lineCreeps.Remove (creep);
        GameEvents.current.EnemyAppear ();
    }

    public TowerTemp GetTargetTower( Creep creep )
    {
        List<TowerTemp> twrs = lines [creep.GetLinePosition ()].lineTowers;
        TowerTemp target = GetMainTargetTower (creep);
        float temp = float.MaxValue;
        for ( int i = 0; i < twrs.Count; i++ )
        {
            float dist = Mathf.Abs (twrs [i].towerTransform.position.x - creep.creepTransform.position.x);
            if ( dist <= temp )
            {
                target = twrs [i];
                temp = dist;
            }
        }        
        return target;
    }

    public TowerTemp GetMainTargetTower( Creep creep )
    {        
        return sortedTowers [creep.GetLinePosition ()];
    }


  
    public Creep GetClosestCreep( TowerTemp tower )
    {
        List<Creep> creeps = lines [tower.LinePosition].lineCreeps;
        Creep target = null;
        float temp = float.MaxValue;
        for ( int i = 0; i < creeps.Count; i++ )
        {
            float dist = Mathf.Abs (tower.towerTransform.position.x - creeps [i].creepTransform.position.x);
            if ( dist <= temp )
            {
                target = creeps [i];
                temp = dist;
            }
        }
        return target;
    }
      */
    public static float GetSpriteDisplace()
    {
        if ( displace > 0.9999f )
            displace = 0.0001f;
        displace += 0.0001f;        
        return displace;
    }
}
