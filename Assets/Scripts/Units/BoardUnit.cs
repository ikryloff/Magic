using UnityEngine;

[RequireComponent (typeof (UCEffects))]
public class BoardUnit : Unit
{
    private int _linePosition;
    private int _columnPosition;
    private string _direction;
    private string _name;
    private UnitTemplate _unitTemplate;
    private UCUnitAnimation _unitAnimation;
    private UnitHealth _health;
    private UCTargetFinder _targetFinder;
    private UCEffects _effects;
    private UCWeapon _weapon;

    protected Cell _cell;

    protected UnitStateIdle _unitStateIdle;
    protected UnitStateHold _unitStateHold;
    protected UnitStateAttack _unitStateAttack;
    protected UnitStateHit _unitStateHit;
    protected UnitStateDie _unitStateDie;
    

    #region 

    public void SetDirection( string dir )
    {
        _direction = dir;
    }

    public string GetDirection()
    {
        return _direction;
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

    public UnitHealth GetUnitHealth()
    {
        return _health;
    }

    public UnitTemplate GetUnitTemplate()
    {
        return _unitTemplate;
    }

    public Cell GetCurrentCell() { return _cell; }


    public void SetHoldState()
    {
        ChangeState (_unitStateHold);
    }

    public virtual void IdleBehavior() { }

    #endregion

    
    public void SetIdleState()
    {
        ChangeState (_unitStateIdle);
    }

    public void SetHitState()
    {
        if(GetCurrentState() == _unitStateIdle)
            ChangeState (_unitStateHit);
    }

    public void SetDieState()
    {
        ChangeState (_unitStateDie);
    }

    public void SetAttackState()
    {
        ChangeState (_unitStateAttack);
    }

    

    public void SetStartDirection( UnitTemplate template )
    {
        if ( template.unitType == UnitType.Human )
            SetDirection (Constants.UNIT_LEFT_DIR);
        else
            SetDirection (Constants.UNIT_RIGHT_DIR);
    }

    protected void Init( UnitTemplate unitTemplate )
    {
        _unitTemplate = unitTemplate;
        _effects = GetComponent<UCEffects> ();
        _effects.Init (unitTemplate);
        _health = GetComponentInChildren<UnitHealth> ();
        _health?.Init (this, _effects);
        _targetFinder = GetComponent<UCTargetFinder>();
        _targetFinder?.Init (this);
        _weapon = GetComponent<UCWeapon> ();
        _weapon?.Init (this);
        _unitAnimation = GetComponent<UCUnitAnimation> ();
        _unitAnimation?.Init (this);
        _unitType = unitTemplate.unitType;
        _name = unitTemplate.unitName;
        SetStartDirection (unitTemplate);
        InitStateMachine ();
    }

    protected void InitStateMachine()
    {

        _unitStateIdle = new UnitStateIdle (this, _unitTemplate, _unitAnimation);
        _unitStateHit = new UnitStateHit (this, _unitTemplate, _unitAnimation);
        _unitStateHold = new UnitStateHold (this, _unitAnimation, _targetFinder, _unitTemplate);
        _unitStateAttack = new UnitStateAttack (this, _unitAnimation, _targetFinder, _weapon);
        _unitStateDie = new UnitStateDie (this, _unitAnimation, _health, _unitTemplate);

        SetIdleState ();
    }
}
