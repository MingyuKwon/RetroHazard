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
    private PlayerAnimation playerAnimation;
    private PlayerStatus status;

    PlayerHealth monoBehaviour;


    private Vector2 ForceInput;
    const float reflectForceScholar = 500f;
    const float damageStandard = 10f;

    public PlayerHealthLogic(PlayerHealth monoBehaviour, PlayerAnimation playerAnimation)
    {
        this.monoBehaviour = monoBehaviour;
        this.rb = monoBehaviour.GetComponent<Rigidbody2D>();
        this.playerAnimation = playerAnimation;
        
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
                if(GameManager.isPlayerPaused) return 0;

                if(status.parrySuccess) return 0;

                GameObject contactObject = contactCollider.transform.parent.transform.parent.gameObject;
                EnemyStatus contactEnemyStat = contactObject.GetComponentInChildren<EnemyStatus>();

                if(contactCollider.tag == "Attack") // 몬스터가 공격 한 것을 맞았냐?
                {
                    damage = contactEnemyStat.Attack * contactEnemyStat.AttackDamageRatio;
                    ForceInput = other.GetContact(0).normal;
                    Debug.Log("player had damage : " + damage);
                    GameAudioManager.instance.PlaySFXMusic(SFXAudioType.Attacked);
                    Reflect(damage);
                    status.HealthChangeDefaultMinus(damage);

                    playerAnimation.StunAnimationStart();
                }

                
            }
        }

        return damage;
        // 반환 값으로 자기가 최종적으로 받은 데미지를 반환한다. 데미지가 0이면 피해를 안입은거고, 데미지에 따라 해야 할 일이 있다면 
        // 반환 값에 맞춰 하면 된다
    }   

    private void Reflect(float Damage)
    {
        Damage = Mathf.Log(Damage / damageStandard + 1);
        ForceInput = ForceInput * reflectForceScholar * Damage;
        rb.AddForce(ForceInput);
        monoBehaviour.StartCoroutine(reflectCoroutine(ForceInput));
    }

    IEnumerator reflectCoroutine(Vector2 ForceInput)
    {
        float timeElapsed = 0;
        while(timeElapsed < 0.5f)
        {
            float movableDistance = 0;
            checkObstacleBehind(ForceInput,out movableDistance);

            if(movableDistance <= 0.1f)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;   
            }
            
            yield return new WaitForFixedUpdate();
            timeElapsed += Time.fixedDeltaTime;
        }

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;   

    }

    private Vector2 checkObstacleBehind(Vector2 forceInput, out float movableDistance)
    {
        int layerMaskNum = 1 << LayerMask.NameToLayer("Environment");

        float checkDistance = 1;
        // Raycast
        RaycastHit2D hit = Physics2D.Raycast(monoBehaviour.transform.position, forceInput, checkDistance, layerMaskNum);
        // hit.collider 가 null 이 아니라면, 무언가에 부딪혔다는 의미입니다.
        if (hit.collider != null)
        {
            movableDistance = hit.distance;
            return hit.point;
        }

        movableDistance = 100; // 그냥 존나 크게 해서 선택 안되게
        return Vector2.zero;
    }
}
