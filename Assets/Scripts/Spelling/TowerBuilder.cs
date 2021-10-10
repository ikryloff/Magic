using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public Wizard wizard;
   // private UIManager ui;
    private UnitsOnBoard _boardUnits;
    private List<Cell> _defCells;
    [SerializeField]
    private UnitTemplate _defTowerTemplate;

    private void Awake()
    {
        Init ();
    }

    private void Init()
    {
        _boardUnits = FindObjectOfType<UnitsOnBoard> ();
        _defCells = new List<Cell> ();
        //ui = FindObjectOfType<UIManager> ();
        wizard = FindObjectOfType<Wizard> ();
        print ("Init Defence TBuilder");
    }

    private void GetDeffCells( List<Cell> cells )
    {
        _defCells = cells;
    }

    public void BuildTower( UnitTemplate spellTemplate, Cell [] cells )
    {
        if ( !IsValidTowerCall (spellTemplate, cells) ) //is enough mana and place is not engaged 
        {
            GameEvents.current.GameStateChangedAction (GameManager.GameState.BoardActive);
            return;
        }
        StartCoroutine (PrepareBuildingRoutine (spellTemplate, cells));
    }

    private bool IsValidTowerCall( UnitTemplate spellTemplate, Cell [] cells )
    {
        for ( int i = 0; i < cells.Length; i++ )
        {
            if ( cells [i].IsEngaged () )
            {
                GameEvents.current.NewGameMessage ("Can`t do that here!");
                return false;
            }
        }

        if ( wizard != null && spellTemplate.cost > wizard.GetManapoints () )
        {
            GameEvents.current.NewGameMessage ("You have no mana!");
            return false;
        }
        return true;
    }

    IEnumerator PrepareBuildingRoutine( UnitTemplate spellTemplate, Cell [] cells )
    {
        //ui.SetPrepareIcon (spellTemplate);
        //wizard.ManaWaste (spellTemplate.cost);
        GameEvents.current.NewGameMessage (spellTemplate.unitName);
        float time = spellTemplate.prepareTime;
        float perc = Time.deltaTime / time * 100;
        float value = 100;
        while ( time > 0 )
        {
            //ui.SetPrepareValue (value);
            time -= Time.deltaTime;
            value -= perc;
            yield return null;
        }
        //ui.SetPrepareValue (0);
        Building (spellTemplate, cells);
    }

    private void Building( UnitTemplate unitTemplate, Cell [] cells )
    {
        for ( int i = 0; i < cells.Length; i++ )
        {
            GameObject newTowerGO = Instantiate (unitTemplate.unitPrefab, cells [i].transform.position, Quaternion.identity);
            TowerUnit newTower = newTowerGO.GetComponent<TowerUnit> ();
            cells [i].SetEngagedByTower (newTower);
            newTower.Activate (unitTemplate, cells [i]);
            UnitsOnBoard.AddTowerToLineTowersList (newTower, cells [i].GetLinePosition ());
            GameEvents.current.TowerWasBuiltAction (newTower, cells [i]);
        }
        GameEvents.current.GameStateChangedAction (GameManager.GameState.BoardActive);
    }


    public void BuildDefTower( List<Cell> cells )
    {
        for ( int i = 0; i < cells.Count; i++ )
        {
            GameObject newTowerGO = Instantiate (_defTowerTemplate.unitPrefab, cells [i].transform.position, Quaternion.identity);
            TowerUnit newTower = newTowerGO.GetComponent<TowerUnit> ();
            newTower.Activate (_defTowerTemplate, cells [i]);
            UnitsOnBoard.AddTowerToLineTowersList (newTower, cells [i].GetLinePosition ());
            cells [i].SetEngagedByTower (newTower);
            GameEvents.current.TowerWasBuiltAction (newTower, cells [i]);
        }

        Debug.Log ("BuildDefTower");

    }
}


