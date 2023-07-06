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

    public float chaseRange = 10f; // 쫓아갈 거리


    public GameObject detectMark;


    private EnemyManager enemyManager;

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();

        target = Player1.instance.playerStatus.transform;

        // 지금 Transform 하나만 계속 쫓을 거고, 그 transform의 위치를 바꿔가며 따라 갈거다.
        // 근데 Transform은 생성이 안되서 이미 있는 거 써야 하는데, status가 위치에 상관없기 떄문에 status의 transform을 썼다
        enemyManager.destinationSetter.target = enemyManager.enemyStatus.transform;
        setRandomPosition();
    }

    private void Update() { // 그냥 계속 사이 거리는 구한다

        Debug.Log("Target Position : " + enemyManager.destinationSetter.target.position + " randomPosition " + randomPosition);

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
            enemyManager.destinationSetter.target.position = target.position; // 목표 위치를 플레이어의 위치로 설정
        }
        else // 추적 사정거리 안에 있지도 않음. 무조건 랜덤 이동
        {
            if(enemyManager.isLockedOnPlayer)
            {
                enemyManager.PlayerLockOn(false);
            }
            
            
            Debug.Log("RandomMoving");
            enemyManager.destinationSetter.target.position = randomPosition; 
            
        }

    }

    private void setRandomPosition()
    {
        float randomX = Random.Range(enemyManager.enemyMoveBoundMin.x, enemyManager.enemyMoveBoundMax.x);
        float randomY = Random.Range(enemyManager.enemyMoveBoundMin.y, enemyManager.enemyMoveBoundMax.y);
        randomPosition = new Vector2(randomX, randomY);
    }

}


