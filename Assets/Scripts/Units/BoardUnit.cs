using UnityEngine;

public class BoardUnit : Unit
{
    protected int _linePosition;
    protected int _columnPosition;

    protected string _direction;

    protected string _name;
    protected float _health;
    protected float _currentHealth;

    protected float _damage;
    protected float _takenDamage;

    protected int _attackRange;
    protected float _attackRate;
    protected float _attackCurrentDelay;
    protected BoardUnit _currentEnemy;
    protected GameObject _bullet;
    protected GameObject _impact;
    protected ParticleSystem _impactParticle;
    protected GameObject _death;
    protected UnitTemplate _unitTemplate;
    protected UnitAnimation _animator;

    protected Cell _cell;

    protected UnitStateIdle _unitStateIdle;
    protected UnitStateHold _unitStateHold;
    protected UnitStateAttack _unitStateAttack;
    protected UnitStateHit _unitStateHit;
    protected UnitStateDie _unitStateDie;

    private SpriteRenderer _sprite;

    #region 

    public SpriteRenderer GetSprite( BoardUnit unit )
    {
        return _sprite;
    }

    public void SetCurrentEnemy( BoardUnit enemy )
    {
        _currentEnemy = enemy;
    }

    public BoardUnit GetCurrentEnemy()
    {
        return _currentEnemy;
    }

    public void SetDirection( string dir )
    {
        _direction = dir;
    }

    public string GetDirection()
    {
        return _direction;
    }

    public void SetTakenDamage( float damage )
    {
        _takenDamage = damage;
    }

    public float GetUnitTakenDamage()
    {
        return _takenDamage;
    }

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

    public void ChangeUnitCurrentHealth( float damage )
    {
        _currentHealth -= damage;
    }

    public float GetUnitCurrentHealth()
    {
        return _currentHealth;
    }

    public void SetUnitCurrentHealth( float health )
    {
        _currentHealth = health;
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

    public float GetUnitCurrentDelay()
    {
        return _attackCurrentDelay;
    }

    public void SetUnitCurrentDelay( float delay )
    {
        _attackCurrentDelay = delay;
    }

    public Cell GetCurrentCell() { return _cell; }

    private void TakeDamage( BoardUnit unit, UnitTemplate sender )
    {
        if ( unit != this )
            return;
        SetTakenDamage (sender.damage);
        ShowUnitImpact ();
        ChangeState (_unitStateHit);
    }

    public void SetHoldState()
    {
        ChangeState (_unitStateHold);
    }

    public virtual BoardUnit GetRandomTarget() { return null; }
    public virtual void MakeDeath() { }
    public virtual void IdleBehavior() { }

    #endregion

    public virtual void SeekEnemies( BoardUnit unit, Cell cell )
    {
        if ( cell.GetLinePosition () != _linePosition )
            return;

        ChangeState (_unitStateHold);
    }

    public void SetIdleState()
    {
        ChangeState (_unitStateIdle);
    }

    public void SetDieState()
    {
        ChangeState (_unitStateDie);
    }

    public void SetAttackState()
    {
        ChangeState (_unitStateAttack);
    }

    private void SetBullet( UnitTemplate template )
    {
        if ( template.bulletPrefab != null )
            _bullet = template.bulletPrefab;
    }

    public void SetStartDirection( UnitTemplate template )
    {
        if ( template.unitType == UnitType.Human )
            SetDirection (Constants.UNIT_LEFT_DIR);
        else
            SetDirection (Constants.UNIT_RIGHT_DIR);
    }

    private void SetImpact( UnitTemplate template )
    {
        if ( template.impactPrefab != null )
        {
            GameObject imp = Instantiate (template.impactPrefab, transform.position, Quaternion.identity);
            imp.transform.parent = transform;
            _impactParticle = imp.GetComponent<ParticleSystem> ();
            _impactParticle.Stop ();
        }
    }


    protected void Init( UnitTemplate template )
    {
        _unitType = template.unitType;
        _unitTemplate = template;
        _name = template.unitName;
        _health = template.health;
        _currentHealth = _health;
        SetImpact (template);
        _death = template.deathPrefab;
        _attackRange = template.attackRange;
        _attackRate = template.attackRate;
        _sprite = GetComponent<SpriteRenderer> ();
        _animator = GetComponent<UnitAnimation> ();
        Utilities.DisplaceZPosition (this); // to prevent flicking
        SetBullet (template);
        SetStartDirection (template);
        StartListening ();
        InitStateMachine ();
    }

    protected void InitStateMachine()
    {

        _unitStateIdle = new UnitStateIdle (this, _unitTemplate, _animator);
        _unitStateHit = new UnitStateHit (this, _unitTemplate, _animator);
        _unitStateHold = new UnitStateHold (this, _animator);
        _unitStateAttack = new UnitStateAttack (this, _animator);
        _unitStateDie = new UnitStateDie (this, _animator);

        SetIdleState ();
    }

    private void StartListening()
    {
        GameEvents.current.OnNewHit += TakeDamage;
        GameEvents.current.OnHumanPositionWasChanged += SeekEnemies;
        GameEvents.current.OnTowerWasBuiltEvent += SeekEnemies;
    }

    protected void StopListening()
    {
        GameEvents.current.OnNewHit -= TakeDamage;
        GameEvents.current.OnHumanPositionWasChanged -= SeekEnemies;
        GameEvents.current.OnTowerWasBuiltEvent -= SeekEnemies;
    }

    private void OnDestroy()
    {
        StopListening ();
    }

    public virtual void Fire( BoardUnit enemy )
    {
        if ( enemy )
        {
            if ( _attackRange > 1 )
            {
                GameObject bulletGO = Instantiate (_bullet, transform.position, Quaternion.identity) as GameObject;
                Bullet bullet = bulletGO.GetComponent<Bullet> ();
                if ( bullet != null )
                {
                    bullet.SeekTarget (enemy, _unitTemplate);
                }
            }
            else
            {
                GameEvents.current.NewHit (enemy, _unitTemplate);
            }
        }
    }

    public void ShowUnitImpact()
    {
        if(_impactParticle != null)
            _impactParticle.Play ();
    }

    
}
