using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    private float _speed;
    private BoardUnit _victim;
    private float _damage;
    private float _hp;
    private Unit.UnitClassProperty _classProperty;


    public void SeekTowerToHeal( Tower tower, float hp )
    {
        _target = tower.transform;
        _hp = hp;
        _victim = tower;
    }


    public void SeekHuman( BoardUnit human, float damage, Unit.UnitClassProperty classProperty )
    {
        _speed = Constants.BULLET_SPEED;
        _victim = human;
        _target = human.transform;
        _damage = damage;
        _classProperty = classProperty;
        
    }

    private void Update()
    {
        if ( _target == null )
        {
            Destroy (gameObject);
            return;
        }

        Vector2 dir = _target.position - transform.position;
        float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
        float distThisFrame = _speed * Time.deltaTime;

        if ( dir.magnitude <= distThisFrame )
        {
            HitTarget ();
            return;
        }        
        transform.Translate (dir.normalized * distThisFrame, Space.World);
        
    }
    private void HitTarget()
    {
        if(_victim != null )
        {
            GameEvents.current.NewHit (_victim, _damage,_classProperty);
        }
        _target = null;
        _victim = null;
        Destroy (gameObject);
    }

}
