using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerHealthLogic 
{
    private Rigidbody2D rb;
    private CapsuleCollider2D playerBodyCollider;
    private PlayerAnimation playerAnimation;
    private PlayerStatus status;


    private Vector2 ForceInput;
    const float reflectForceScholar = 400f;
    const float damageStandard = 10f;

    public PlayerHealthLogic(Rigidbody2D rb, PlayerAnimation playerAnimation, CapsuleCollider2D playerBodyCollider)
    {
        this.rb = rb;
        this.playerAnimation = playerAnimation;
        this.playerBodyCollider = playerBodyCollider;
    }

    public void Start() {
        status = Player1.instance.playerStatus;
    }


    // 우선 layer로 전체 충돌을 감지 한 다음에, 세부적으로 충돌이 어느 부위와 일어 났는지는 tag로 확인한다

    // block success Enemy는 막아도 콜라이더 처리가 완벽하지 않아서 적 공격이 뚫고 들어오는 경우가 종종 있다
    // 따라서 아까 막은 공격이라면 그 공격의 연속적인 콜라이더에 대한 충돌을 그냥 없는 것 처리해 주기 위해서 받는 인수
    public float playerHealthCollisionEnter(Collision2D other)
    {
        Collider2D contactCollider = other.GetContact(0).collider;

        float damage = 0f;

        if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Enemy Body") 
            || contactCollider.gameObject.layer == LayerMask.NameToLayer("Enemy not Body")) // enemy 몸이나 enemy의 일부에 맞은 경우 
        {
            if(other.otherCollider.tag == "Player Body") // 만약 플레이어의 몸체에 닿았다면
            {
                GameObject contactObject = contactCollider.transform.parent.transform.parent.gameObject;
                EnemyStatus contactEnemyStat = contactObject.GetComponentInChildren<EnemyStatus>();

                if(status.blockSuccessEnemy == contactObject.name) return 0;


                if(contactCollider.tag == "Enemy Body") // 몬스터의 몸에 부딪혔나?
                {
                    damage = contactEnemyStat.Attack * contactEnemyStat.bodyDamageRatio;
                }
                else if(contactCollider.tag == "Attack") // 몬스터가 공격 한 것을 맞았냐?
                {
                    damage = contactEnemyStat.Attack * contactEnemyStat.AttackDamageRatio;
                }

                ForceInput = other.GetContact(0).normal;
                DamageReDuce();
                Debug.Log("player had damage : " + damage);
                Reflect(damage);
                status.HealthChangeDefaultMinus(damage);

                playerAnimation.StunAnimationStart();
            }
        }

        return damage;
        // 반환 값으로 자기가 최종적으로 받은 데미지를 반환한다. 데미지가 0이면 피해를 안입은거고, 데미지에 따라 해야 할 일이 있다면 
        // 반환 값에 맞춰 하면 된다
    }   

    private void DamageReDuce()
    {
        //damage = damage * ( (float)(100 - status.ArmorDefence) / 100 );
    }

    private void Reflect(float Damage)
    {
        Damage = Mathf.Log(Damage / damageStandard + 1);
        rb.AddForce(ForceInput * reflectForceScholar * Damage);
    }


    /////////////////////animation event///////////////////////////
    public void StunStart()
    {
        if(GameManager.Sheild_Durability_Reducing)
        {
            status.SheildDurabilityChange(1);
        }

        status.blockSuccessEnemy = null;

        GameManager.instance.SetPausePlayer(true);
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.ResetPlayerAnimationState();
        playerBodyCollider.enabled = false;
        playerAnimation.vfxAnimation.StunAnimationStart();
    }

    public void StunEnd()
    {
        GameManager.instance.SetPausePlayer(false);
        playerBodyCollider.enabled = true;
    }

    /////////////////////animation event///////////////////////////
}
