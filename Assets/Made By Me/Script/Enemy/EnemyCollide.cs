using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollide : MonoBehaviour
{
    Rigidbody2D rb;
    EnemyStatus status;
    private CapsuleCollider2D EnemyBodyCollider;
    private EnemyManager enemyManager;
    private Animator animator;

    private Collider2D contactCollider;
    private GameObject contactObject;
    private PlayerStatus contactPlayerStat;
    private Vector2 ForceInput;

    const float reflectForceScholar = 1000f;
    private float damage = 0f;
    const float damageStandard = 50f;
    
    private void Awake() {
        status = GetComponentInChildren<EnemyStatus>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyManager = GetComponent<EnemyManager>();
        EnemyBodyCollider = GetComponentInChildren<EnemyCollider>().gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            contactCollider = other.GetContact(0).collider;
            contactObject = contactCollider.transform.parent.transform.parent.gameObject;
            contactPlayerStat = contactObject.GetComponentInChildren<PlayerStatus>();

            if(other.otherCollider.tag == "Enemy Body")
            {
                if(contactCollider.tag == "Player Body")
                {
                    damage = 0f;
                }
                else if(contactCollider.tag == "Attack")
                {
                    damage = contactPlayerStat.Attack;
                    animator.SetTrigger("Stun");
                }

                ForceInput = other.GetContact(0).normal;
                DamageReDuce();
                Debug.Log("Enemy had damage : " + damage);
                Reflect(damage);
                status.HealthChange(damage);
            
            }
            else if(other.otherCollider.tag == "Attack")
            {
                if(contactCollider.tag == "Sheild")
                {
                    if(contactPlayerStat.parryFrame && !enemyManager.isParried)
                    {
                        enemyManager.TriggerEnemyParriedAnimation();
                    }
                }
                else if(contactCollider.tag == "Player Body")
                {

                }
            }

        }

    }

    public void StunStart()
    {
        enemyManager.isEnemyPaused = true;
        EnemyBodyCollider.enabled = false;
    }

    public void StunEnd()
    {
        enemyManager.isEnemyPaused = false;
        EnemyBodyCollider.enabled = true;
        if(enemyManager.isParried)
        {
            enemyManager.SetEnemyParried(false);
        }
    }

    private void Reflect(float Damage)
    {
        Damage = Mathf.Log(Damage / damageStandard + 1);

        rb.AddForce(ForceInput * reflectForceScholar * Damage);
    }

    private void DamageReDuce()
    {
        damage = damage * ( (float)(100 - status.ArmorDefence) / 100 );
    }

    public void ParreidStart()
    {
        enemyManager.SetEnemyParried(true);
    }

    public void ParriedEnd()
    {
        enemyManager.SetEnemyParried(false);
    }
}
