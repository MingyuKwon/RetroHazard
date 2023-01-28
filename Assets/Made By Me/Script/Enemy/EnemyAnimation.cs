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
        StartCoroutine(AttackRepeating());
    }


    private void Update() {
        
        
    }

    IEnumerator AttackRepeating()
    {
        while(true)
        {
            if(enemyManager.isEnemyPaused) yield return new WaitForEndOfFrame();

            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(2f);
        }
        
    }

    private void SetWalkAnimation()
    {

    }

    private void SetAttackAnimation()
    {
        
    }

}
