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


    private EnemyManager enemyManager;

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();

        target = Player1.instance.playerStatus.transform;
        randomTransform = transform.parent.GetChild(1).transform;

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
            float distance = Vector3.Distance(transform.position, target.position);
            if(distance > enemyManager.chaseRange)
            {
                enemyManager.isLockedOnPlayer = false;
            }
        }
    }

    public void setRandomPosition()
    {
        float randomX = Random.Range(enemyManager.enemyMoveBoundMin.x, enemyManager.enemyMoveBoundMax.x);
        float randomY = Random.Range(enemyManager.enemyMoveBoundMin.y, enemyManager.enemyMoveBoundMax.y);
        randomPosition = new Vector2(randomX, randomY);

        randomTransform.position = randomPosition;
        enemyManager.aiPath.maxSpeed = enemyManager.randomSpeed;
        enemyManager.destinationSetter.target = randomTransform;
    }

    public void setPlayerPosition()
    {
        enemyManager.aiPath.maxSpeed = enemyManager.chaseSpeed;
        enemyManager.destinationSetter.target = target;
    }

}


