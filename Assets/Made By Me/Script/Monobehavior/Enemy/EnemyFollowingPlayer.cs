using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class EnemyFollowingPlayer : MonoBehaviour
{
    GameObject player;

    public GameObject detectMark;
    public float detectTime = 0.5f;


    private EnemyManager enemyManager;

    private void Awake() {
        player = GameObject.FindObjectOfType<PlayerHealth>().gameObject;
        enemyManager = GetComponent<EnemyManager>();

        this.enabled = false;
    }

    private void OnEnable() {
        StartCoroutine(FollowingPlayerDirectly());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    IEnumerator FollowingPlayerDirectly()
    {
        float timeElapsed = 0f;
        detectMark.SetActive(true);
        while(timeElapsed < detectTime)
        {
            timeElapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        detectMark.SetActive(false);

        while(true)
        {
            if(enemyManager.MoveStop || enemyManager.isEnemyPaused)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }

            Vector3 BetweenVector = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z);
            float distance = BetweenVector.magnitude;

            if(distance > 10)
            {
                enemyManager.PlayerLockOn(false);
                yield break;
            } 

            Vector3 towardPlayerDirection =  BetweenVector.normalized;
            SetXYAnimation(towardPlayerDirection);
            float towardPlayerMagnitude = enemyManager.enemySpeed * Time.fixedDeltaTime;
            transform.Translate(towardPlayerDirection * towardPlayerMagnitude);
            yield return new WaitForFixedUpdate();
        }
        
    }

    private void SetXYAnimation(Vector3 towardPlayerDirection)
    {
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
    }
}


