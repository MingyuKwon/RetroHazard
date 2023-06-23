using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

// ================ code cleaned by making PlayerHealthLogic ==============================
// 맣은 health 인데, 사실 상 충돌 및 스턴을 담당한다

// Health가 player를 singleTon으로 만들고 있다
// 오직 Health 에서만 rigidbody에 접근할 수 있다
public class PlayerHealth : MonoBehaviour
{
    Rigidbody2D rb;
    
    private CapsuleCollider2D playerBodyCollider;
    private PlayerHealthLogic playerHealthLogic;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerBodyCollider = GetComponentInChildren<PlayerCollider>().gameObject.GetComponent<CapsuleCollider2D>();

        playerHealthLogic = new PlayerHealthLogic(rb, Player1.instance.playerAnimation, playerBodyCollider);
    }

    private void Start() {
        playerHealthLogic.Start();
    }

    private void OnEnable() {
        PauseMainUI.GotoMainMenuEvent += DestroyMyself;
    }

    private void OnDisable() {
        PauseMainUI.GotoMainMenuEvent -= DestroyMyself;
    }

    private void DestroyMyself()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        float damage = playerHealthLogic.playerHealthCollisionEnter(other);
        
    }

    // 구조가 맞으면 stun 애니매이션을 일으키고, 그 일으킨 애니매이션이 처음 이벤트로 이 함수를 실행 시키게 되어 있다
    public void StunStart()
    {
        playerHealthLogic.StunStart();
    }

    public void StunEnd()
    {
        playerHealthLogic.StunEnd();
    }

}
