using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private EnemyManager enemyManager;
    EnemyStatus status;

    private PolygonCollider2D PlayerDetect;
    private BoxCollider2D AttackDecide;

    private void Awake() {
        status = GetComponentInChildren<EnemyStatus>();
        enemyManager = GetComponent<EnemyManager>();

        PlayerDetect = transform.GetChild(1).GetComponentInChildren<PolygonCollider2D>();
        AttackDecide = transform.GetChild(1).GetComponentInChildren<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player not Body"))
        {
            // tirgger에서 어떤 콜라이더와 닿았는지 알 길이 없으니 차라리 각 상태에 따라 활성화 되어 있는 콜라이더가 하나뿐이게 해서
            // 그냥 하나의 Ontrigger Enter로 처리를 하자.
            // 일단 처음에는 detect만 가지고 있다가 dtect 성공시 광범위 하게 콜라이더가 변하고, attack이 켜진다
            // attack은 무조건 광범위 범위 안에 들어가있으므로, 최소한 범위 안에 있는 동안 새로운  trigger enter은 무조건 공격이다.
        }
    }


}
