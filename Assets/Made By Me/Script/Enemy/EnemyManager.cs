using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Animator animator;
    public bool isEnemyPaused = false;
    public bool isParried = false;
    public bool MoveStop = false;

    //Move
    public float enemySpeed = 3f;
    //Move

    // randomMove
    public Vector3 RandomStartPosition;
    // randomMove


    // animation
    public int animationX = 0;
    public int animationY = -1;
    // animation

    [SerializeField] Color normalColor;
    [SerializeField] Color parriedColor;

    EnemyStatus status;
    EnemyFollowingPlayer following;
    private Animator vfxAnimator;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        animator = GetComponent<Animator>();
        status = GetComponentInChildren<EnemyStatus>();
        following = GetComponent<EnemyFollowingPlayer>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        vfxAnimator = GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>();
    }

    public void TriggerEnemyParriedAnimation()
    {
        vfxAnimator.SetTrigger("Parried");
        animator.SetTrigger("Stagger");
    }

    public void SetEnemyParried(bool flag)
    {
        isParried = flag;
        isEnemyPaused = flag;
    }

    public void KillEnemy()
    {
        animator.SetTrigger("Die");
        vfxAnimator.SetTrigger("Die");
    }

    public void EnemyMoveStop(float delay)
    {
        if(delay < 0)
        {
            Debug.LogError("Delay time must be longer than 0");
            return;
        }
        EnemyMoveStopDirect(true);
        StartCoroutine(Delay(delay));
    }
    IEnumerator Delay(float delay)
    {   
        yield return new WaitForSeconds(delay);
        EnemyMoveStopDirect(false);
    }

    public void EnemyMoveStopDirect(bool flag)
    {
        MoveStop = flag;
        StopAllCoroutines();
    }

}
