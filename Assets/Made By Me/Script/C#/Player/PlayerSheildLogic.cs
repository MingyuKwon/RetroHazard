using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;


public class PlayerSheildLogic 
{
    PlayerAnimation playerAnimation;
    PlayerStatus status;

    public PlayerSheildLogic(PlayerAnimation playerAnimation, PlayerStatus status)
    {
        this.playerAnimation = playerAnimation;
        this.status = status;
    }

    public void CollisionEnter2D(Collision2D other)
    {
        Collider2D contactCollider = other.GetContact(0).collider;

        if(other.otherCollider.tag == "Sheild")
        {
            if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Enemy not Body"))
            {
                if(contactCollider.tag == "Attack" && status.parryFrame)
                {
                    playerAnimation.vfxAnimation.SetAnimationFlag("Trigger", "Parry");
                    status.parrySuccess = true;
                    GameManager.isPlayerParry = true;
                    GameManager.instance.SlowMotion();
                }else
                {
                    playerAnimation.SetAnimationFlag("Trigger", "Block");
                    if(!GameManager.Sheild_Durability_Reducing)
                    {
                        GameManager.EventManager.Invoke_Sheild_Durability_Reduce_Start_Event();
                    }
                    status.blockSuccessEnemy = contactCollider.transform.parent.transform.parent.name;
                }
            }
            else if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Enemy Body"))
            {
                playerAnimation.SetAnimationFlag("Trigger", "Block");
                if(!GameManager.Sheild_Durability_Reducing)
                {
                    GameManager.EventManager.Invoke_Sheild_Durability_Reduce_Start_Event();
                }
                status.blockSuccessEnemy = contactCollider.transform.parent.transform.parent.name;
            }

        }
    }

    public void RecoveryVFXStart()
    {
        GameManager.EventManager.Invoke_Recovery_VFX_Start_Event();
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
        if(!GameManager.Sheild_Durability_Reducing)
        {
            GameManager.EventManager.Invoke_Sheild_Durability_Reduce_Start_Event();
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
