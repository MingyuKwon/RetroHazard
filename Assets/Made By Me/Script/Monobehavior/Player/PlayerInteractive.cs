using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

// ================ code cleaned by making PlayerInteractiveLogic ==============================

public class PlayerInteractive : MonoBehaviour
{
    private Player player;

    private PlayerInteractiveLogic playerInteractiveLogic;

    void OnTriggerStay2D(Collider2D other) {
        playerInteractiveLogic.triggerStay2D(other);
    }

    void OnTriggerExit2D(Collider2D other) {
        playerInteractiveLogic.triggerExit2D(other);
    }

    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        playerInteractiveLogic = new PlayerInteractiveLogic(player);

        playerInteractiveLogic.delegateInpuiFunctions();
    }

    private void OnDestroy() {
        playerInteractiveLogic.removeInpuiFunctions();
    }

}
