using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : BoardUnit
{
    private CreepAnimation creepAnimation;
    [SerializeField]
    private float _xp;
    private float _speed;
    private bool isSlow;
    private float fireDelay;
    private float fireCountDown;
    private List<TowerUnit> _towers;
    public GameObject deathPref;
    private GameObject suppressionWind;
    private GameObject defenceAffect;
    private HumanState _humanState;


    public enum HumanState
    {
        Walk,
        AttackLeft,
        AttackRight,
        ReadyLeft,
        ReadyRight,
        Hit,
        Die,
    }

    private void Awake()
    {
        suppressionWind = GameAssets.instance.GetAssetByString (Constants.SUPPRESSION_WIND);
        defenceAffect = GameAssets.instance.GetAssetByString (Constants.DEFFENCE_AFFECT);
        creepAnimation = GetComponent<CreepAnimation> ();


    }

    private void OnEnable()
    {
        GameEvents.current.OnNewHit += TakeDamage;
        GameEvents.current.OnHumanPositionWasChanged += GetEnemies;
        GameEvents.current.OnTowerWasBuilt += GetEnemies;
    }
    private void OnDisable()
    {
        GameEvents.current.OnNewHit -= TakeDamage;
        GameEvents.current.OnHumanPositionWasChanged -= GetEnemies;
        GameEvents.current.OnTowerWasBuilt -= GetEnemies;
    }

    private void GetEnemies( BoardUnit human, Cell cell )
    {
        if(human.GetType() == typeof(Human) && human != this )
            return;

        if ( cell.GetLinePosition() != _linePosition )
            return;

        List<TowerUnit> enemies = UnitsOnBoard.LineTowersList [_linePosition];

        if ( enemies.Count == 0 )
            return;

        for ( int i = 0; i < enemies.Count; i++ )
        {
            if ( Mathf.Abs (enemies [i].GetColumnPosition () - _columnPosition) <= attackRange )
            {
                if ( !_towers.Contains (enemies [i]) )
                {
                    _towers.Add (enemies [i]);
                    Debug.Log (name + " Found " + enemies [i].name + " Count " + _towers.Count + " at line " + _linePosition);
                }
            }
        }

        if(_towers.Count > 0 )
        {
            Decide ();
        }
        else
        {
            ChangeState (HumanState.Walk);
        }

    }

    private void Decide()
    {
        if(GetRandomTarget().transform.position.x < transform.position.x )
        {
            ChangeState (HumanState.ReadyLeft);
        }
        else
        {
            ChangeState (HumanState.ReadyRight);
        }
    }

    private TowerUnit GetRandomTarget()
    {
        return _towers [Random.Range (0, _towers.Count)];
    }
   

    public void Activate( int linePosition, UnitTemplate template )
    {
        _towers = new List<TowerUnit> ();
        _unitTemplate = template;
        _health = _unitTemplate.health;
        _currentHealth = _health;
        _xp = _unitTemplate.xp;
        _speed = _unitTemplate.speed;
        _impact = _unitTemplate.impactPrefab;
        _death = _unitTemplate.deathPrefab;
        attackRange = _unitTemplate.attackRange;
        SetLinePosition (linePosition);
        DisplaceZPosition (); // to prevent flicking
        SetBullet (_unitTemplate);
        ChangeState (HumanState.Walk);

    }

    void Update()
    {
        UpdateState ();
    }

    public void UpdateState()
    {
        switch ( _humanState )
        {
            case HumanState.Walk:
                Walk ();
                break;
            case HumanState.AttackLeft:
                break;
            case HumanState.AttackRight:
                break;
            case HumanState.ReadyLeft:
                _speed = 0;
                break;
            case HumanState.ReadyRight:
                _speed = 0;
                break;
            case HumanState.Hit:
                break;
            case HumanState.Die:
                break;
        }
    }

    private void ChangeState(HumanState state )
    {
        _humanState = state;
        Debug.Log ( name +  " State " + _humanState);
    }

    private void Walk()
    {
        transform.Translate (Vector2.left * _speed * Time.deltaTime);
    }

    public void SetSlowSpeed()
    {
        if ( !isSlow )
        {
            _speed = _speed / 2;
            isSlow = true;
            PlaySlowAffect ();
        }
    }



    public void PlaySlowAffect()
    {
        Instantiate (defenceAffect, transform);
        Instantiate (suppressionWind, transform);
    }

    private void OnTriggerExit2D( Collider2D collider )
    {
        if ( collider.gameObject.tag.Equals (Constants.CELL_TAG) )
        {
            Cell cell = collider.GetComponent<Cell> ();
            this.SetColumnPosition (cell.GetColumnPosition () - 1);
            this.SetLinePosition (cell.GetLinePosition ());
            GameEvents.current.HumanPositionWasChanged (this, cell);
        }
    }




    public void MoveUp()
    {
        //ec.RemoveCreepFromEnemyList (this);
        //linePosition -= 1;
        //sprite.sortingOrder = linePosition;
        //creepTransform.position = new Vector3 (creepTransform.position.x, creepTransform.position.y + Constants.CELL_HEIGHT, creepTransform.position.z);
        //ec.AddCreepToEnemyList (this);


    }

    public void MoveDown()
    {
        //ec.RemoveCreepFromEnemyList (this);
        //linePosition += 1;
        //sprite.sortingOrder = linePosition;
        //creepTransform.position = new Vector3 (creepTransform.position.x, creepTransform.position.y - Constants.CELL_HEIGHT, creepTransform.position.z);
        //ec.AddCreepToEnemyList (this);

    }

    public void MoveBack()
    {
        transform.position = new Vector3 (transform.position.x + Constants.CELL_WIDTH * 5, transform.position.y, transform.position.z);

    }

    public override void AnimateHit()
    {
        creepAnimation.HitAnimation ();
    }


    public override void MakeDeath()
    {
        GameEvents.current.HumanDeathEvent (this);
        Instantiate (_death, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }



    private void AttackSerias()
    {
        if ( fireCountDown <= 0 )
        {
            Attack (_damage);
            fireCountDown = fireDelay;
        }
        fireCountDown -= Time.deltaTime;
    }


    private void Attack( float damage )
    {


        //if ( targetTower == null )
        //{
        //    GetClosestTower ();
        //    return;
        //}
        //creepAnimation.FaceToAnimation (targetTower);
        //creepAnimation.AttackAnimation (targetTower);
        //StartCoroutine (FireAfterAnimation (damage));
    }

    public void Fire( float damage )
    {
        //if ( targetTower )
        //{
        //    if ( isRanger )
        //    {
        //        GameObject bulletGO = Instantiate (bulletPref, creepTransform.position, Quaternion.identity) as GameObject;
        //        Bullet bullet = bulletGO.GetComponent<Bullet> ();
        //        if ( bullet != null )
        //        {
        //            bullet.SeekTower (targetTower, damage);
        //        }
        //    }
        //    else
        //    {
        //        targetTower.CalcDamage (damage);
        //    }
        //    targetTower.UpdateTargetAfterHit (this);
        //}
    }

    private IEnumerator FireAfterAnimation( float damage )
    {
        yield return new WaitForSeconds (Constants.ANIM_ATTACK_TIME);
        Fire (damage);
    }



}

