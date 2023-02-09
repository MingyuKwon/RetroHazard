using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;

public class GameManagerUI : MonoBehaviour
{
    public static GameManagerUI instance = null;

    blackOut blackoutUI;
    DialogUI dialogUI;
    InGameUI inGameUI;
    TabUI tabUI;

    public bool isShowingTab;
    public bool isShowingMenu;

    public int CurrentContainer;

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
        tabUI = FindObjectOfType<TabUI>();
        inGameUI = FindObjectOfType<InGameUI>();
    }

    private void Start() {
        dialogUI.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(true);
        tabUI.gameObject.SetActive(false);
        blackoutUI.gameObject.SetActive(false);
    }

// BlackOut UI
    public void BlackOut(int index)
    {
        blackoutUI.BlackOut(index);
    }
// BlackOut UI

// Tab UI

///////////////// override start ////////////////////
[Button]
    public void Visualize_Tab_Interactive(bool flag, InteractiveDialog dialog)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Interactive(flag, dialog);
        isShowingTab = flag;
    }

    public void Visualize_Tab_Interactive(bool flag)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Interactive(flag);
        isShowingTab = flag;
    }
///////////////// override end ////////////////////

[Button]
    public void Visualize_Tab_Menu(bool flag)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(true);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Menu(flag);
        isShowingTab = flag;
    }

    ///////// override start //////////////////////
    public void Visualize_Tab_Obtain(bool flag)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Obtain(flag);
        isShowingTab = flag;
    }

    public void Visualize_Tab_Obtain(bool flag, bulletItem item)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Obtain(flag , item);
        isShowingTab = flag;
    }

    public void Visualize_Tab_Obtain(bool flag, ExpansionItem item)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Obtain(flag , item);
        isShowingTab = flag;
    }
    ///////// override end //////////////////////
// Tab UI

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
