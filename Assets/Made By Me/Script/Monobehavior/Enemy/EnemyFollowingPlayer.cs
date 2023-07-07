using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;
using Pathfinding;


public class EnemyFollowingPlayer : MonoBehaviour
{
    public Transform target; // 플레이어의 위치
    private Vector3 randomPosition; // 랜덤한 위치

    public Transform randomTransform;

    public GameObject detectMark;


    private EnemyManager enemyManager;

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();

        target = Player1.instance.playerStatus.transform;
        randomTransform = transform.parent.GetChild(1).transform;

        setRandomPosition();
        enemyManager.destinationSetter.target = randomTransform;
    }

    private void Update() { // 그냥 계속 사이 거리는 구한다

        if(!enemyManager.canMove || enemyManager.isEnemyPaused)
        {
            return;
        }

        if(enemyManager.isLockedOnPlayer)
        {
            if (enemyManager.aiPath.remainingDistance > enemyManager.chaseRange) 
            {
                enemyManager.PlayerLockOn(false);
                enemyManager.enemyAnimation.animator.speed = 1.0f;
                setRandomPosition();
            }else
            {
                enemyManager.aiPath.maxSpeed = enemyManager.chaseSpeed;
                enemyManager.enemyAnimation.animator.speed = 1.2f;
            }

        }else
        {
            enemyManager.enemyAnimation.animator.speed = 1.0f;
            if(enemyManager.aiPath.reachedEndOfPath)
            {
                setRandomPosition();
            }  
        }
    }

    private void setRandomPosition()
    {
        float randomX = Random.Range(enemyManager.enemyMoveBoundMin.x, enemyManager.enemyMoveBoundMax.x);
        float randomY = Random.Range(enemyManager.enemyMoveBoundMin.y, enemyManager.enemyMoveBoundMax.y);
        randomPosition = new Vector2(randomX, randomY);

        randomTransform.position = randomPosition;
        enemyManager.aiPath.maxSpeed = enemyManager.randomSpeed;
        enemyManager.destinationSetter.target = randomTransform;
    }

}


