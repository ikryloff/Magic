using System.Collections;
using UnityEngine;

public class Cell : MonoBehaviour
{

    private SpriteRenderer _cellSprite;
    private CellPos _cellPos;
    private Vector2 _position;
    private bool _isLoaded;
    private bool _isPrepared;
    private bool _isEngaged;
    private bool _isGameEnder;
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
        GameEvents.current.OnCastOver += CountCell;  // from touch controller when user finished draw the cast
        GameEvents.current.OnCastResetEvent += ReloadCell; // from spellcaster when we need to reset spellcasting
        GameEvents.current.OnTimeToColorCellsEvent += ColorCellByType; // from Board to color cells by type
        
        SetCellPos (cellPos);
        
    }


    private void OnDisable()
    {
        GameEvents.current.OnCastOver -= CountCell;
        GameEvents.current.OnCastResetEvent -= ReloadCell;
        GameEvents.current.OnTimeToColorCellsEvent -= ColorCellByType; 
    }
   

    private void SetCellPos( CellPos cellPos )
    {
        _cellPos = cellPos;
    }

    public void SetEngagedByTower( TowerUnit tower )
    {
        _isEngaged = true;
        _engagingTower = tower;
        cellType = 2;
        ColorCellByType ();
    }
    public void SetFreefromTower()
    {
        _isEngaged = false;
        _engagingTower = null;
        cellType = 0;
        ColorCellByType ();
    }
    public void SetUnusable()
    {
        _isEngaged = true;
        cellType = 1;
    }

    public void SetEnder()
    {
        _isGameEnder = true;
    }

    public bool IsEngaged()
    {
        return _isEngaged;
    }

    public bool IsLoaded()
    {
        return _isLoaded;
    }

    private void CountCell()
    {
        if ( _isLoaded ) SpellCaster.CellsCount += 1;
    }

    public void ReloadCell()
    {
        if ( !_isLoaded && !_isPrepared )
            return;
        _isLoaded = false;
        _isPrepared = false;
        ColorCellByType ();
    }

    public void LoadCell()
    {
        if ( _cellSprite )
        {
            _cellSprite.sprite = spellSprite;
            _cellSprite.sortingOrder = 0; ;
        }
        _isLoaded = true;
        _spellCaster.CastLine.Add (this);
    }

    public void ColorCellToPrepare()
    {
        if ( _cellSprite )
        {
            _isPrepared = true;
            _cellSprite.sprite = colorSprite;
            _cellSprite.sortingOrder = 0;
        }
    }

    public void ColorCellByType()
    {
        if ( _isLoaded || _isPrepared )
            return;
        if ( _cellSprite )
        {
            if ( cellType == 1 )
                _cellSprite.sprite = untouchableSprite;
            if ( cellType == 0 )
                _cellSprite.sprite = cellSprite;
            if ( cellType == 2 )
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

    public bool IsGameEnder()
    {
        return _isGameEnder;
    }

}
