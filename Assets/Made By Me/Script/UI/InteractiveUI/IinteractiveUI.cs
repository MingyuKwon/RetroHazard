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

    public void EnterPressed(InputActionEventData data)
    {
        if(!inputOk) return;
        callCount++;
    }

    public void SetInteractiveUI(bool flag)
    {
        interactiveDialogPanel.SetActive(flag);
        inputOk = flag;
        if(!flag)
        {
            interact_Input_Rlease_Event?.Invoke();
            gameObject.SetActive(false);
        }
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
        SetInteractiveUI(flag);
        
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

        SetInteractiveUI(flag);
        
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
