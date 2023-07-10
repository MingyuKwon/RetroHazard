using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    private EnemyManager enemyManager;
    private float AttackKind;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyManager = GetComponent<EnemyManager>();
    }

    private void Start() {
        animator.Play("Walk");
    }

    private void Update() {
        if(enemyManager.isEnemyStunned) return;
        SetXYAnimation();
    }

    private void SetXYAnimation()
    {
        animator.SetFloat("X", enemyManager.animationX);
        animator.SetFloat("Y", enemyManager.animationY);
    }

    public void Animation_StopAllCoroutine()
    {
        StopAllCoroutines();
        enemyManager.enemyFollowingPlayer.detectMark.SetActive(false);
    }

    public void Parreid()
    {
        if(enemyManager.isEnemyStunned) return;
        enemyManager.vfxAnimator.SetTrigger("Parried");
        Animation_StopAllCoroutine();

        StartCoroutine(ParreidTime());
    }

    IEnumerator ParreidTime()
    {
        animator.Play("Stagger");

        enemyManager.isParried = true;
        enemyManager.isEnemyStunned = true;
        enemyManager.canMove = false;
        enemyManager.enemyBodyCollider.enabled = true;

        yield return new WaitForEndOfFrame();
        while(!isCurrentAnimationEnd())
        {
            yield return new WaitForEndOfFrame();
        }

        enemyManager.enemyStatus.ParriedWithParrySheild = false;
        enemyManager.isParried = false;
        enemyManager.isEnemyStunned = false;
        enemyManager.canMove = true;
        animator.Play("Walk");
    }

    public void Stun()
    {
        enemyManager.vfxAnimator.SetTrigger("Stun");
        enemyManager.enemyStatus.ParriedWithParrySheild = false;
        enemyManager.isEnemyStunned = true;
        enemyManager.enemyBodyCollider.enabled = false;
        Animation_StopAllCoroutine();

        StartCoroutine(EnemyStunTime());
    }

    IEnumerator EnemyStunTime()
    {
        animator.Play("Stun");
        enemyManager.canMove = false;
        yield return new WaitForEndOfFrame();
        while(!isCurrentAnimationEnd())
        {
            yield return new WaitForEndOfFrame();
        }
        enemyManager.canMove = true;
        enemyManager.isEnemyStunned = false;
        enemyManager.enemyBodyCollider.enabled = true;
        animator.Play("Walk");
    }

    public void Attack()
    {
        if(enemyManager.isEnemyStunned) return;
        StartCoroutine(EnemyAttackTime());
    }

    IEnumerator EnemyAttackTime()
    {
        animator.Play("Attack");
        enemyManager.canMove = false;
        yield return new WaitForEndOfFrame();
        while(!isCurrentAnimationEnd())
        {
            yield return new WaitForEndOfFrame();
        }
        enemyManager.canMove = true;
        enemyManager.enemyBodyCollider.enabled = true;
        animator.Play("Walk");
    }

    public void PlayerLockOn(bool flag)
    {
        if(!flag) return;
        if(enemyManager.isEnemyStunned) return;

        enemyManager.enemyFollowingPlayer.detectMark.SetActive(true);
        StartCoroutine(EnemyIdleTime());
    }

    IEnumerator EnemyIdleTime()
    {
        animator.Play("Idle");
        enemyManager.canMove = false;
        yield return new WaitForEndOfFrame();
        while(!isCurrentAnimationEnd())
        {
            yield return new WaitForEndOfFrame();
        }
        enemyManager.enemyFollowingPlayer.detectMark.SetActive(false);
        enemyManager.canMove = true;
        animator.Play("Walk");
    }

    public bool isCurrentAnimationEnd()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
    }



    ///////////////// For Animation Reference /////////////////////////////////////
    ///////////////// For Animation Reference /////////////////////////////////////
    ///////////////// For Animation Reference /////////////////////////////////////

    bool isNowTransforming = false;

    // Down left right up
    // 0     1    2     3
    public void Animation_BodyAttack(int direction) 
    {
        isNowTransforming = true;
        StartCoroutine(TransformMove(direction));
    }

    public void Animation_Stop_TransformMove()
    {
        isNowTransforming = false;
    }

    IEnumerator TransformMove(int direction)
    {
        Vector2 destinationPosition = transform.position;
        enemyManager.enemyBodyCollider.enabled = false;

        Vector2 ForceInput = Vector2.up;

            switch(direction)
            {
                case 0 :
                    ForceInput = Vector2.down;
                    break;
                case 1 :
                    ForceInput = Vector2.left;
                    break;
                case 2 :
                    ForceInput = Vector2.right;
                    break;
                case 3 :
                    ForceInput = Vector2.up;
                    break;
            }

        float frictionReduce = 1f;

        while(isNowTransforming)
        {
            float vectorC = 0;
            float temp = 0;
            if((temp = checkObstacleBehind(ForceInput)) > -1)
            {
                vectorC = Mathf.Min(ForceInput.magnitude * frictionReduce , temp);
            }else
            {
                vectorC = ForceInput.magnitude * frictionReduce;
            }
            enemyManager.enemyRigidbody2D.MovePosition((Vector2)transform.position + ForceInput.normalized * vectorC * enemyManager.attackSpeed );
            yield return new WaitForSeconds(0.02f);
            frictionReduce = Mathf.Lerp(frictionReduce , 0 , 0.3f);
        }
        enemyManager.enemyBodyCollider.enabled = true;
    }

    private float checkObstacleBehind(Vector2 forceInput)
    {
        int layerMaskNum = 1 << LayerMask.NameToLayer("Environment");

        float checkDistance = 50;
        // Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, forceInput, checkDistance, layerMaskNum);

        // hit.collider 가 null 이 아니라면, 무언가에 부딪혔다는 의미입니다.
        if (hit.collider != null)
        {
            return hit.distance;
        }
        return -1;
    }




}
