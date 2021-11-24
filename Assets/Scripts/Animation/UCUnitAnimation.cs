using System.Collections;
using UnityEngine;


[RequireComponent (typeof (UCTargetFinder))]
public class UCUnitAnimation : MonoBehaviour
{
    private Animator animator;
    private float _attackTime = 0f;
    private BoardUnit _unit;

    public void Init( BoardUnit unit )
    {
        _unit = unit;
        animator = GetComponent<Animator> ();
        GetAttackAnimationClipTime ();
        GameEvents.current.OnNewHit += AnimateHit;
        GameEvents.current.OnAttackStartedEvent += AttackAnimation;
        GameEvents.current.OnIdleStateEvent += IdleAnimation;
        GameEvents.current.OnStopToFightEvent += AnimateStay;

        IdleAnimation (_unit);
    }

    private void OnDestroy()
    {
        GameEvents.current.OnNewHit -= AnimateHit;
        GameEvents.current.OnAttackStartedEvent -= AttackAnimation;
        GameEvents.current.OnIdleStateEvent -= IdleAnimation;
        GameEvents.current.OnStopToFightEvent -= AnimateStay;
    }

    private void GetAttackAnimationClipTime()
    {
        if ( animator == null ) return;
        AnimationClip [] clips = animator.runtimeAnimatorController.animationClips;
        foreach ( AnimationClip clip in clips )
        {
            if ( clip.name.Equals (Constants.ANIM_UNIT_ATTACK) )
            {
                _attackTime = clip.length;
            }
        }
    }

    private void AnimateHit( BoardUnit unit, UnitTemplate hitTemplate )
    {
        if ( unit != _unit ) return;
        animator.Play (Constants.ANIM_UNIT_HIT);
    }

    public void AttackAnimation( BoardUnit unit, BoardUnit enemy )
    {
        if ( unit != _unit ) return;

        if ( animator == null )
        {
            GameEvents.current.AttackAnimationFinishedAction (unit ,enemy);
            return;
        }
        animator.SetBool (Constants.ANIM_BOOL_IS_IN_BATTLE, true);
        animator.Play (Constants.ANIM_UNIT_ATTACK);
        StartCoroutine (AttackAnimationRoutine (_attackTime, enemy));
    }

    public void AnimateWalk()
    {
        animator.Play (Constants.ANIM_UNIT_WALK);
    }

    public void AnimateStay(BoardUnit unit, BoardUnit enemy )
    {
        if ( unit != _unit ) return;
        if ( animator == null ) return;
        animator.Play (Constants.ANIM_UNIT_STAY);
        if ( enemy != null )
            animator.SetBool (Constants.ANIM_BOOL_IS_IN_BATTLE, true);
    }

   


    private IEnumerator AttackAnimationRoutine( float length, BoardUnit enemy )
    {
        while ( length > 0 )
        {
            length -= Time.deltaTime;
            yield return null;
        }
        GameEvents.current.AttackAnimationFinishedAction (_unit, enemy);
    }

     
    public void IdleAnimation( BoardUnit unit )
    {
        if ( unit != _unit ) return;
        if ( animator == null ) return;
        if ( _unit.GetUnitType () == Unit.UnitType.Human )
            AnimateWalk ();
        else
            AnimateStay (_unit, null);
        animator.SetBool (Constants.ANIM_BOOL_IS_IN_BATTLE, false);
    }

   

}
