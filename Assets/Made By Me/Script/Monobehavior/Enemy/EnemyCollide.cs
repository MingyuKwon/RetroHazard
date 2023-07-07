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

    const float reflectForceScholar = 1200f;
    const float damageAndStopDelay = 0.7f;
    private float damage = 0f;
    const float damageStandard = 20f;
    
    private void Awake() {
        status = GetComponentInChildren<EnemyStatus>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyManager = GetComponent<EnemyManager>();
        EnemyBodyCollider = GetComponentInChildren<EnemyCollider>().gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        contactCollider = other.GetContact(0).collider;
        
        if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Player not Body"))
        {
            contactObject = contactCollider.transform.parent.transform.parent.gameObject;
            contactPlayerStat = contactObject.GetComponentInChildren<PlayerStatus>();

            if(other.otherCollider.tag == "Enemy Body" && contactCollider.tag != "Sheild")
            {
                if(contactCollider.tag == "Attack")
                {
                    damage = contactPlayerStat.Attack;
                    animator.SetTrigger("Stun");
                }

                ForceInput = other.GetContact(0).normal;
                DamageReDuce();
                damage = damage * (float)(status.ParriedWithParrySheild ? 1.5 : 1);
                Debug.Log("Enemy had damage : " + damage);
                Reflect(damage);
                status.HealthChange(damage);
                damage = 0;
            
            }
            else if(other.otherCollider.tag == "Attack")
            {
                if(contactCollider.tag == "Sheild")
                {
                    if(contactPlayerStat.parryFrame && !enemyManager.isParried)
                    {
                        if(contactPlayerStat.Sheild == 1) status.ParriedWithParrySheild = true;
                        enemyManager.TriggerEnemyParriedAnimation();
                    }
                }
            }
        }else if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            if(other.otherCollider.tag == "Enemy Body")
            {
                StartCoroutine(DamageAndStopDelay());
            }
        }
    }

    IEnumerator DamageAndStopDelay()
    {
        enemyManager.canMove = false;
        yield return new WaitForSeconds(damageAndStopDelay);
        enemyManager.canMove = true;
    }

    public void StunStart()
    {
        status.ParriedWithParrySheild = false;
        enemyManager.canMove = false;
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
        status.ParriedWithParrySheild = false;
        enemyManager.SetEnemyParried(false);
    }
}
