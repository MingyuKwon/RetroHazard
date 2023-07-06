using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private EnemyManager enemyManager;
    private float AttackKind;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyManager = GetComponent<EnemyManager>();
    }

    private void Start() {
    
    }


    private void Update() {
        if(enemyManager.isEnemyPaused) return;
        SetXYAnimation();
        SetWalkAnimation();
    }

    private void SetXYAnimation()
    {
        animator.SetFloat("X", enemyManager.animationX);
        animator.SetFloat("Y", enemyManager.animationY);
    }

    private void SetWalkAnimation()
    {
        animator.SetBool("Walk", true);
    }

    private void SetAttackAnimation()
    {
        
    }

    public void AttackStart()
    {
        enemyManager.EnemyMoveStopDirect(true);
    }

    public void AttackEnd()
    {
        enemyManager.EnemyMoveStopDirect(false);
    }

    public void PlayerLockOn(bool flag)
    {
        if(!flag) return;

        enemyManager.enemyFollowingPlayer.detectMark.SetActive(true);
        StartCoroutine(EnemyIdleTime());
        animator.Play("Idle");
    }

    IEnumerator EnemyIdleTime()
    {
        if(!isCurrentAnimationEnd())
        {
            yield return null;
        }
        enemyManager.enemyFollowingPlayer.detectMark.SetActive(false);
        // 여기에 달리는 애니매이션 시작
    }

    public bool isCurrentAnimationEnd()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
    }


}
