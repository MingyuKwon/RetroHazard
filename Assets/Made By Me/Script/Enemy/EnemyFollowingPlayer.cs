using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class EnemyFollowingPlayer : MonoBehaviour
{
    GameObject player;

    [SerializeField] [InfoBox("speed is in inverse proportion with this value")] float enemySpeed = 1;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.DOMove(player.transform.position,enemySpeed);
    }
}
