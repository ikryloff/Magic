using System.Collections.Generic;
using UnityEngine;

public class UnitsOnBoard : MonoBehaviour
{
    private List <TowerUnit> [] _lineTowersList;
    private List <Human> [] _lineHumansLists;

    private void OnEnable()
    {
        GameEvents.current.OnTowerWasBuilt += AddTowerToLineTowersList;
        GameEvents.current.OnHumanPositionWasChanged += AddHumanToLineHumansList;
        GameEvents.current.OnHumanDeath += RemoveHumanFromLineHumansList;
        InitLists ();
    }

    private void OnDisable()
    {
        GameEvents.current.OnTowerWasBuilt -= AddTowerToLineTowersList;
        GameEvents.current.OnHumanPositionWasChanged -= AddHumanToLineHumansList;
        GameEvents.current.OnHumanDeath -= RemoveHumanFromLineHumansList;
    }

    private void InitLists()
    {
        Debug.Log ("Init BoardUnits Lists");
        _lineTowersList = new List<TowerUnit> [9]; // 0 and 8 are unused
        for ( int i = 0; i < _lineTowersList.Length; i++ )
        {
            _lineTowersList [i] = new List<TowerUnit> ();
        }
        _lineHumansLists = new List<Human> [9]; // 0 and 8 are unused
        for ( int i = 0; i < _lineHumansLists.Length; i++ )
        {
            _lineHumansLists [i] = new List<Human> ();
        }
        GameEvents.current.FieldIsBuiltAction ();

    }

    

    public  List<TowerUnit> GetLineTowersList(int lineNumber) 
    {
        return _lineTowersList [lineNumber];
    }

    public  List<Human> GetLineHumansList( int lineNumber )
    {
        return _lineHumansLists [lineNumber];
    }

    public  void AddTowerToLineTowersList (TowerUnit tower, Cell cell )
    {
        _lineTowersList [cell.GetLinePosition()].Add (tower);
        Debug.Log ("Tower " + tower.GetUnitName() + " Added to list " + cell.GetLinePosition ());
    }

    public  void RemoveTowerFromLineTowersList( TowerUnit tower )
    {
        _lineTowersList [tower.GetLinePosition ()].Remove (tower);
    }

    public  void AddHumanToLineHumansList( Human human, Cell cell )
    {
        if ( _lineHumansLists [cell.GetLinePosition ()].Contains (human) )
            return;
        _lineHumansLists [cell.GetLinePosition ()].Add (human);
        Debug.Log ("Human " + human.name + " Added to list " + cell.GetLinePosition ());
    }

    public void RemoveHumanFromLineHumansList( Human human )
    {
        _lineHumansLists [human.GetLinePosition ()].Remove (human);
        Debug.Log ("Human " + human.name + " Removed from list " + human.GetLinePosition ());
    }

    public Human GetRandomHumanToAttack( Cell cell )
    {
        int line = cell.GetLinePosition ();
        if ( _lineHumansLists [line].Count == 0 )
        {
            return null;
        }
        else
            return _lineHumansLists [line] [Random.Range (0, _lineHumansLists [line].Count)];
    }

    public Human GetNearestHumanToAttack( Cell cell )
    {
        int line = cell.GetLinePosition ();

        if ( _lineHumansLists [line].Count == 0 )
        {
            return null;
        }
        else
        {
            int min = int.MaxValue;
            Human nearest = null;
            for ( int i = 0; i < _lineHumansLists [line].Count; i++ )
            {
                Human human = _lineHumansLists [line] [i];
                if ( human.GetColumnPosition () < min )
                {
                    nearest = human;
                    min = nearest.GetColumnPosition ();
                }
            }
            return nearest;
        }

    }

    public List <Human> GetAllHumansInLineToAttack( Cell cell )
    {
        int line = cell.GetLinePosition ();
        if ( _lineHumansLists [line].Count == 0 )
        {
            return null;
        }
        else
            return _lineHumansLists [line];

    }

    public List<Human> GetAllHumansToAttack( )
    {
        List<Human> allHumans = new List<Human> ();

        for ( int i = 0; i < _lineHumansLists.Length; i++ )
        {
            for ( int j = 0; j < _lineHumansLists[i].Count; j++ )
            {
                if(_lineHumansLists[i].Count != 0 )
                {
                    allHumans.Add (_lineHumansLists [i] [j]);
                }                    
            }
        }
        Debug.Log (allHumans.Count);
        return allHumans;


    }

    public TowerUnit GetTowerUnitFromCell( Cell cell )
    {
        int line = cell.GetLinePosition ();
        if ( _lineTowersList [line].Count == 0 )
        {
            return null;
        }
        else
        {
            for ( int i = 0; i < _lineTowersList[line].Count; i++ )
            {
                TowerUnit towerUnit = _lineTowersList [line][i];
                if ( towerUnit.GetColumnPosition () == cell.GetColumnPosition () )
                    return towerUnit;
            }
        }
        return null;
    }
}
