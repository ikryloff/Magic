using UnityEngine;

public class BoardUnit : Unit
{
    private int _linePosition;
    private int _columnPosition;
    private string _direction;
    private string _name;
    private UnitTemplate _unitTemplate;
    private UnitAnimation _unitAnimation;
    private HealthBar _healthBar;
    private TargetFinder _targetFinder;
    private Weapon _weapon;

    protected Cell _cell;

    protected UnitStateIdle _unitStateIdle;
    protected UnitStateHold _unitStateHold;
    protected UnitStateAttack _unitStateAttack;
    protected UnitStateHit _unitStateHit;
    protected UnitStateDie _unitStateDie;
    protected GameObject _deathParticle;
    protected GameObject _bornParticle;
    private ParticleSystem _impactParticle;

    #region 
    public ParticleSystem GetImpactObject()
    {
        return _impactParticle;
    }

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

    public UnitTemplate GetUnitTemplate()
    {
        return _unitTemplate;
    }

    public Cell GetCurrentCell() { return _cell; }


    public void SetHoldState()
    {
        ChangeState (_unitStateHold);
    }

    public void MakeDeath()
    {
        RemoveUnit ();
        Instantiate (_deathParticle, transform.position, Quaternion.identity);
        SetDieState ();
        Destroy (gameObject);
    }

    public void MakeBorn()
    {
        if(_bornParticle != null)
            Instantiate (_bornParticle, transform.position, Quaternion.identity);
    }

    public void SetImpact( UnitTemplate template )
    {
        if ( template.impactPrefab != null )
        {
            GameObject imp = Instantiate (template.impactPrefab, transform.position, Quaternion.identity);
            imp.transform.parent = transform;
            _impactParticle = imp.GetComponent<ParticleSystem> ();
            _impactParticle.Stop ();
        }
    }

    public virtual void RemoveUnit() { }
    public virtual void IdleBehavior() { }

    #endregion

    
    public void SetIdleState()
    {
        ChangeState (_unitStateIdle);
    }

    public void SetHitState()
    {
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

    protected void Init( UnitTemplate template )
    {
        _unitTemplate = template;
        _healthBar = GetComponentInChildren<HealthBar> ();
        _healthBar?.Init (this);
        _targetFinder = GetComponent<TargetFinder>();
        _targetFinder?.Init (this);
        _weapon = GetComponent<Weapon> ();
        _weapon?.Init (this);
        _unitAnimation = GetComponent<UnitAnimation> ();
        _unitAnimation?.Init (this);
        SetImpact (_unitTemplate);
        _unitType = template.unitType;
        _name = template.unitName;
        _deathParticle = template.deathPrefab;
        _bornParticle = template.bornPrefab;
        MakeBorn ();
        SetStartDirection (template);
        InitStateMachine ();
    }

    protected void InitStateMachine()
    {

        _unitStateIdle = new UnitStateIdle (this, _unitTemplate, _unitAnimation);
        _unitStateHit = new UnitStateHit (this, _unitTemplate, _unitAnimation);
        _unitStateHold = new UnitStateHold (this, _unitAnimation, _targetFinder, _unitTemplate);
        _unitStateAttack = new UnitStateAttack (this, _unitAnimation, _targetFinder, _weapon);
        _unitStateDie = new UnitStateDie (this, _unitAnimation);

        SetIdleState ();
    }
}
