﻿using UnityEngine;

public class CreepAnimation : MonoBehaviour
{
    private Human creep;
    private Animator animator;
    private bool isFightAnimationOn;
    private bool isBackDirection;

    private void Awake()
    {
        creep = GetComponent<Human> ();
        animator = GetComponent<Animator> ();
    }    

    public void HitAnimation()
    {        
        if ( isBackDirection )
            animator.Play (Constants.ANIM_ENEMY_HIT_BACK);
        else
            animator.Play (Constants.ANIM_ENEMY_HIT_FORWARD);
    }

    public bool IsFightAnimationIsOn()
    {
        return isFightAnimationOn;
    }

    //public void FaceToAnimation( TowerTemp tower )
    //{
    //    isFightAnimationOn = true;
    //    bool isTargetForward = IsTargetFoward (tower.towerTransform);
    //    if ( isTargetForward ) // target forward
    //    {
    //        animator.SetBool (Constants.ANIM_ENEMY_FIGHT_STATE_BACK, false);
    //        animator.SetBool (Constants.ANIM_ENEMY_FIGHT_STATE_FORWARD, true);
    //        isBackDirection = false;
    //    }
    //    else
    //    {
    //        animator.SetBool (Constants.ANIM_ENEMY_FIGHT_STATE_FORWARD, false);
    //        animator.SetBool (Constants.ANIM_ENEMY_FIGHT_STATE_BACK, true);
    //        isBackDirection = true;
    //    }
    //}

    public void StopFightAnimation()
    {
        isFightAnimationOn = false;
        animator.SetBool (Constants.ANIM_ENEMY_FIGHT_STATE_BACK, false);
        animator.SetBool (Constants.ANIM_ENEMY_FIGHT_STATE_FORWARD, false);
        isBackDirection = false;
    }

    //public void AttackAnimation( TowerTemp tower )
    //{
    //    bool isTargetForward = IsTargetFoward (tower.towerTransform);
    //    if ( isTargetForward ) // target forward
    //    {
    //        animator.Play (Constants.ANIM_ENEMY_ATTACK_FORWARD);
    //        isBackDirection = false;
    //    }
    //    else
    //    {
    //        animator.Play (Constants.ANIM_ENEMY_ATTACK_BACK);
    //        isBackDirection = true;
    //    }

    //}

    private bool IsTargetFoward( Transform tower )
    {
        if ( tower && creep )
            return creep.creepTransform.position.x - tower.position.x >= 0;
        else return false;
    }


}
