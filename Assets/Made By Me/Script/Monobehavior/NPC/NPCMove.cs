using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCMove : MonoBehaviour
{
    Transform[] targets;
    Rigidbody2D NPCrigidbody2D;
    Animator animator;

    public AIPath aiPath; // AIPath 컴포넌트
    public AIDestinationSetter destinationSetter; // AIDestinationSetter 컴포넌트
    public Seeker seeker;

    public int currentTarget = 0;
    int totalTargetCount;

    private void Awake() {
        totalTargetCount = transform.GetChild(1).childCount;
        targets = new Transform[totalTargetCount]; 

        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        seeker = GetComponent<Seeker>();

        for(int i=0; i<totalTargetCount; i++)
        {
            GameObject newGameObject = new GameObject();
            GraphNode closestNode = AstarPath.active.GetNearest(transform.GetChild(1).GetChild(i).position).node;
            newGameObject.transform.position = (Vector3)closestNode.position;

            targets[i] = newGameObject.transform;
        }

        NPCrigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        transform.position = targets[0].position;
        currentTarget = 0;
        StartCoroutine(walkingCoroutine());
    }

    private void OnEnable() {
        GameManager.EventManager.NPCWalkAgainEvent += WalkAgain;
    }

    private void OnDisable() {
        GameManager.EventManager.NPCWalkAgainEvent -= WalkAgain;
    }

    bool isFirst = true;
    IEnumerator walkingCoroutine()
    {
        while(true)
        {
            setNextTarget();

            if(isFirst)
            {
                isFirst = false;
            }else
            {
                yield return new WaitForSeconds(1f);
            }
            
            aiPath.canMove = true;

            animator.Play("Walk");

            while(!aiPath.reachedEndOfPath)
            {
                walkingAnimation();
                yield return null;
            }

            aiPath.canMove = false;
            animator.Play("Idle");
        }
    }

    private void WalkAgain()
    {
        if(aiPath.canMove && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.Play("Walk");
            idleFlag = false;
        }
    }

    private void walkingAnimation()
    {
        if(idleFlag) return;
        
        Vector2 playerDirection = destinationSetter.target.position - transform.position;

        int upAngle = (int)Vector2.Angle(Vector2.up, playerDirection);
        int rightAngle = (int)Vector2.Angle(Vector2.right, playerDirection);
        int leftAngle = (int)Vector2.Angle(Vector2.left, playerDirection);
        int downAngle = (int)Vector2.Angle(Vector2.down, playerDirection);

        if(upAngle <= 45)
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 1);
        }else if(rightAngle <= 45)
        {
            animator.SetFloat("X", 1);
            animator.SetFloat("Y", 0);
        }else if(leftAngle <= 45)
        {
            animator.SetFloat("X", -1);
            animator.SetFloat("Y", 0);
        }else if(downAngle <= 45)
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", -1);
        }

    }

    bool idleFlag = false;
    public void IdleAnimation()
    {
        idleFlag = true;
        Vector2 playerDirection = Player1.instance.playerMove.transform.position - transform.position;

        int upAngle = (int)Vector2.Angle(Vector2.up, playerDirection);
        int rightAngle = (int)Vector2.Angle(Vector2.right, playerDirection);
        int leftAngle = (int)Vector2.Angle(Vector2.left, playerDirection);
        int downAngle = (int)Vector2.Angle(Vector2.down, playerDirection);

        if(upAngle <= 45)
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 1);
        }else if(rightAngle <= 45)
        {
            animator.SetFloat("X", 1);
            animator.SetFloat("Y", 0);
        }else if(leftAngle <= 45)
        {
            animator.SetFloat("X", -1);
            animator.SetFloat("Y", 0);
        }else if(downAngle <= 45)
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", -1);
        }

        animator.Play("Idle");

    }

    private void setNextTarget()
    {
        int previousTarget = currentTarget;

        if(UnityEngine.Random.Range(0, 2) == 0)
        {
            currentTarget = (previousTarget -1 + totalTargetCount) % totalTargetCount;
        }else
        {
            currentTarget = (previousTarget + 1) % totalTargetCount;
        }

        destinationSetter.target = targets[currentTarget];
    }
}
