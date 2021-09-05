using System.Collections;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public Wizard wizard;
    private UIManager ui;
    private UnitsOnBoard _boardUnits;
    [SerializeField]
    private Cell [] _defCells;
    [SerializeField]
    private UnitTemplate _defTowerTemplate;



    private void Awake()
    {
        _boardUnits = FindObjectOfType<UnitsOnBoard> ();
    }

    private void Start()
    {
        InitDefenceTowerBuilding ();
    }

    private void InitDefenceTowerBuilding()
    {
        ui = FindObjectOfType<UIManager> ();
        wizard = FindObjectOfType<Wizard> ();
        print ("Init Defence TBuilder");
        BuildTower (_defTowerTemplate, _defCells);
    }



    public void BuildTower( UnitTemplate spellTemplate, Cell [] cells )
    {
        if ( !IsValidTowerCall (spellTemplate, cells) ) //is enough mana and place is not engaged 
            return;
        StartCoroutine (PrepareBuildingRoutine (spellTemplate, cells));
    }

    private bool IsValidTowerCall( UnitTemplate spellTemplate, Cell [] cells )
    {
        for ( int i = 0; i < cells.Length; i++ )
        {
            if ( cells [i].GetEngaged() )
            {
                GameEvents.current.NewGameMessage ("Can`t do that here!");
                GameEvents.current.StopCastingAction ();
                return false;
            }
        }

        if ( wizard != null && spellTemplate.cost > wizard.GetManapoints () )
        {
            GameEvents.current.NewGameMessage ("You have no mana!");
            GameEvents.current.StopCastingAction (); ;
            return false;
        }
        return true;
    }

    IEnumerator PrepareBuildingRoutine( UnitTemplate spellTemplate, Cell [] cells )
    {
        Wizard.IsStopCasting = true;
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
            GameObject newTowerGO = Instantiate (unitTemplate.towerPrefab, cells[i].transform.position, Quaternion.identity);
            SetSpritePosition (newTowerGO, cells[i]);
            TowerUnit newTower = newTowerGO.GetComponent<TowerUnit> ();
            newTower.towerCost = unitTemplate.cost / unitTemplate.targetIndexes.Length;
            newTower.Activate (unitTemplate, cells [i]);
            GameEvents.current.TowerWasBuilt (newTower, cells [i]);
        }
        GameEvents.current.StopCastingAction ();
    }

    private void SetSpritePosition( GameObject go, Cell cell )
    {
        // to avoid texture flickering
        SpriteRenderer sr = go.GetComponent<SpriteRenderer> ();
        sr.sortingOrder = cell.GetLinePosition ();
        go.transform.position += new Vector3 (0, 0, EnemyController.GetSpriteDisplace ());
    }
}


