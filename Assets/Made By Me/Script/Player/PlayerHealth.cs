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
    private Animator animator;
    private Animator vfxAnimator;

    private Collider2D contactCollider;
    private GameObject contactObject;
    private EnemyStatus contactEnemyStat;
    private Vector2 ForceInput;

    const float reflectForceScholar = 150f;
    private float damage = 0f;
    const float damageStandard = 10f;
    
    private void Awake() {
        status = GetComponentInChildren<PlayerStatus>();
        animator = GetComponent<Animator>();
        vfxAnimator = GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerBodyCollider = GetComponentInChildren<PlayerCollider>().gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.otherCollider.tag == "Player Body" && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            contactCollider = other.GetContact(0).collider;
            contactObject = contactCollider.transform.parent.transform.parent.gameObject;
            contactEnemyStat = contactObject.GetComponentInChildren<EnemyStatus>();

            if(contactCollider.tag == "Enemy Body")
            {
                damage = contactEnemyStat.Attack * contactEnemyStat.bodyDamageRatio;
            }
            else if(contactCollider.tag == "Attack")
            {
                damage = contactEnemyStat.Attack * contactEnemyStat.AttackDamageRatio;
            }

            ForceInput = other.GetContact(0).normal;
            Reflect(damage);
            animator.SetTrigger("Stun");
        }
    }

    public void StunStart()
    {
        GameManager.instance.isPlayerPaused = true;
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.SetPlayerAnimationIdle();
        playerBodyCollider.enabled = false;
        vfxAnimator.SetTrigger("Stun");
    }

    public void StunEnd()
    {
        GameManager.instance.isPlayerPaused = false;
        playerBodyCollider.enabled = true;
    }

    private void Reflect(float Damage)
    {
        Damage = Mathf.Log(Damage / damageStandard + 1);

        rb.AddForce(ForceInput * reflectForceScholar * Damage);
    }
}
