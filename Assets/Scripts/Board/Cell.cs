using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    
    public SpriteRenderer CellRenderer;
    private int _linePosition;
    private int _columnNumber;
    public bool IsLoaded;
    public bool IsUsed;
    private bool _isEngaged;
    [SerializeField]
    private int cellType; // 0 - common, 1 - untouchable 
    private SpellCaster castManager;
    public Sprite spellSprite;
    public Sprite cellSprite;
    public Sprite colorSprite;
    public Sprite untouchableSprite;
    private TowerUnit _engagingTower;
    const int _cellNameNumberOfChars = 5;

    private void Awake()
    {
        CellRenderer = GetComponent<SpriteRenderer> ();
        castManager = FindObjectOfType<SpellCaster> ();        
    }

    public void CellInit()
    {
        GetColumnNumber ();
        GameEvents.current.OnTowerWasBuilt += SetEngagedByTower;
    }

    private void GetColumnNumber()
    {
        _columnNumber = int.Parse( name.Substring (_cellNameNumberOfChars) );
    }

    private void OnDisable()
    {
        GameEvents.current.OnCastOver -= CountCell;
        GameEvents.current.OnCastReset -= ReloadCell;
        GameEvents.current.OnTowerWasBuilt -= SetEngagedByTower;        
    }
    void Start()
    {
        GameEvents.current.OnCastOver += CountCell;
        GameEvents.current.OnCastReset += ReloadCell;
        spellSprite = ObjectsHolder.Instance.spellSprite;
        cellSprite = ObjectsHolder.Instance.cellSprite;
        colorSprite = ObjectsHolder.Instance.colorSprite;
        untouchableSprite = ObjectsHolder.Instance.untouchableSprite;

        Wizard.IsStopCasting = true;
        StartCoroutine (CastDelay ());

    }

    public void SetEngagedByTower(TowerUnit tower, Cell cell)
    {
        if(cell == this)
        {
            _isEngaged = true;
            _engagingTower = tower;
        }
    }

    public void SetFreefromTower()
    {
        _isEngaged = false;
        _engagingTower = null;
    }

    public bool GetEngaged()
    {
        return _isEngaged;
    }

    private void CountCell()
    {
        if ( IsLoaded ) SpellCaster.CellsCount += 1;
    }

    public void ReloadCell()
    {
        if ( CellRenderer)
        {
            if(cellType == 1)
                CellRenderer.sprite = untouchableSprite;
            else
                CellRenderer.sprite = cellSprite;

            CellRenderer.sortingOrder = 0;
        }
        IsLoaded = false;
    }

    public void LoadCell()
    {
        if ( Wizard.IsStopCasting )
            return;
        if ( CellRenderer )
        {
            CellRenderer.sprite = spellSprite;
            CellRenderer.sortingOrder = 20;
        }
        IsLoaded = true;
        castManager.CastLine.Add (this);
    }

    public void ColorCell()
    {
        if ( CellRenderer )
        {
            CellRenderer.sprite = colorSprite;
            CellRenderer.sortingOrder = 0;
        }
    }

    IEnumerator CastDelay()
    {
        yield return new WaitForSeconds (0.5f);
        Wizard.IsStopCasting = false;
    }

    public int GetLinePosition()
    {
        return _linePosition;
    }

    public void SetLinePosition( int linePosition )
    {
        _linePosition = linePosition;
    }

    public int GetColumnPosition()
    {
        return _columnNumber;
    }

    public void SetColumnPosition( int columnNumber )
    {
        _columnNumber = columnNumber;
    }

}
