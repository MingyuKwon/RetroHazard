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

    public PlayerSheildLogic(PlayerAnimation playerAnimation)
    {
        this.playerAnimation = playerAnimation;
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

    public void Start() {
        status = Player1.instance.playerStatus;
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
            UI.instance.inGameUI.UpdateIngameUI();
        }
        status.parrySuccess = false;

    }

}
