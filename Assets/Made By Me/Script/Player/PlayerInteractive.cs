using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerInteractive : MonoBehaviour
{
    private Player player;
    private NPCDialogScript nearNPC = null;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "NPC" )
        {
            nearNPC = other.GetComponent<NPCDialogScript>();
            GameManager.instance.isPlayerNearNPC = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "NPC" )
        {
            GameManager.instance.isPlayerNearNPC = false;
        }
    }

    private void Awake() {
        player = ReInput.players.GetPlayer(0);

        player.AddInputEventDelegate(InteractivePressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Interactive");
        player.AddInputEventDelegate(InteractiveReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "Interactive");

    }

    private void Update() {
        if(GameManager.instance.isPlayerPaused) return;
    }
    
// Normal Input event
    private void InteractivePressed(InputActionEventData data)
    {
        if(GameManager.instance.isPlayerNearNPC && !GameManager.instance.isPlayerSheilding)
        {
            GameMangerInput.instance.changePlayerInputRule(1);
            GameManager.instance.SetPlayerAnimationIdle();
            nearNPC.showDialog();
        }
    }
    private void InteractiveReleased(InputActionEventData data)
    {

    }
// Normal Input event

    private void OnDestroy() {
        player.RemoveInputEventDelegate(InteractivePressed);
        player.RemoveInputEventDelegate(InteractiveReleased);
    }



}
