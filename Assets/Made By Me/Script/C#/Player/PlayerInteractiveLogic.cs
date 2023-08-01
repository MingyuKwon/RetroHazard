using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerInteractiveLogic
{
    private NPCDialogScript nearNPC = null;
    public Interact nearInteract = null;
    PlayerInteractive monoBehaviour;

    public PlayerInteractiveLogic(PlayerInteractive monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
    }
    public void triggerStay2D(Collider2D other)
    {
        if(other.tag == "NPC" )
        {
            nearNPC = other.GetComponent<NPCDialogScript>();
            GameManager.isPlayerNearNPC = true;
        }else if(other.tag == "Interact")
        {
            nearInteract = other.GetComponent<Interact>();
        }
    }

    public void triggerExit2D(Collider2D other){
        if(other.tag == "NPC" )
        {
            GameManager.isPlayerNearNPC = false;
        }else if(other.tag == "Interact")
        {
            nearInteract = null;
        }
    }

    public void OnEnable() {
        GameMangerInput.InputEvent.InteractiveJustPressed += InteractivePressed;
        GameMangerInput.InputEvent.InteractiveJustReleased += InteractiveReleased;
    }

    public void OnDisable() {
        GameMangerInput.InputEvent.InteractiveJustPressed -= InteractivePressed;
        GameMangerInput.InputEvent.InteractiveJustReleased -= InteractiveReleased;
    }


    // Normal Input event
    private void InteractivePressed()
    {
        Debug.Log("isPlayerNearWarp : " +  warp.isPlayerNearWarp);
        if(warp.isPlayerNearWarp)
        {
            GameManager.EventManager.Invoke_WarpEvent();
             return;
        }
                
        if(GameManager.isPlayerNearStageWarp && !GameManager.isPlayerSheilding)
        {
            GameManager.EventManager.Invoke_InStageWarpEvent();
        }
        else if(GameManager.isPlayerNearNPC && !GameManager.isPlayerSheilding)
        {
            GameManager.instance.ResetPlayerAnimationState();
            nearNPC.showDialog();
        }else if(nearInteract != null && !GameManager.isPlayerSheilding)
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
                    if(nearInteract is HiddenInteract)
                    {
                        ((HiddenInteract)nearInteract).Hidden_Interact();
                    }else if(nearInteract is OneWayDoor)
                    {
                        ((OneWayDoor)nearInteract).OneWayDoorInteract(monoBehaviour.transform);
                    }else if(nearInteract is RealInteract)
                    {
                        GameManagerUI.instance.Visualize_Tab_Interactive(true, nearInteract.dialog);
                        ((RealInteract)nearInteract).InteractiveWithReal();
                    }
                }
                
            }
            
        }
    }
    private void InteractiveReleased()
    {

    }
// Normal Input event
}
