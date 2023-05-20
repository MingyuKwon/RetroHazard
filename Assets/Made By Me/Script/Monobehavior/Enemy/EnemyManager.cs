using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Animator animator;
    public bool isEnemyPaused = false;
    public bool isParried = false;
    public bool MoveStop = false;

    public bool isLockedOnPlayer = false;

    //Move
    public float enemySpeed = 3f;
    //Move

    // randomMove
    public Vector3 RandomStartPosition;


    // animation
    public int animationX = 0;
    public int animationY = -1;
    // animation

    [SerializeField] Color normalColor;
    [SerializeField] Color parriedColor;

    EnemyStatus status;
    EnemyTrigger trigger;
    EnemyFollowingPlayer following;
    EnemyRandomMove randomMove;
    private Animator vfxAnimator;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        animator = GetComponent<Animator>();
        status = GetComponentInChildren<EnemyStatus>();

        following = GetComponent<EnemyFollowingPlayer>();
        randomMove = GetComponent<EnemyRandomMove>();

        trigger = GetComponentInChildren<EnemyTrigger>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        vfxAnimator = GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>();
    }

    private void Update() {
        CheckRotate();
    }

    private void CheckRotate()
    {
        if(animationX == 0)
        {
            if(animationY == 1)
            {
                transform.GetChild(1).transform.rotation = Quaternion.Euler(new Vector3 (0, 0, 180));
            }else if(animationY == -1)
            {
                transform.GetChild(1).gameObject.transform.rotation = Quaternion.Euler(new Vector3 (0, 0, 0));
            }
        }else if(animationY == 0)
        {
            if(animationX == 1)
            {
                transform.GetChild(1).gameObject.transform.rotation = Quaternion.Euler(new Vector3 (0, 0, 90));
            }else if(animationX == -1)
            {
                transform.GetChild(1).gameObject.transform.rotation = Quaternion.Euler(new Vector3 (0, 0, 270));
            }
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
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
        EnemyMoveStopDirect(flag);
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

    public void PlayerLockOn(bool flag)
    {
        isLockedOnPlayer = flag;

        following.enabled = flag;
        randomMove.enabled = !flag;

        trigger.PlayerDetect.enabled = !flag;
        trigger.AttackDecide.enabled = flag;
    }

}
