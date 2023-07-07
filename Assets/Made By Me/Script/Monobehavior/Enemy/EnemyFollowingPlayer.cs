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

    private Transform randomTransform;

    public float chaseRange = 10f; // 쫓아갈 거리


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

        if(enemyManager.MoveStop || enemyManager.isEnemyPaused)
        {
            return;
        }

        Vector3 BetweenVector = new Vector3(target.position.x - transform.position.x,
             target.position.y - transform.position.y, 
             target.position.z - transform.position.z);

        Vector3 towardPlayerDirection =  BetweenVector.normalized;
        enemyManager.SetXYAnimation(towardPlayerDirection);

        float distanceToTarget = BetweenVector.magnitude;

        if (distanceToTarget < chaseRange && enemyManager.isLockedOnPlayer) 
        // 플레이어와의 거리가 chaseRange 미만이면 일단 추적 사정거리 안에 있음
        // 그 상태에서 지금 플레이어를 추적하고 있는 상황이어야지만 따라감
        {
            enemyManager.destinationSetter.target = target;
        }
        else // 추적 사정거리 안에 있지도 않음. 무조건 랜덤 이동
        {
            if(enemyManager.isLockedOnPlayer)
            {
                enemyManager.PlayerLockOn(false);
            }
            
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
        enemyManager.destinationSetter.target = randomTransform;
    }

}


