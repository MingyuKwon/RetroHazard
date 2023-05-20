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
    public static event Action Recovery_VFX_Start_Event;
    private CapsuleCollider2D playerBodyCollider;
    private Collider2D contactCollider;
    PlayerStatus status;
    PlayerAnimation playerAnimation;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        playerAnimation = GetComponent<PlayerAnimation>();
        status = GetComponentInChildren<PlayerStatus>();
        playerBodyCollider = GetComponentInChildren<PlayerCollider>().gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        contactCollider = other.GetContact(0).collider;
        if(other.otherCollider.tag == "Sheild")
        {
            if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Enemy not Body"))
            {
                if(contactCollider.tag == "Attack" && status.parryFrame)
                {
                    playerAnimation.vfxAnimation.SetAnimationFlag("Trigger", "Parry");
                    status.parrySuccess = true;
                    GameManager.instance.isPlayerParry = true;
                    GameManager.instance.SlowMotion();
                }else
                {
                    animator.SetTrigger("Block");
                    if(!GameManager.instance.Sheild_Durability_Reducing)
                    {
                        Sheild_Durability_Reduce_Start_Event.Invoke();
                    }
                    status.blockSuccessEnemy = contactCollider.transform.parent.transform.parent.name;
                }
            }
            else if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Enemy Body"))
            {
                animator.SetTrigger("Block");
                if(!GameManager.instance.Sheild_Durability_Reducing)
                {
                    Sheild_Durability_Reduce_Start_Event.Invoke();
                }
                status.blockSuccessEnemy = contactCollider.transform.parent.transform.parent.name;
            }

            
            
        }
        
    }

    public void RecoveryVFXStart()
    {
        Recovery_VFX_Start_Event.Invoke();
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
            status.UpdateIngameUI();
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
        playerAnimation.SetAnimationFlag("Trigger", "Parry");
        
        status.blockSuccessEnemy = null;
        GameManager.instance.ResetPlayerAnimationState();
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
