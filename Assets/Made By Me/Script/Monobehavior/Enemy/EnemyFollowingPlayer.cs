using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;
using Pathfinding;


public class EnemyFollowingPlayer : MonoBehaviour
{
    private Vector3 randomPosition; // 랜덤한 위치

    public Transform randomTransform;

    public GameObject detectMark;

    private EnemyManager enemyManager;

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();
        randomTransform = transform.parent.GetChild(1).transform;
    }

    private void Start() {
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
            if(enemyManager.target.position.x > enemyManager.maxBoundX || 
            enemyManager.target.position.x < enemyManager.minBoundX || 
            enemyManager.target.position.y > enemyManager.maxBoundY || 
            enemyManager.target.position.y < enemyManager.minBoundY)
            {
                enemyManager.isLockedOnPlayer = false;
            }
        }
    }

    public void setRandomPosition()
    {
        randomTransform.position = GetRandomPointInSpecificGraph();
        enemyManager.aiPath.maxSpeed = enemyManager.finalRandomSpeed;
        enemyManager.destinationSetter.target = randomTransform;
    }

    Vector3 GetRandomPointInSpecificGraph()
    {
        GraphNode node;
        Vector3 randomPoint;

    // Keep trying until a walkable node is found
        do
        {
            int randomX = Random.Range(0, enemyManager.currentMovingGraph.width); // Get random x coordinate
            int randomZ = Random.Range(0, enemyManager.currentMovingGraph.depth); // Get random z coordinate
            node = enemyManager.currentMovingGraph.GetNode(randomX, randomZ);
            randomPoint = (Vector3)node.position;
        } while (!node.Walkable);

        return randomPoint;
    }

    public void setPlayerPosition()
    {
        enemyManager.aiPath.maxSpeed = enemyManager.finalChaseSpeed;
        enemyManager.destinationSetter.target = enemyManager.target;
    }

}


