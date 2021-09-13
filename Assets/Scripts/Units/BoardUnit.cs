using System;
using UnityEngine;

public class BoardUnit : Unit
{
    public enum States
    {
        Idle, //common state
        Seeking, //looking to the target
        Attacking, //attack cycle animation
        Dead //dead animation, before removal from play field
    }

    public static float Displace = 0f;
    protected int _linePosition;
    protected int _columnPosition;

    protected string _name;
    protected float _health;
    protected float _currentHealth;

    protected float _damage;

    public float attackRange;
    public float attackRate;

    public GameObject _bullet;
    public GameObject _impact;
    public GameObject _death;

    protected UnitTemplate _unitTemplate;

    public GameObject GetImpact()
    {
        return _impact;
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



    protected float GetSpriteDisplace()
    {
        if ( Displace > 0.9999f )
            Displace = 0.0001f;
        Displace += 0.0001f;
        return Displace;
    }

    // to prevent flickering
    protected void DisplaceZPosition()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer> ();
        float dp = GetSpriteDisplace ();
        sr.sortingOrder = GetLinePosition ();
        transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + dp);

    }

    private void CheckHP()
    {
        if ( _currentHealth <= 0 )
            MakeDeath ();
        if (_currentHealth >= _health )
            _currentHealth = _health;
    }


    public void TakeDamage( BoardUnit unit, UnitTemplate sender )
    {
        if ( unit != this )
            return;
        _currentHealth -= sender.damage;
        float ratio = _currentHealth / _health;
        ShowHealthBar (ratio);
        AnimateHit ();
        CheckHP ();

        Debug.Log (this.name + " Got " + sender.damage + " points of damage");
    }

    private void ShowHealthBar(float ratio)
    {
        GameEvents.current.HealthChangedEvent (this, ratio);
    }

    public virtual void MakeDeath() { }
    public virtual void AnimateHit() { }


    public void SetBullet( UnitTemplate template )
    {
        if ( template.bulletPrefab == null )
            _bullet = template.bulletPrefab;
    }

}
