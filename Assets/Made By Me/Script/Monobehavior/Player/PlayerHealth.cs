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
    private PlayerHealthLogic playerHealthLogic;

    private void Awake() {
        playerHealthLogic = new PlayerHealthLogic(this, Player1.instance.playerAnimation);
    }

    private void Start() {
        playerHealthLogic.Start();
    }

    private void OnEnable() {
        GameManager.EventManager.CloseGame_GotoMainMenuEvent += DestroyMyself;
    }

    private void OnDisable() {
        GameManager.EventManager.CloseGame_GotoMainMenuEvent -= DestroyMyself;
    }

    private void DestroyMyself()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        float damage = playerHealthLogic.playerHealthCollisionEnter(other);
        
    }

}
