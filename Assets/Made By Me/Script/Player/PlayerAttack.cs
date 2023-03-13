using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour
{
    private Player player;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        player.AddInputEventDelegate(OnAttack, UpdateLoopType.Update, InputActionEventType.ButtonPressed , "Attack");
    }

    private void OnDestroy() {
        player.RemoveInputEventDelegate(OnAttack);
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isPlayerPaused) return;
    }

    private void OnAttack(InputActionEventData data)
    {
        
    }


}
