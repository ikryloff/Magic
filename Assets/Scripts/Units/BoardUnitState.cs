using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardUnitState : MonoBehaviour
{
    public enum UnitState
    {
        Idle,
        AttackLeft,
        AttackRight,
        ReadyToLeft,
        ReadyToRight,
        HitWhenLeft,
        HitWhenRight,
        Die,
    }

    protected UnitState _unitState;
    protected BoardUnit _unit;
    protected UnitAnimation _animator;
    protected BoardUnit _enemy;

    private void OnEnable()
    {
        GameEvents.current.OnNewHit += AnimateHit;
    }

    private void OnDisable()
    {
        GameEvents.current.OnNewHit -= AnimateHit;
    }

    public void Init()
    {
        _unit = GetComponent<BoardUnit> ();
        _animator = GetComponent<UnitAnimation> ();
        ChangeState (UnitState.Idle);
    }

    public void UpdateState()
    {
        switch ( _unitState )
        {
            case UnitState.Idle:
                Idle ();
                break;
            case UnitState.AttackLeft:
                Attack ();
                break;
            case UnitState.AttackRight:
                Attack ();
                break;
            case UnitState.HitWhenLeft:
                HitWhenLeft ();
                break;
            case UnitState.HitWhenRight:
                HitWhenRight ();
                break;
            case UnitState.ReadyToLeft:
                StayReadyToLeft ();
                break;
            case UnitState.ReadyToRight:
                StayReadyToRight ();
                break;
            case UnitState.Die:
                Die ();
                break;
        }
    }

    public void ChangeState( UnitState state )
    {
        _unitState = state;
        UpdateState ();
    }

    public virtual void SetIdleState()
    {
        ChangeState (UnitState.Idle);
    }

    public virtual void SetDieState()
    {
        ChangeState (UnitState.Die);
    }

    public void AnimateHit( BoardUnit unit, UnitTemplate sender )
    {
        if ( unit != _unit )
            return;
        if ( _unitState == UnitState.ReadyToRight || _unitState == UnitState.AttackRight || _unitState == UnitState.HitWhenRight )
            ChangeState (UnitState.HitWhenRight);
        if( _unitState == UnitState.ReadyToLeft || _unitState == UnitState.AttackLeft || _unitState == UnitState.HitWhenLeft)
            ChangeState (UnitState.HitWhenLeft);
        if(_unitState == UnitState.Idle )
        {
            if(unit.GetType() == typeof(Human))
                ChangeState (UnitState.HitWhenLeft);
            else
                ChangeState (UnitState.HitWhenRight);
        }
    }

    public void Decide()
    {
        _enemy = _unit.GetRandomTarget ();

        if ( _enemy == null )
        {
            ChangeState (UnitState.Idle);
            return;
        }
        if ( _enemy.transform.position.x < transform.position.x )
        {
            ChangeState (UnitState.ReadyToLeft);
        }
        else
        {
            ChangeState (UnitState.ReadyToRight);
        }
    }

    public void StayReadyToLeft()
    {
        Stop ();
        _animator.AnimateStayLeft ();
        ChangeState (UnitState.AttackLeft);
    }

    public void StayReadyToRight()
    {
        Stop ();
        _animator.AnimateStayRight ();
        ChangeState (UnitState.AttackRight);
    }

    public void HitWhenLeft()
    {
        Stop ();
        _animator.HitLeftAnimation ();
    }

    public void HitWhenRight()
    {
        Stop ();
        _animator.HitRightAnimation ();
    }

    public virtual void Idle() { }
    public virtual void Stop() { }
    public virtual void Die() { }

    public void Attack()
    {
        if ( _enemy == null )
        {
            Decide ();
            return;
        }
        StartCoroutine (AttackByRateRoutine ());
    }


    IEnumerator AttackByRateRoutine()
    {
        float delay = _unit.GetUnitRate();
        while (  delay > 0 )
        {
            if( _enemy == null )
            {
                Stop ();
                Decide ();
                yield break;
            }
            delay -= Time.deltaTime;
            yield return null;
        }
        MakeShot ();
    }

    public void MakeShot()
    {
        if ( _enemy == null )
        {
            Decide ();
            return;
        }
        Debug.Log ( name + " Attack  " + Time.time);
        if ( _enemy.transform.position.x < transform.position.x )
        {
            _animator.AttackLeftAnimation ();
        }
        else
        {
            _animator.AttackRightAnimation ();
        }
    }

    public void Fire()
    {
        if ( _enemy )
        {
            if ( _unit.GetUnitRange() > 1 )
            {
                GameObject bulletGO = Instantiate (_unit.GetBullet(), transform.position, Quaternion.identity) as GameObject;
                Bullet bullet = bulletGO.GetComponent<Bullet> ();
                if ( bullet != null )
                {
                    bullet.SeekTarget (_enemy, _unit.GetUnitTemplate());
                }
            }
            else
            {
                GameEvents.current.NewHit (_enemy, _unit.GetUnitTemplate ());
            }
        }
        Decide ();
    }


}
