using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class EnemyFollowingPlayer : MonoBehaviour
{
    GameObject player;

    private EnemyManager enemyManager;

    float enemySpeed = 3f;

    private void Awake() {
        player = GameObject.FindObjectOfType<PlayerHealth>().gameObject;
        enemyManager = GetComponent<EnemyManager>();
    }

    void FixedUpdate()
    {
        if(enemyManager.MoveStop) return;
        if(enemyManager.isEnemyPaused) return;
        FollowingPlayerDirectly();
    }

    private void FollowingPlayerDirectly()
    {
        Vector3 towardPlayerDirection =  new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z).normalized;

        bool isXBig = Mathf.Abs(towardPlayerDirection.x) >= Mathf.Abs(towardPlayerDirection.y);

        if(isXBig)
        {
            if(towardPlayerDirection.x > 0)
            {
                enemyManager.animationX = 1;
            }else
            {
                enemyManager.animationX = -1;
            }
            enemyManager.animationY = 0;
        }else
        {
            if(towardPlayerDirection.y > 0)
            {
                enemyManager.animationY = 1;
            }else
            {
                enemyManager.animationY = -1;
            }
            enemyManager.animationX = 0;
        }
        
        float towardPlayerMagnitude = enemySpeed * Time.fixedDeltaTime;
        transform.Translate(towardPlayerDirection * towardPlayerMagnitude);
    }
}
