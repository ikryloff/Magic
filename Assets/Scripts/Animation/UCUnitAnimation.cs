using System.Collections;
using UnityEngine;

[RequireComponent (typeof (UCTargetFinder))]
public class UCUnitAnimation : MonoBehaviour
{
    private Animator animator;
    private float _attackLeftTime;
    private float _attackRightTime;
    private float _hitLeftTime;
    private float _hitRightTime;
    private BoardUnit _unit;
    

    public void Init(BoardUnit unit)
    {
        _unit = unit;
        animator = GetComponent<Animator> ();
        GetAnimationClipTime ();
    }

    private void GetAnimationClipTime()
    {
        AnimationClip [] clips = animator.runtimeAnimatorController.animationClips;
        foreach ( AnimationClip clip in clips )
        {
            if ( clip.name.Equals (Constants.ANIM_UNIT_HIT_LEFT) )
                _hitLeftTime = clip.length;
            if ( clip.name.Equals (Constants.ANIM_UNIT_HIT_RIGHT) )
                _hitRightTime = clip.length;
            if ( clip.name.Equals (Constants.ANIM_UNITY_ATTACK_LEFT) )
                _attackLeftTime = clip.length;
            if ( clip.name.Equals (Constants.ANIM_UNITY_ATTACK_RIGHT) )
                _attackRightTime = clip.length;
        }
    }

    public void AnimateHitWhenLeft()
    {
        animator.enabled = true;
        animator.Play (Constants.ANIM_UNIT_HIT_LEFT);
        StartCoroutine (HitAnimationRoutine (_hitLeftTime, Constants.ANIM_UNIT_HIT_LEFT));
    }

    public void AnimateHitWhenRight()
    {
        animator.enabled = true;
        animator.Play (Constants.ANIM_UNIT_HIT_RIGHT);
        StartCoroutine (HitAnimationRoutine (_hitRightTime, Constants.ANIM_UNIT_HIT_RIGHT));
    }

    public void AttackLeftAnimation()
    {
        animator.enabled = true;
        animator.Play (Constants.ANIM_UNITY_ATTACK_LEFT);
        StartCoroutine (AttackAnimationRoutine (_attackLeftTime, Constants.ANIM_UNITY_ATTACK_LEFT));
    }

    public void AttackRightAnimation()
    {
        animator.enabled = true;
        animator.Play (Constants.ANIM_UNITY_ATTACK_RIGHT);
        StartCoroutine (AttackAnimationRoutine (_attackRightTime, Constants.ANIM_UNITY_ATTACK_RIGHT));
    }

    public void AnimateWalk()
    {
        animator.enabled = true;
        animator.Play (Constants.ANIM_UNIT_WALK);
    }

    public void AnimateStayLeft()
    {
        animator.enabled = true;
        animator.Play (Constants.ANIM_UNIT_STAY_LEFT);

    }

    public void AnimateStayRight()
    {
        animator.enabled = true;
        animator.Play (Constants.ANIM_UNIT_STAY_RIGHT);
    }

    private IEnumerator HitAnimationRoutine( float length, string animType )
    {
        while ( length > 0 )
        {
            length -= Time.deltaTime;
            yield return null;
        }
        GameEvents.current.AnimationFinishedEvent (_unit, animType);
    }

    private IEnumerator AttackAnimationRoutine( float length, string animType )
    {
        while ( length > 0 )
        {
            length -= Time.deltaTime;
            yield return null;
        }
        GameEvents.current.AnimationFinishedEvent (_unit, animType);
    }

    public void StopAllAnimations()
    {
        StopAllCoroutines ();
        animator.enabled = false;
    }

    public void IdleAnimation()
    {
        animator.enabled = true;
        if ( _unit.GetUnitType () == Unit.UnitType.Human )
            AnimateWalk ();
        else
            AnimateStayRight ();
    }

}
