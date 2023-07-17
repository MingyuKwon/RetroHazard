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
            if(status.isBlocked) return;

            if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Enemy not Body") ||
                contactCollider.gameObject.layer == LayerMask.NameToLayer("Enemy Body"))
            { 
                Debug.Log("Sheild Attack BLock");
                playerAnimation.SetSheildBlock();
                if(!GameManager.Sheild_Durability_Reducing)
                {
                    GameManager.EventManager.Invoke_Sheild_Durability_Reduce_Start_Event();
                }
            }
            
        }
    }

    public void OnTriggerEnter2D(Collider2D other) { // 일단은 플레이어에서 tirgger가 될 만한 곳은 패리 밖에 없으니 어느 부분과 Trigger가 되었는지는 구분 안해도 될 것 같다
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy not Body"))
        {
            if(other.gameObject.tag == "Attack" && status.parryFrame)
            {
                Debug.Log("Sheild Attack Parry");
                status.parrySuccess = true;
                GameManager.instance.EnemyCollideIgnore(true);
                GameManager.Sheild_Durability_Reducing = false;
                Player1.instance.playerAnimation.SheildColliderEnable(false);
                GameManager.isPlayerParry = true;
                playerAnimation.vfxAnimation.SetAnimationFlag("Trigger", "Parry");
                GameAudioManager.instance.PlaySFXMusic(SFXAudioType.ParrySuccess);
                GameManager.instance.SlowMotion();
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

    
}
