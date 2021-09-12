using System.Collections;
using UnityEngine;

public class Cell : MonoBehaviour
{

    private SpriteRenderer _cellSprite;
    private CellPos _cellPos;
    private Vector2 _position;
    public bool IsLoaded;
    public bool IsUsed;
    private bool _isEngaged;
    [SerializeField]
    private int cellType; // 0 - common, 1 - untouchable 
    private SpellCaster _spellCaster;
    public Sprite spellSprite;
    public Sprite cellSprite;
    public Sprite colorSprite;
    public Sprite untouchableSprite;
    private TowerUnit _engagingTower;
    const int _cellNameNumberOfChars = 5;

   
    public void Init( CellPos cellPos )
    {
        _cellSprite = GetComponent<SpriteRenderer> ();
        _spellCaster = FindObjectOfType<SpellCaster> ();
        GameEvents.current.OnTowerWasBuilt += SetEngagedByTower;
        GameEvents.current.OnCastOver += CountCell;
        GameEvents.current.OnCastResetAction += ReloadCell;
        spellSprite = ObjectsHolder.Instance.spellSprite;
        cellSprite = ObjectsHolder.Instance.cellSprite;
        colorSprite = ObjectsHolder.Instance.colorSprite;
        untouchableSprite = ObjectsHolder.Instance.untouchableSprite;
        SetPos (cellPos);
        
    }


    private void OnDisable()
    {
        GameEvents.current.OnCastOver -= CountCell;
        GameEvents.current.OnCastResetAction -= ReloadCell;
        GameEvents.current.OnTowerWasBuilt -= SetEngagedByTower;
    }
   

    private void SetPos( CellPos cellPos )
    {
        _cellPos = cellPos;
    }

    public void SetEngagedByTower( TowerUnit tower, Cell cell )
    {
        if ( cell == this )
        {
            _isEngaged = true;
            _engagingTower = tower;
        }
    }

    public void SetUnusable()
    {
        _isEngaged = true;
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
        if ( _cellSprite )
        {
            if ( cellType == 1 )
                _cellSprite.sprite = untouchableSprite;
            else
                _cellSprite.sprite = cellSprite;

            _cellSprite.sortingOrder = 0;
        }
        IsLoaded = false;
    }

    public void LoadCell()
    {
        if ( _cellSprite )
        {
            _cellSprite.sprite = spellSprite;
            _cellSprite.sortingOrder = 20;
        }
        IsLoaded = true;
        _spellCaster.CastLine.Add (this);
    }

    public void ColorCellToPrepare()
    {
        if ( _cellSprite )
        {
            _cellSprite.sprite = colorSprite;
            _cellSprite.sortingOrder = 0;
        }
    }

    public int GetLinePosition()
    {
        return _cellPos.Line;
    }

    public int GetColumnPosition()
    {
        return _cellPos.Col;
    }



}
