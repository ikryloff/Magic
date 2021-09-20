using System.Collections;
using UnityEngine;

public class BoardUnit : Unit 
{
    protected int _linePosition;
    protected int _columnPosition;

    protected string _name;
    protected float _health;
    protected float _currentHealth;

    protected float _damage;

    protected int _attackRange;
    protected float _attackRate;

    protected GameObject _bullet;
    protected GameObject _impact;
    protected GameObject _death;

    protected UnitTemplate _unitTemplate;
    protected UnitAnimation _animator;

    protected BoardUnitState _boardUnitState;
    #region

    
    

    public GameObject GetImpact()
    {
        return _impact;
    }

    public GameObject GetBullet()
    {
        return _bullet;
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

    public float GetUnitHealth()
    {
        return _health;
    }
    public void SetUnitHealth( float health )
    {
        _health = health;
    }

    public float GetUnitDamage()
    {
        return _damage;
    }

    public float GetUnitRange()
    {
        return _attackRange;
    }

    public float GetUnitRate()
    {
        return _attackRate;
    }

    
    

    private void CheckHP()
    {
        if ( _currentHealth <= 0 )
            MakeDeath ();
        if ( _currentHealth >= _health )
            _currentHealth = _health;
    }

    public void TakeDamage( BoardUnit unit, UnitTemplate sender )
    {
        if ( unit != this )
            return;
        _currentHealth -= sender.damage;
        float ratio = _currentHealth / _health;
        ShowHealthBar (ratio);
        CheckHP ();
        Debug.Log (this.name + " Got " + sender.damage + " points of damage");
    }

    private void ShowHealthBar( float ratio )
    {
        GameEvents.current.HealthChangedEvent (this, ratio);
    }

    public virtual BoardUnit GetRandomTarget() { return null; }
    public virtual void MakeDeath() { }
    public virtual void Idle() { }
    public virtual void GetEnemies( BoardUnit human, Cell cell ) { }

    public void SetBullet( UnitTemplate template )
    {
        if ( template.bulletPrefab == null )
            _bullet = template.bulletPrefab;
    }

    #endregion

    protected UnitStateIdle _unitStateIdle;
    protected UnitStateHold _unitStateHold;
    protected UnitStateAttack _unitStateAttack;
    protected UnitStateHit _unitStateHit;

    protected void InitStateMachine()
    {

        _unitStateIdle = new UnitStateIdle (this, _unitTemplate, _animator);
        _unitStateHit = new UnitStateHit (this, _unitTemplate, _animator);
        _unitStateHold = new UnitStateHold (this);
        _unitStateAttack = new UnitStateAttack (this);

        ChangeState (_unitStateIdle);
    }

    protected void Init(UnitTemplate template)
    {
        _unitType = template.unitType;
        _unitTemplate = template;
        _name = template.unitName;
        _health = template.health;
        _currentHealth = _health;
        _impact = template.impactPrefab;
        _death = template.deathPrefab;
        _attackRange = template.attackRange;
        _attackRate = template.attackRate;
        _animator = GetComponent<UnitAnimation>();
        Utilities.DisplaceZPosition (this); // to prevent flicking
        SetBullet (template);
        StartListening ();
        InitStateMachine ();
    }

    private void StartListening()
    {
        GameEvents.current.OnNewHit += TakeDamage;
        GameEvents.current.OnHumanPositionWasChanged += GetEnemies;
        GameEvents.current.OnTowerWasBuiltAction += GetEnemies;
    }

    private void StopListening()
    {
        Debug.Log ("Stop Listen");
        GameEvents.current.OnNewHit -= TakeDamage;
        GameEvents.current.OnHumanPositionWasChanged -= GetEnemies;
        GameEvents.current.OnTowerWasBuiltAction -= GetEnemies;
    }

    private void OnDisable()
    {
        StopListening ();
    }
}
