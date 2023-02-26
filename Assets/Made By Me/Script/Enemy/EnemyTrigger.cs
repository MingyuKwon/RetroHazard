using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private EnemyManager enemyManager;
    EnemyStatus status;

    public PolygonCollider2D PlayerDetect;
    public BoxCollider2D AttackDecide;

    private void Awake() {
        status = GetComponentInChildren<EnemyStatus>();
        enemyManager = GetComponent<EnemyManager>();

        PlayerDetect = transform.GetChild(1).GetComponentInChildren<PolygonCollider2D>();
        AttackDecide = transform.GetChild(1).GetComponentInChildren<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            if(enemyManager.isLockedOnPlayer)
            {
                enemyManager.Attack();
            }else
            {
                enemyManager.PlayerLockOn(true);
            }
        }
    }


}
