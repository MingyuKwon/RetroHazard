using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;
using Pathfinding;


public class EnemyFollowingPlayer : MonoBehaviour
{
    private Transform target; // 플레이어의 위치
    private Vector3 randomPosition; // 랜덤한 위치

    public Transform randomTransform;

    public GameObject detectMark;

    GridGraph currentMovingGraph; // Assuming we are using a GridGraph

    float minBoundX;
    float minBoundY;

    float maxBoundX;
    float maxBoundY;



    private EnemyManager enemyManager;

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();

        target = Player1.instance.playerStatus.transform;
        randomTransform = transform.parent.GetChild(1).transform;
    }

    private void Start() {
        currentMovingGraph = AstarPath.active.data.graphs[enemyManager.graphNum] as GridGraph;

        minBoundX = currentMovingGraph.center.x - currentMovingGraph.size.x;
        minBoundY = currentMovingGraph.center.y - currentMovingGraph.size.y;

        maxBoundX = currentMovingGraph.center.x + currentMovingGraph.size.x;
        maxBoundY = currentMovingGraph.center.y + currentMovingGraph.size.y;

        setRandomPosition();
    }

    private void Update() { 
        if(!enemyManager.isLockedOnPlayer)
        {
            if(enemyManager.aiPath.reachedEndOfPath)
            {
                setRandomPosition();
            }  
        }else 
        {            
            if(transform.position.x > maxBoundX || 
            transform.position.x < minBoundX || 
            transform.position.y > maxBoundY || 
            transform.position.y < minBoundY)
            {
                Debug.Log("Out of Bound");
                enemyManager.isLockedOnPlayer = false;
            }
        }
    }

    public void setRandomPosition()
    {
        randomTransform.position = GetRandomPointInSpecificGraph();
        enemyManager.aiPath.maxSpeed = enemyManager.randomSpeed;
        enemyManager.destinationSetter.target = randomTransform;
    }

    Vector3 GetRandomPointInSpecificGraph()
    {
        GraphNode node;
        Vector3 randomPoint;

    // Keep trying until a walkable node is found
        do
        {
            int randomX = Random.Range(0, currentMovingGraph.width); // Get random x coordinate
            int randomZ = Random.Range(0, currentMovingGraph.depth); // Get random z coordinate
            node = currentMovingGraph.GetNode(randomX, randomZ);
            randomPoint = (Vector3)node.position;
        } while (!node.Walkable);

        return randomPoint;
    }

    public void setPlayerPosition()
    {
        enemyManager.aiPath.maxSpeed = enemyManager.chaseSpeed;
        enemyManager.destinationSetter.target = target;
    }

}


