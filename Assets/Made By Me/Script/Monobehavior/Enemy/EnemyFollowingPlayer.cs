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

    BoxCollider2D ranDomRange;

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();
        randomTransform = transform.parent.GetChild(1).transform;
        ranDomRange = transform.parent.GetChild(2).GetComponent<BoxCollider2D>();
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
            int randomX; // Get random x coordinate
            int randomY; // Get random z coordinate

            randomX = Random.Range(0, enemyManager.currentMovingGraph.width);
            randomY = Random.Range(0, enemyManager.currentMovingGraph.depth); 
            node = enemyManager.currentMovingGraph.GetNode(randomX, randomY);

            int count = 0;
            while(!isInRange((Vector3)node.position))
            {
                randomX = Random.Range(0, enemyManager.currentMovingGraph.width);
                randomY = Random.Range(0, enemyManager.currentMovingGraph.depth); 
                node = enemyManager.currentMovingGraph.GetNode(randomX, randomY);
                count++;

                if(count > 100)
                {
                    Debug.Log("Cant find correct Node in RandomPostion");
                    return Vector3.zero;
                }
            }

            randomPoint = (Vector3)node.position;
        } while (!node.Walkable);

        return randomPoint;
    }

    public void setPlayerPosition()
    {
        enemyManager.aiPath.maxSpeed = enemyManager.finalChaseSpeed;
        enemyManager.destinationSetter.target = enemyManager.target;
    }

    private bool isInRange(Vector3 position)
    {
        if(ranDomRange == null) return true;

        if( ranDomRange.bounds.max.x >= position.x && 
            ranDomRange.bounds.min.x <= position.x && 
            ranDomRange.bounds.max.y >= position.y && 
            ranDomRange.bounds.min.y <= position.y )
            {
                return true;
            }

        return false;
    }

}


