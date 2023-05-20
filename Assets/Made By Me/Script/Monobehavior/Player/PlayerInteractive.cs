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
    public Interact nearInteract = null;

    void OnTriggerStay2D(Collider2D other) {

        if(other.tag == "NPC" )
        {
            nearNPC = other.GetComponent<NPCDialogScript>();
            GameManager.instance.isPlayerNearNPC = true;
        }else if(other.tag == "Interact")
        {
            nearInteract = other.GetComponent<Interact>();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "NPC" )
        {
            GameManager.instance.isPlayerNearNPC = false;
        }else if(other.tag == "Interact")
        {
            nearInteract = null;
        }
    }

    private void Awake() {
        player = ReInput.players.GetPlayer(0);

        player.AddInputEventDelegate(InteractivePressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Interactive");
        player.AddInputEventDelegate(InteractiveReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "Interactive");

    }

    private void OnDestroy() {
        player.RemoveInputEventDelegate(InteractivePressed);
        player.RemoveInputEventDelegate(InteractiveReleased);
    }

// Normal Input event
    private void InteractivePressed(InputActionEventData data)
    {
        if(GameManager.instance.isPlayerNearNPC && !GameManager.instance.isPlayerSheilding)
        {
            GameManager.instance.ResetPlayerAnimationState();
            nearNPC.showDialog();
        }else if(nearInteract != null && !GameManager.instance.isPlayerSheilding)
        {
            if(nearInteract.isItem)
            {
                nearInteract.ObtainItem();
            }else
            {   
                if(nearInteract.isInventoryBox)
                {
                    GameManagerUI.instance.Visualize_BoxUI(true);
                }else if(nearInteract.isSaveSpot)
                {
                    GameManagerUI.instance.Visualize_SaveUI(true, true);
                }else
                {
                    GameManager.EventManager.InvokeInteractEvent();
                    GameManagerUI.instance.Visualize_Tab_Interactive(true, nearInteract.dialog);
                }
                
            }
            
        }
    }
    private void InteractiveReleased(InputActionEventData data)
    {

    }
// Normal Input event

    



}
