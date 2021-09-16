using System.Collections.Generic;
using UnityEngine;

public class UnitsOnBoard : MonoBehaviour
{
    public static List <TowerUnit> [] LineTowersList;
    public static List <Human> [] LineHumansLists;

    private void OnEnable()
    {
        GameEvents.current.OnTowerWasBuilt += AddTowerToLineTowersList;
        GameEvents.current.OnHumanPositionWasChanged += AddHumanToLineHumansList;
        GameEvents.current.OnHumanDeathAction += RemoveHumanFromLineHumansList;
        InitLists ();
    }

    private void OnDisable()
    {
        GameEvents.current.OnTowerWasBuilt -= AddTowerToLineTowersList;
        GameEvents.current.OnHumanPositionWasChanged -= AddHumanToLineHumansList;
        GameEvents.current.OnHumanDeathAction -= RemoveHumanFromLineHumansList;
    }

    private void InitLists()
    {
        Debug.Log ("Init BoardUnits Lists");
        LineTowersList = new List<TowerUnit> [9]; // 0 and 8 are unused
        for ( int i = 0; i < LineTowersList.Length; i++ )
        {
            LineTowersList [i] = new List<TowerUnit> ();
        }
        LineHumansLists = new List<Human> [9]; // 0 and 8 are unused
        for ( int i = 0; i < LineHumansLists.Length; i++ )
        {
            LineHumansLists [i] = new List<Human> ();
        }
        GameEvents.current.BoardIsBuiltEvent ();

    }

    

    public  List<TowerUnit> GetLineTowersList(int lineNumber) 
    {
        return LineTowersList [lineNumber];
    }

    public  List<Human> GetLineHumansList( int lineNumber )
    {
        return LineHumansLists [lineNumber];
    }

    public  void AddTowerToLineTowersList (TowerUnit tower, Cell cell )
    {
        LineTowersList [cell.GetLinePosition()].Add (tower);
        Debug.Log ("Tower " + tower.GetUnitName() + " Added to list " + cell.GetLinePosition ());
    }

    public  void RemoveTowerFromLineTowersList( TowerUnit tower )
    {
        LineTowersList [tower.GetLinePosition ()].Remove (tower);
    }

    public  void AddHumanToLineHumansList( Human human, Cell cell )
    {
        if ( LineHumansLists [cell.GetLinePosition ()].Contains (human) )
            return;
        LineHumansLists [cell.GetLinePosition ()].Add (human);
        Debug.Log ("Human " + human.name + " Added to list " + cell.GetLinePosition ());
    }

    public void RemoveHumanFromLineHumansList( Human human )
    {
        LineHumansLists [human.GetLinePosition ()].Remove (human);
        Debug.Log ("Human " + human.name + " Removed from list " + human.GetLinePosition ());
    }

    public Human GetRandomHumanToAttack( Cell cell )
    {
        int line = cell.GetLinePosition ();
        if ( LineHumansLists [line].Count == 0 )
        {
            return null;
        }
        else
            return LineHumansLists [line] [Random.Range (0, LineHumansLists [line].Count)];
    }

    public Human GetNearestHumanToAttack( Cell cell )
    {
        int line = cell.GetLinePosition ();

        if ( LineHumansLists [line].Count == 0 )
        {
            return null;
        }
        else
        {
            int min = int.MaxValue;
            Human nearest = null;
            for ( int i = 0; i < LineHumansLists [line].Count; i++ )
            {
                Human human = LineHumansLists [line] [i];
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
        if ( LineHumansLists [line].Count == 0 )
        {
            return null;
        }
        else
            return LineHumansLists [line];

    }

    public List<Human> GetAllHumansToAttack( )
    {
        List<Human> allHumans = new List<Human> ();

        for ( int i = 0; i < LineHumansLists.Length; i++ )
        {
            for ( int j = 0; j < LineHumansLists[i].Count; j++ )
            {
                if(LineHumansLists[i].Count != 0 )
                {
                    allHumans.Add (LineHumansLists [i] [j]);
                }                    
            }
        }
        Debug.Log (allHumans.Count);
        return allHumans;


    }

    public TowerUnit GetTowerUnitFromCell( Cell cell )
    {
        int line = cell.GetLinePosition ();
        if ( LineTowersList [line].Count == 0 )
        {
            return null;
        }
        else
        {
            for ( int i = 0; i < LineTowersList[line].Count; i++ )
            {
                TowerUnit towerUnit = LineTowersList [line][i];
                if ( towerUnit.GetColumnPosition () == cell.GetColumnPosition () )
                    return towerUnit;
            }
        }
        return null;
    }
}
