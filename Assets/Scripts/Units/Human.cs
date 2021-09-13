using System.Collections;
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
    public GameObject deathPref;
    private GameObject suppressionWind;
    private GameObject defenceAffect;


    //private float time;

    private void OnEnable()
    {
        GameEvents.current.OnNewHit += TakeDamage;
    }
    private void OnDisable()
    {
        GameEvents.current.OnNewHit -= TakeDamage;
    }



    private void Awake()
    {
        suppressionWind = GameAssets.instance.GetAssetByString (Constants.SUPPRESSION_WIND);
        defenceAffect = GameAssets.instance.GetAssetByString (Constants.DEFFENCE_AFFECT);
        creepAnimation = GetComponent<CreepAnimation> ();

    }

    public void Activate( int linePosition, UnitTemplate template )
    {
        _unitTemplate = template;
        _health = _unitTemplate.health;
        _currentHealth = _health;
        _xp = _unitTemplate.xp;
        _speed = _unitTemplate.speed;
        _impact = _unitTemplate.impactPrefab;
        _death = _unitTemplate.deathPrefab;
        SetLinePosition (linePosition);
        DisplaceZPosition (); // to prevent flicking
        SetBullet (_unitTemplate);
    }

    void Update()
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

    private void OnTriggerEnter2D( Collider2D collider )
    {
        if ( collider.gameObject.tag.Equals (Constants.CELL_TAG) )
        {
            Cell cell = collider.GetComponent<Cell> ();
            this.SetColumnPosition (cell.GetColumnPosition ());
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

