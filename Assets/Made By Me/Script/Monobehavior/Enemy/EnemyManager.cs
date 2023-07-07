using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


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

    // animation
    public int animationX = 0;
    public int animationY = -1;
    // animation

    [SerializeField] Color normalColor;
    [SerializeField] Color parriedColor;

    public EnemyStatus enemyStatus;
    public EnemyFollowingPlayer enemyFollowingPlayer;
    public EnemyAnimation enemyAnimation;
    
    public Vector2 enemyMoveBoundMin;
    public Vector2 enemyMoveBoundMax;

    public AIPath aiPath; // AIPath 컴포넌트
    public AIDestinationSetter destinationSetter; // AIDestinationSetter 컴포넌트
    public Seeker seeker;


    private Animator vfxAnimator;
    private SpriteRenderer spriteRenderer;

    public PolygonCollider2D PlayerDetect;
    public BoxCollider2D AttackDecide;



    // 이건 플레이어 발견했을 때 관련
    public float detectTime = 0.5f;
     // 이건 플레이어 발견했을 때 관련


    private void Awake() {
        animator = GetComponent<Animator>();
        enemyStatus = GetComponentInChildren<EnemyStatus>();

        enemyFollowingPlayer = GetComponent<EnemyFollowingPlayer>();

        enemyAnimation = GetComponent<EnemyAnimation>(); 

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        vfxAnimator = GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>();

        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        seeker = GetComponent<Seeker>();

        PlayerDetect = transform.GetChild(1).GetComponentInChildren<PolygonCollider2D>();
        AttackDecide = transform.GetChild(1).GetComponentInChildren<BoxCollider2D>();

        BoxCollider2D boxCollider2D = transform.GetChild(5).GetComponent<BoxCollider2D>();
        enemyMoveBoundMin = boxCollider2D.bounds.min;
        enemyMoveBoundMax = boxCollider2D.bounds.max;

        enemyFollowingPlayer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            if(isLockedOnPlayer)
            {
                Attack();
            }else
            {
                PlayerLockOn(true);
            }
        }
    }

    private void Update() {
        CheckRotate();
    }

    private void CheckRotate() // 현재 보고 있는 방향에 따라 콜라이더 등을 전체 돌려줌
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

    public void SetXYAnimation(Vector3 towardPlayerDirection)
    {
        bool isXBig = Mathf.Abs(towardPlayerDirection.x) >= Mathf.Abs(towardPlayerDirection.y);

        if(isXBig)
        {
            if(towardPlayerDirection.x > 0)
            {
                animationX = 1;
            }else
            {
                animationX = -1;
            }
            animationY = 0;
        }else
        {
            if(towardPlayerDirection.y > 0)
            {
                animationY = 1;
            }else
            {
                animationY = -1;
            }
            animationX = 0;
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

        // 플레이어는 이미 찾았으니 찾는 콜라이더 없애고 공격 할지 말지 정하는 콜라이더 활성화
        PlayerDetect.enabled = !flag;
        AttackDecide.enabled = flag;

        enemyAnimation.PlayerLockOn(flag);
    }

}
