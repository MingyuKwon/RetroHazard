using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyManager : MonoBehaviour
{
    [Header("Used in randomMove graph Finding")]
   public int graphNum = 0;

    private Animator animator;
    public bool isEnemyStunned{
        get{
            return isEnemyPaused;
        }
        set{
            aiPath.canMove = !value;
            isEnemyPaused = value;
        }
    }
    public bool isEnemyPaused = false;
    public bool isParried = false;
    public bool canMove{
        get{
            return aiPath.canMove;
        }

        set{
            if(!isEnemyStunned)
            {
                aiPath.canMove = value;
            }
        }
    }

    private bool _isLockedOnPlayer = false;
    public bool isLockedOnPlayer{
        get{
            return _isLockedOnPlayer;
        }

        set{
            _isLockedOnPlayer = value;
            if(_isLockedOnPlayer)
            {
                enemyAnimation.animator.speed = 1.2f;
                enemyFollowingPlayer.setPlayerPosition();
            }else
            {
                enemyAnimation.animator.speed = 1.0f;
                enemyFollowingPlayer.setRandomPosition();
            }

            PlayerDetect.enabled = !_isLockedOnPlayer;
            AttackDecide.enabled = _isLockedOnPlayer;

            enemyAnimation.PlayerLockOn(_isLockedOnPlayer);

        }
    }

    public bool lockOnMutex = false;


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

    public AIPath aiPath; // AIPath 컴포넌트
    public AIDestinationSetter destinationSetter; // AIDestinationSetter 컴포넌트
    public Seeker seeker;


    public Animator vfxAnimator;
    private SpriteRenderer spriteRenderer;

    public Rigidbody2D enemyRigidbody2D;

    public PolygonCollider2D PlayerDetect;
    public BoxCollider2D AttackDecide;



    [Header("Following Move")]
    public float randomSpeed = 2f; // 방황할 속도
    public float chaseSpeed = 4f; // 쫓아갈 속도

    [Header("Animation")]
    public float attackSpeed = 1.8f; // 몸통박치기 시에 순간 대시할 속도

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

        enemyRigidbody2D = GetComponent<Rigidbody2D>();

        enemyFollowingPlayer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            if(CheckObstacleBetween(other.transform.position - transform.position)) return;

            if(!isLockedOnPlayer) // 플레이어에게 록온 되어 있는데 trigger 닿았다면 그건 공격 신호
            {
                isLockedOnPlayer = true;

            }else // 플레이어에게 록온 안되었는데 trigger 닿았다면 그건 탐색 완료 신호
            {
                enemyAnimation.Attack();
            }
        }
    }

    private bool CheckObstacleBetween(Vector2 rayDirection)
    {
        int targetMask = 1 << LayerMask.NameToLayer("Player Body");

        int obstacleMask = 1 << LayerMask.NameToLayer("Environment");

        float checkDistance = 10;
        // Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, checkDistance, obstacleMask | targetMask);

        // hit.collider 가 null 이 아니라면, 무언가에 부딪혔다는 의미입니다.
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Player Body"))
            {
                return false;
            }else if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Environment"))
            {
                return true;
            }
        }
        return false;
    }

    private void Update() {
        SetXYAnimation(aiPath.velocity.normalized);
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

    public void KillEnemy()
    {
        animator.SetTrigger("Die");
        vfxAnimator.SetTrigger("Die");
    }

    public void DamageAndStop(float damageAndStopDelay)
    {
        StartCoroutine(DamageAndStopDelay(damageAndStopDelay));
    }

    IEnumerator DamageAndStopDelay(float damageAndStopDelay)
    {
        isEnemyStunned = true;
        yield return new WaitForSeconds(damageAndStopDelay);
        isEnemyStunned = false;
    }

}
