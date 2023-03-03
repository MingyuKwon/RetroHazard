using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;

public class IinteractiveUI : MonoBehaviour
{
    static public event Action interact_Input_Rlease_Event;

    public bool inputOk = false;

    public bool isCalledByKeyItem = false;

    private Player player;
    public int callCount = 0;
    GameObject interactiveDialogPanel;
    Text interactiveDialogText;
    string[] interactivedialogTexts;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);

        player.AddInputEventDelegate(EnterPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Enter");

        interactiveDialogPanel = GetComponentInChildren<InteractiveDialogPanel>().gameObject;
        interactiveDialogText = interactiveDialogPanel.gameObject.GetComponentInChildren<Text>();
    }

    private void OnEnable() {
        GameManagerUI.instance.isInteractiveUIActive = true;
        transform.parent.GetComponent<UI>().MouseCursor(true);
    }

    private void OnDisable() {
        GameManagerUI.instance.isInteractiveUIActive = false;
        transform.parent.GetComponent<UI>().MouseCursor(false);
        isCalledByKeyItem = false;
    }


    public void EnterPressed(InputActionEventData data)
    {
        if(this.gameObject.activeInHierarchy == false) return;
        if(!inputOk) return;
        callCount++;
        
    }

    public void SetInteractiveDialogText(string[] texts)
    {
        interactivedialogTexts = texts;
    }

    ///////////////////////////////override////////////////////////
    public void VisualizeInteractiveUI(bool flag)
    {
        if(flag)
        {
            StartCoroutine(InteractiveDialog());
        }else
        {
            callCount = 0;

        }
        interactiveDialogPanel.SetActive(flag);
        if(!flag)
        {
            interact_Input_Rlease_Event?.Invoke();
            gameObject.SetActive(false);
        }

        Interactive_ChangeInput_PauseGame(flag);
        
        
    }
    public void VisualizeInteractiveUI(bool flag, string ItemName)
    {        
        if(flag)
        {
            StartCoroutine(InteractiveDialog(ItemName));
        }else
        {
            callCount = 0;
        }

        interactiveDialogPanel.SetActive(flag);
        Interactive_ChangeInput_PauseGame(flag);
        
        if(!flag)
        {
            interact_Input_Rlease_Event?.Invoke();
            gameObject.SetActive(false);
        }
        
    }

    private void Interactive_ChangeInput_PauseGame(bool flag)
    {
        if(flag)
        {
            GameMangerInput.instance.changePlayerInputRule(1);
        }else
        {
            if(GameManagerUI.instance.isDialogUIActive || GameManagerUI.instance.isTabUIActive || GameManagerUI.instance.isBoxUIActive)
            {

            }else
            {
                GameMangerInput.instance.changePlayerInputRule(0);
            }
            
        }

        inputOk = flag;

        GameManager.instance.SetPauseGame(flag);
    }

    ///////////////////////////////override////////////////////////

    ///////////////////////////////override////////////////////////
    IEnumerator InteractiveDialog()
    {
        int strCount = interactivedialogTexts.Length;
        while(strCount > callCount)
        {
            interactiveDialogText.text = interactivedialogTexts[callCount];
            yield return new WaitForEndOfFrame();
        }
        VisualizeInteractiveUI(false);
    }

    IEnumerator InteractiveDialog(string ItemName)
    {
        int strCount = interactivedialogTexts.Length;
        while(callCount == 0)
        {
            interactiveDialogText.text = "You obtained <b><color=blue>\"" + ItemName + "\"</color></b> !";
            yield return new WaitForEndOfFrame();
        }

        callCount = 0;

        while(strCount > callCount)
        {
            interactiveDialogText.text = interactivedialogTexts[callCount];
            yield return new WaitForEndOfFrame();
        }

        VisualizeInteractiveUI(false);
        GameManager.instance.SetPlayerAnimationObtainKeyItem(false);

    }

    ///////////////////////////////override////////////////////////
}
