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

    public void Parreid()
    {
        if(enemyManager.isEnemyStunned) return;
        enemyManager.vfxAnimator.SetTrigger("Parried");
        StopAllCoroutines();

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
        if(enemyManager.isEnemyStunned) return;

        enemyManager.enemyStatus.ParriedWithParrySheild = false;
        enemyManager.isEnemyStunned = true;
        enemyManager.enemyBodyCollider.enabled = false;
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
        enemyManager.enemyBodyCollider.enabled = false;
        while(isNowTransforming)
        {
            switch(direction)
            {
                case 0 :
                    transform.Translate(new Vector3(0f, -enemyManager.attackSpeed, 0f));
                    break;
                case 1 :
                    transform.Translate(new Vector3(-enemyManager.attackSpeed, 0f, 0f));
                    break;
                case 2 :
                    transform.Translate(new Vector3(enemyManager.attackSpeed, 0f, 0f));
                    break;
                case 3 :
                    transform.Translate(new Vector3(0f, enemyManager.attackSpeed, 0f));
                    break;
            }
            
            yield return new WaitForSeconds(0.01f);
        }
        enemyManager.enemyBodyCollider.enabled = true;
    }




}
