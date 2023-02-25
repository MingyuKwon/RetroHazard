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

    private IEnumerator AttackRepeating()
    {
        while(true)
        {
            if(enemyManager.isEnemyPaused) yield return new WaitForEndOfFrame();

            animator.SetTrigger("Attack");
            
            yield return new WaitForSeconds(2f);
            
        }
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

}
