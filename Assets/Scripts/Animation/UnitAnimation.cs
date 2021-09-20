using System;
using System.Collections;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    //private BoardUnitState unitState;
    private Animator animator;
    private float _attackLeftTime;
    private float _attackRightTime;
    private float _hitLeftTime;
    private float _hitRightTime;
    private BoardUnit _unit;
    
    public bool IsOver;

    private void Awake()
    {
        //unitState = GetComponent<BoardUnitState> ();
        _unit = GetComponent<BoardUnit> ();
        animator = GetComponent<Animator> ();
        GetAnimationClipTime ();
    }

    private void GetAnimationClipTime()
    {
        AnimationClip [] clips = animator.runtimeAnimatorController.animationClips;
        foreach ( AnimationClip clip in clips )
        {
            if(clip.name.Equals(Constants.ANIM_UNIT_HIT_LEFT) )
                _hitLeftTime = clip.length;
            if ( clip.name.Equals (Constants.ANIM_UNIT_HIT_RIGHT) )
                _hitRightTime = clip.length;
            if ( clip.name.Equals (Constants.ANIM_UNITY_ATTACK_LEFT) )
                _attackLeftTime = clip.length;
            if ( clip.name.Equals (Constants.ANIM_UNITY_ATTACK_RIGHT) )
                _attackRightTime = clip.length;
        }
    }

    public void HitRightAnimation()
    {
        animator.Play (Constants.ANIM_UNIT_HIT_RIGHT);
        StartCoroutine (HitAnimationRoutine (_hitRightTime));
    }
    public void HitLeftAnimation()
    {
        animator.Play (Constants.ANIM_UNIT_HIT_LEFT);
        StartCoroutine (HitAnimationRoutine (_hitLeftTime));
    }

    public void AttackLeftAnimation()
    {
        animator.Play (Constants.ANIM_UNITY_ATTACK_LEFT);
        StartCoroutine (AttackAnimationRoutine (_attackLeftTime));
    }

    public void AttackRightAnimation()
    {
        animator.Play (Constants.ANIM_UNITY_ATTACK_RIGHT);
        StartCoroutine (AttackAnimationRoutine (_attackRightTime));
    }

    public void AnimateWalk()
    {
        animator.Play (Constants.ANIM_UNIT_WALK);
    }

    public void AnimateStayLeft()
    {
        animator.Play (Constants.ANIM_UNIT_STAY_LEFT);

    }

    public void AnimateStayRight()
    {
        animator.Play (Constants.ANIM_UNIT_STAY_RIGHT);
    }

    private IEnumerator HitAnimationRoutine(float length)
    {
        while ( length > 0 )
        {
            length -= Time.deltaTime;
            yield return null;
        }
        //unitState.Decide();
    }

    private IEnumerator AttackAnimationRoutine( float length )
    {
        while ( length > 0 )
        {
            length -= Time.deltaTime;
            yield return null;
        }
       // unitState.Fire ();
    }

    public void StopAllAnimations()
    {
        StopAllCoroutines ();
    }

    public void IdleAnimation()
    {
        StopAllCoroutines ();
        if ( _unit.GetUnitType () == Unit.UnitType.Human )
            AnimateWalk ();
        else
            AnimateStayRight ();

    }

}
