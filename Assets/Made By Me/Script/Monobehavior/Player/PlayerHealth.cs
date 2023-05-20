using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerStatus status;
    private CapsuleCollider2D playerBodyCollider;
    private PlayerAnimation playerAnimation;


    private Collider2D contactCollider;
    private GameObject contactObject;
    private EnemyStatus contactEnemyStat;
    private Vector2 ForceInput;

    const float reflectForceScholar = 300f;
    private float damage = 0f;
    const float damageStandard = 10f;
    
    public static PlayerHealth instance;
    private void Awake() {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        status = GetComponentInChildren<PlayerStatus>();
        playerAnimation = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();
        playerBodyCollider = GetComponentInChildren<PlayerCollider>().gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable() {
        PauseMainUI.GotoMainMenuEvent += DestroyMyself;
    }

    private void OnDisable() {
        PauseMainUI.GotoMainMenuEvent -= DestroyMyself;
    }

    private void DestroyMyself()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {

        contactCollider = other.GetContact(0).collider;

        if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Enemy Body") || contactCollider.gameObject.layer == LayerMask.NameToLayer("Enemy not Body"))
        {
            if(other.otherCollider.tag == "Player Body")
            {
                contactObject = contactCollider.transform.parent.transform.parent.gameObject;
                contactEnemyStat = contactObject.GetComponentInChildren<EnemyStatus>();

                if(status.blockSuccessEnemy == contactObject.name) return;


                if(contactCollider.tag == "Enemy Body")
                {
                    damage = contactEnemyStat.Attack * contactEnemyStat.bodyDamageRatio;
                }
                else if(contactCollider.tag == "Attack")
                {
                    damage = contactEnemyStat.Attack * contactEnemyStat.AttackDamageRatio;
                }

                ForceInput = other.GetContact(0).normal;
                DamageReDuce();
                Debug.Log("player had damage : " + damage);
                Reflect(damage);
                playerAnimation.SetAnimationFlag("Trigger","Stun");
                status.HealthChange(damage);
                damage = 0;
            }
        }
    }

    private void DamageReDuce()
    {
        damage = damage * ( (float)(100 - status.ArmorDefence) / 100 );
    }

    private void Reflect(float Damage)
    {
        Damage = Mathf.Log(Damage / damageStandard + 1);

        rb.AddForce(ForceInput * reflectForceScholar * Damage);
    }


    public void StunStart()
    {
        if(GameManager.instance.Sheild_Durability_Reducing)
        {
            status.SheildDurabilityChange(1);
        }
        GameManager.instance.SetPausePlayer(true);
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.ResetPlayerAnimationState();
        status.blockSuccessEnemy = null;
        playerBodyCollider.enabled = false;
        playerAnimation.vfxAnimation.SetAnimationFlag("Trigger","Stun");

    }

    public void StunEnd()
    {
        GameManager.instance.SetPausePlayer(false);
        playerBodyCollider.enabled = true;
    }

}
