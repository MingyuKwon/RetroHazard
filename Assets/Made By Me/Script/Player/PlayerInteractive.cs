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

        player.AddInputEventDelegate(EnterPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Enter");
        player.AddInputEventDelegate(UpPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectUp");
        player.AddInputEventDelegate(DownPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectDown");
        player.AddInputEventDelegate(RightPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectRight");
        player.AddInputEventDelegate(LeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectLeft");

    }

    private void Update() {
        if(GameManager.instance.isPlayerPaused) return;
    }
// Normal Input event
    private void InteractivePressed(InputActionEventData data)
    {
        if(GameManager.instance.isPlayerNearNPC)
        {
            GameMangerInput.instance.changePlayerInputRule(1);
            nearNPC.EnterPressed();
        }
    }
    private void InteractiveReleased(InputActionEventData data)
    {

    }
// Normal Input event

// Talk NPC Input event
    private void EnterPressed(InputActionEventData data)
    {
        nearNPC.EnterPressed();
    }
    private void UpPressed(InputActionEventData data)
    {
        nearNPC.UpPressed();
    }
    private void DownPressed(InputActionEventData data)
    {
        nearNPC.DownPressed();
    }
    private void RightPressed(InputActionEventData data)
    {
        nearNPC.RightPressed();
    }
    private void LeftPressed(InputActionEventData data)
    {
        nearNPC.LeftPressed();
    }
// Talk NPC Input event

    private void OnDestroy() {
        player.RemoveInputEventDelegate(InteractivePressed);
        player.RemoveInputEventDelegate(InteractiveReleased);
    }



}
