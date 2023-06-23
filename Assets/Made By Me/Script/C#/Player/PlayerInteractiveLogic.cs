using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerInteractiveLogic
{
    private NPCDialogScript nearNPC = null;
    public Interact nearInteract = null;


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
        if(GameManager.isPlayerNearNPC && !GameManager.isPlayerSheilding)
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
                    GameManager.EventManager.InvokeInteractEvent();
                    GameManagerUI.instance.Visualize_Tab_Interactive(true, nearInteract.dialog);
                }
                
            }
            
        }
    }
    private void InteractiveReleased()
    {

    }
// Normal Input event
}
