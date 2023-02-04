using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;


public class PlayerShield : MonoBehaviour
{
    public static event Action Sheild_Durability_Reduce_Start_Event;
    private CapsuleCollider2D playerBodyCollider;
    PlayerStatus status;
    PlayerAnimation playerAnimation;
    private Animator vfxAnimator;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        playerAnimation = GetComponent<PlayerAnimation>();
        vfxAnimator = GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>();
        status = GetComponentInChildren<PlayerStatus>();
        playerBodyCollider = GetComponentInChildren<PlayerCollider>().gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable() {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.otherCollider.tag == "Sheild" && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(other.GetContact(0).collider.tag == "Attack" && status.parryFrame)
            {
                vfxAnimator.SetTrigger("Parry");
                status.parrySuccess = true;
                GameManager.instance.SlowMotion();
            }else
            {
                animator.SetTrigger("Block");
                if(!GameManager.instance.Sheild_Durability_Reducing)
                {
                    Sheild_Durability_Reduce_Start_Event.Invoke();
                }
                status.blockSuccessEnemy = other.GetContact(0).collider.transform.parent.transform.parent.name;
            }
            
        }
        
    }


    public void ParryFrameStart()
    {
        status.parryFrame = true;
    }

    public void ParryFrameEnd()
    {
        status.parryFrame = false;
        if(!status.parrySuccess)
        {
            status.SheildDurabilityChange(1);
        }else
        {
            status.SheildDurabilityChange(0);
        }
        status.parrySuccess = false;
    }

    public void ParryStart()
    {
        if(!GameManager.instance.Sheild_Durability_Reducing)
        {
            Sheild_Durability_Reduce_Start_Event.Invoke();
        }
        GameManager.instance.SetPlayerMove(false);
    }

    public void ParryEnd()
    {
        playerAnimation.isParrying = false;
        
        GameManager.instance.SetPlayerFree();
        GameManager.instance.SetPlayerMove(true);
    }

    public void BlockStart()
    {
        GameManager.instance.SetPlayerMove(false);
        status.isBlocked = true;
    }

    public void BlockEnd()
    {
        status.SheildDurabilityChange(1);
        GameManager.instance.SetPlayerMove(true);
        status.isBlocked = false;
        status.blockSuccessEnemy = null;
    }
}
