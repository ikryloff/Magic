using System.Collections;
using UnityEngine;

public class Human : BoardUnit
{
    EnemyController ec;
    private CreepAnimation creepAnimation;
    [SerializeField]
    private string enemyType;


    private float xp;
    private float speed;
    private bool isRanger;
    private bool isDead;
    private bool isSlow;
    private float hitPoints;
    private float startHp;
    private float hp_norm;
    private float fireDelay;
    private float fireCountDown;
    private float damage;
    private string bulletName;



    public GameObject bulletPref;
    public GameObject impactPref;
    public GameObject deathPref;
    private GameObject suppressionWind;
    private GameObject defenceAffect;
    private ObjectsHolder oh;
    private SpriteRenderer sprite;

    public Transform creepTransform;

    public HealthBar healthBar;
    private GameObject healthBarGO;
    private XPpoints xpPoints;


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
        healthBarGO = healthBar.gameObject;
        creepTransform = gameObject.transform;
        sprite = GetComponent<SpriteRenderer> ();
        // SetCreepProperties ();
    }

    private void Start()
    {
        oh = ObjectsHolder.Instance;
        xpPoints = oh.xpPoints;
        hp_norm = 1f;
        startHp = hitPoints;
        ec = oh.enemyController;
        SetBullet ();
        healthBarGO.SetActive (false);
        DisplaceZPosition (); // to prevent flicking

        // GetMainTower ();
        // GetClosestTower ();
    }

    void Update()
    {
        if ( isDead )
            return;
        creepTransform.Translate (Vector2.left * 0.11f * Time.deltaTime);
    }

    private void SetCreepProperties()
    {
        xp = EnemyProperties.GetXP (enemyType);
        speed = EnemyProperties.GetSpeed (enemyType);
        hitPoints = EnemyProperties.GetHP (enemyType);
        attackRange = EnemyProperties.GetAttackRange (enemyType);
        fireDelay = EnemyProperties.GetFireDelay (enemyType);
        damage = EnemyProperties.GetDamage (enemyType);
        isRanger = EnemyProperties.IsRanger (enemyType);
        if ( isRanger )
        {
            bulletName = EnemyProperties.GetBulletName (enemyType);
        }
    }

    public void SetSlowSpeed()
    {
        if ( !isSlow )
        {
            speed = speed / 2;
            isSlow = true;
            PlaySlowAffect ();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void DisplaceZPosition()
    {
        float dp = EnemyController.GetSpriteDisplace ();
        sprite.sortingOrder = GetLinePosition ();
        creepTransform.position = new Vector3 (creepTransform.position.x, creepTransform.position.y, creepTransform.position.z + dp);

    }

    public void PlaySlowAffect()
    {
        Instantiate (defenceAffect, creepTransform);
        Instantiate (suppressionWind, creepTransform);
    }

    private void OnTriggerEnter2D( Collider2D collider)
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
        creepTransform.position = new Vector3 (creepTransform.position.x + Constants.CELL_WIDTH * 5, creepTransform.position.y, creepTransform.position.z);

    }

    private void CheckHP()
    {
        if ( hitPoints <= 0 )
        {
            isDead = true;

            creepAnimation.StopFightAnimation ();
            healthBarGO.SetActive (false);
            ec.humans.Remove (this);
            xpPoints.AddPoints (xp, transform.position.x);
            MakeDeath ();
        }
    }



    public void TakeDamage( BoardUnit unit, float damage, Unit.UnitClassProperty property )
    {
        if ( unit != this )
            return;

        if ( !healthBarGO.activeSelf )
            healthBarGO.SetActive (true);
        hitPoints -= damage;
        hp_norm = hitPoints / startHp;
        healthBar.SetHBSize (hp_norm);
        MakeImpact ();
        creepAnimation.HitAnimation ();
        CheckHP ();

        Debug.Log (this.name + " Got " + damage + " points of damage");
    }

    private void MakeImpact()
    {
        Instantiate (impactPref, creepTransform.position, Quaternion.identity);
    }

    private void MakeDeath()
    {
        GameEvents.current.HumanDeath (this);
        Instantiate (deathPref, creepTransform.position, Quaternion.identity);
        Destroy (gameObject);

    }



    private void AttackSerias()
    {
        if ( fireCountDown <= 0 )
        {
            Attack (damage);
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

    private void SetBullet()
    {
        if ( bulletName != null )
            bulletPref = GameAssets.instance.GetAssetByString (bulletName);
        impactPref = GameAssets.instance.GetAssetByString (Constants.BLOOD_IMPACT);
        deathPref = GameAssets.instance.GetAssetByString (Constants.CREEP_DEATH);
    }

}

