﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    private float _speed;
    private BoardUnit _victim;
    private float _hp;
    private UnitTemplate _sender;

    public void SeekTowerToHeal( Tower tower, float hp )
    {
        _target = tower.transform;
        _hp = hp;
        _victim = tower;
    }


    public void SeekHuman( BoardUnit human, UnitTemplate template )
    {
        if ( human != null )
        {
            _speed = Constants.BULLET_SPEED;
            _victim = human;
            _target = human.transform;
            _sender = template;
        }


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
        if ( _victim != null )
        {
            GameEvents.current.NewHit (_victim, _sender);
            Instantiate (_victim.GetImpact (), transform.position, Quaternion.identity);
        }
        _target = null;
        _victim = null;

        Destroy (gameObject);
    }

}
