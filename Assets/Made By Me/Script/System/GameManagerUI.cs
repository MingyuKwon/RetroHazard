using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    public static GameManagerUI instance = null;

    blackOut blackoutUI;
    DialogUI dialogUI;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        blackoutUI = FindObjectOfType<blackOut>();
        dialogUI = FindObjectOfType<DialogUI>();
    }

// BlackOut UI
    public void BlackOut(int index)
    {
        blackoutUI.BlackOut(index);
    }
// BlackOut UI


// Dialog UI
    public void VisualizeDialogUI(bool flag, bool isNPC)
    {
        dialogUI.VisualizeDialogUI(flag,isNPC);
    }



    //////////////////SetTExt/////////////////////////////////////
    public void SetDialogText(string text)
    {
        dialogUI.SetDialogText(text);
    }
    public void SetSpeakerText(string text)
    {
        dialogUI.SetSpeakerText(text);
    }
    public void SetInteractiveDialogText(string[] texts)
    {
        dialogUI.SetInteractiveDialogText(texts);
    }
    public void SetOptionsText(string[] texts)
    {
        dialogUI.SetOptionsText(texts);
    }
    //////////////////SetTExt///////////////////////////////////////



    public void showOptionUI(bool flag)
    {
        dialogUI.showOptionUI(flag);
    }
    public void showSpeakerPanelUI(bool flag)
    {
        dialogUI.SetSpeakerPanelUI(flag);
    }
    public void showDialogPanelUI(bool flag)
    {
        dialogUI.SetDialogPanelUI(flag);
    }



    /////////////////Override////////////////////////
    public void showInteractiveDialogPanelUI(bool flag)
    {
        dialogUI.showInteractiveDialogPanelUI(flag);
    }
    public void showInteractiveDialogPanelUI(bool flag, string ItemName)
    {
        dialogUI.showInteractiveDialogPanelUI(flag,ItemName);
    }
    /////////////////Override////////////////////////



    //////////////////////////////////////////////////
    public void showTalkNPCDialog(bool visited , Dialog dialog)
    {
        dialogUI.showTalkNPCDialog(visited,dialog);
    }
    //////////////////////////////////////////////////


    public void SelectOption(int index)
    {
        dialogUI.SelectOption(index);
    }

    // Dialog UI
}
