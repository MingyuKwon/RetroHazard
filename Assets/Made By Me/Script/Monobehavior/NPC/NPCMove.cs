using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCMove : MonoBehaviour
{
    Transform[] targets;
    Rigidbody2D NPCrigidbody2D;

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
    }

    private void Start() {
        transform.position = targets[0].position;
        currentTarget = 0;
        StartCoroutine(walkingCoroutine());
    }

    IEnumerator walkingCoroutine()
    {
        while(true)
        {
            setNextTarget();

            yield return new WaitForSeconds(1f);
            aiPath.canMove = true;

            while(!aiPath.reachedEndOfPath)
            {
                yield return new WaitForEndOfFrame();
                
            }

            aiPath.canMove = false;
        }
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
