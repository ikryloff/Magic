using UnityEngine;

[RequireComponent (typeof (UCEffects))]
[RequireComponent (typeof (UCBoardUnitMoving))]
public class BoardUnit : Unit
{
    private int _linePosition;
    private int _columnPosition;
    private string _direction;
    private string _name;
    private UnitTemplate _unitTemplate;
    private UCUnitHealth _health;
    private UCTargetFinder _targetFinder;
    private UCEffects _effects;
    private UCWeapon _weapon;
    private UCBoardUnitMoving _moving;
    private UCUnitAnimation _animation;

    protected Cell _cell;

    #region 

   
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
        return _columnPosition;
    }

    public void SetColumnPosition( int columnPosition )
    {
        _columnPosition = columnPosition;
    }

    public string GetUnitName()
    {
        return _name;
    }

    public UCUnitHealth GetUnitHealth()
    {
        return _health;
    }

    public UnitTemplate GetUnitTemplate()
    {
        return _unitTemplate;
    }

    public Cell GetCurrentCell() { return _cell; }

    #endregion

  
    protected void Init( UnitTemplate unitTemplate )
    {
        _unitTemplate = unitTemplate;
        _effects = GetComponent<UCEffects> ();
        _effects.Init (unitTemplate);
        _moving = GetComponent<UCBoardUnitMoving> ();
        _moving.Init (this);
        _health = GetComponentInChildren<UCUnitHealth> ();
        _health?.Init (this, _effects);
        _targetFinder = GetComponent<UCTargetFinder>();
        _targetFinder?.Init (this);
        _weapon = GetComponent<UCWeapon> ();
        _weapon?.Init (this);
        _unitType = unitTemplate.unitType;
        _animation = GetComponent<UCUnitAnimation> ();
        _animation?.Init (this);
    }

}
