using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

// ================ code cleaned by making PlayerInteractiveLogic ==============================

public class PlayerInteractive : MonoBehaviour
{
    private PlayerInteractiveLogic playerInteractiveLogic;

    void OnTriggerStay2D(Collider2D other) {
        playerInteractiveLogic.triggerStay2D(other);
    }

    void OnTriggerExit2D(Collider2D other) {
        playerInteractiveLogic.triggerExit2D(other);
    }

    private void Awake() {
        playerInteractiveLogic = new PlayerInteractiveLogic();
    }

    private void OnEnable() {
        playerInteractiveLogic.OnEnable();
    }

    private void OnDisable() {
        playerInteractiveLogic.OnDisable();
    }



}
