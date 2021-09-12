using System.Collections;
using UnityEngine;

public class Human : BoardUnit
{
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

    }

    public void Activate( int linePosition, UnitTemplate template )
    {
        oh = ObjectsHolder.Instance;
        xpPoints = oh.xpPoints;
        hp_norm = 1f;
        startHp = template.health;
        hitPoints = startHp;
        SetBullet (template);
        healthBarGO.SetActive (false);
        SetLinePosition (linePosition);
        DisplaceZPosition (); // to prevent flicking


        // GetMainTower ();
        // GetClosestTower ();
    }

    void Update()
    {
        transform.Translate (Vector2.left * 0.11f * Time.deltaTime);
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

    private void CheckHP()
    {
        if ( hitPoints <= 0 )
        {
            isDead = true;

            creepAnimation.StopFightAnimation ();
            healthBarGO.SetActive (false);
            xpPoints.AddPoints (xp, transform.position.x);
            MakeDeath ();
        }
    }



    public void TakeDamage( BoardUnit unit, UnitTemplate sender )
    {
        if ( unit != this )
            return;

        if ( !healthBarGO.activeSelf )
            healthBarGO.SetActive (true);
        hitPoints -= sender.damage;
        hp_norm = hitPoints / startHp;
        healthBar.SetHBSize (hp_norm);
        MakeImpact ();
        creepAnimation.HitAnimation ();
        CheckHP ();

        Debug.Log (this.name + " Got " + sender.damage + " points of damage");
    }

    private void MakeImpact()
    {
        Instantiate (impactPref, transform.position, Quaternion.identity);
    }

    private void MakeDeath()
    {
        GameEvents.current.HumanDeathEvent (this);
        Instantiate (deathPref, transform.position, Quaternion.identity);
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

    private void SetBullet( UnitTemplate template )
    {
        if ( template.bulletPrefab == null )
            bulletPref = template.bulletPrefab;
        impactPref = GameAssets.instance.GetAssetByString (Constants.BLOOD_IMPACT);
        deathPref = GameAssets.instance.GetAssetByString (Constants.CREEP_DEATH);
    }

}

