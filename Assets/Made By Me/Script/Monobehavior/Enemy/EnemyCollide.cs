using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollide : MonoBehaviour
{
    private EnemyManager enemyManager;

    private Collider2D contactCollider;
    private GameObject contactObject;
    private PlayerStatus contactPlayerStat;
    private Vector2 ForceInput;

    const float reflectForceScholar = 15f;
    const float damageAndStopDelay = 0.7f;
    private float damage = 0f;
    const float damageStandard = 20f;
    
    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        contactCollider = other.GetContact(0).collider;
        
        if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Player not Body")) // 만약 플레이어의 몸이 아닌 곳에 맞았다면
        {
            contactObject = contactCollider.transform.parent.transform.parent.gameObject;
            contactPlayerStat = contactObject.GetComponentInChildren<PlayerStatus>();

            if(other.otherCollider.tag == "Enemy Body" && contactCollider.tag != "Sheild") // 방패에 맞은게 아니라면? -> 공격에 맞은거
            {
                if(contactCollider.tag == "Attack") // 공격 맞은 경직
                {
                    damage = contactPlayerStat.Attack; 
                    enemyManager.enemyAnimation.Stun();
                }

                ForceInput = other.GetContact(0).normal;
                DamageReDuce();
                damage = damage * (float)(enemyManager.enemyStatus.ParriedWithParrySheild ? 1.5 : 1);
                Debug.Log("Enemy had damage : " + damage);
                Reflect(damage);
                enemyManager.enemyStatus.HealthChange(damage);
                damage = 0;
            
            }
            else if(other.otherCollider.tag == "Attack") // 만약 적이 공격 했는데
            {
                if(contactCollider.tag == "Sheild") // 실드로 막았다면
                {
                    if(contactPlayerStat.parryFrame && !enemyManager.isParried) // 그리고 그 실드가 패링중이라면
                    {
                        if(contactPlayerStat.Sheild == 1) enemyManager.enemyStatus.ParriedWithParrySheild = true;
                        enemyManager.enemyAnimation.Parreid();
                    }
                }
            }
        }else if(contactCollider.gameObject.layer == LayerMask.NameToLayer("Player Body")) // 만약 플레이어 몸이랑 맞았다면
        {
            if(other.otherCollider.tag == "Enemy Body") // 내 몸이랑 맞았다면
            {
                StartCoroutine(DamageAndStopDelay()); // 데미지 주고 잠시 대기
            }
        }
    }

    IEnumerator DamageAndStopDelay()
    {
        enemyManager.canMove = false;
        yield return new WaitForSeconds(damageAndStopDelay);
        enemyManager.canMove = true;
    }

    private void Reflect(float Damage)
    {
        Damage = Mathf.Log(Damage / damageStandard + 1);
        ForceInput = ForceInput * reflectForceScholar * Damage;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(ForceInput);
    }

    private void DamageReDuce()
    {
        damage = damage * ( (float)(100 - enemyManager.enemyStatus.ArmorDefence) / 100 );
    }

}
