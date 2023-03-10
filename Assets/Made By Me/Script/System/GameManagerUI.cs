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
    BoxUI boxUI;
    IinteractiveUI interactiveUI;
    PauseRootUI PauseUI;

    public bool isDialogUIActive;
    public bool isTabUIActive;
    public bool isinGameUIActive;
    public bool isInteractiveUIActive;
    public bool isBoxUIActive;


    // it is for GameManager not in GameNanagerUI
    public bool isShowingTab;
    public bool isShowingMenu; 
    public bool isShowingBox;


    public bool isShowingESC;
    // it is for GameManager not in GameNanagerUI

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
        boxUI = FindObjectOfType<BoxUI>();
        inGameUI = FindObjectOfType<InGameUI>();
        interactiveUI = FindObjectOfType<IinteractiveUI>();
        PauseUI = FindObjectOfType<PauseRootUI>();
        
    }
    private void OnEnable() {
        PauseMainUI.GotoMainMenuEvent += DestroyMyself;
    }

    private void OnDisable() {
        PauseMainUI.GotoMainMenuEvent -= DestroyMyself;
    }

    private void DestroyMyself()
    {
        Destroy(this.gameObject);
    }



    private void Start() {
        dialogUI.gameObject.SetActive(false);
        tabUI.gameObject.SetActive(false);
        interactiveUI.gameObject.SetActive(false);
        blackoutUI.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(true);
        boxUI.gameObject.SetActive(false);
        PauseUI.gameObject.SetActive(false);

        dialogUI.VisualizeDialogUI(false);
        tabUI.Visualize_Tab_Interactive(false);
        interactiveUI.VisualizeInteractiveUI(false);
        boxUI.Visualize_BoxUI(false);

        Cursor.visible = false;
        
    }

    private void Update() {
        isDialogUIActive = dialogUI.gameObject.activeInHierarchy;
        isTabUIActive = tabUI.gameObject.activeInHierarchy;
        isBoxUIActive = boxUI.gameObject.activeInHierarchy;
        isinGameUIActive = inGameUI.gameObject.activeInHierarchy;
        isInteractiveUIActive = interactiveUI.gameObject.activeInHierarchy;
    }

    private void Visualize_PauseUI(bool flag)
    {
        PauseUI.gameObject.SetActive(flag);
        inGameUI.gameObject.SetActive(!flag);
    }

    public void Visualize_PauseMainUI(bool flag)
    {
        Visualize_PauseUI(flag);
        PauseUI.pauseMainUI.gameObject.SetActive(flag);
        PauseUI.saveSlotUI.gameObject.SetActive(!flag);
    }

    public void Visualize_SaveUI(bool flag)
    {
        Visualize_PauseUI(flag);
        PauseUI.saveSlotUI.gameObject.SetActive(flag);
        PauseUI.pauseMainUI.gameObject.SetActive(!flag);
    }

    public void Visualize_SaveUI(bool flag, bool isSave)
    {
        PauseUI.saveSlotUI.isSave = isSave;
        Visualize_SaveUI(flag);
    }

// BlackOut UI
    public void BlackOut(int index)
    {
        blackoutUI.BlackOut(index);
    }
    
    public void BlackOut(float speed)
    {
        blackoutUI.BlackOut(speed);
    }
// BlackOut UI

// Tab UI

///////////////// override start ////////////////////
    public void Visualize_Tab_Interactive(bool flag, InteractiveDialog dialog)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Interactive(flag, dialog);
        isShowingTab = flag;

        dialogUI.gameObject.SetActive(false);
        boxUI.gameObject.SetActive(false);
    }

    public void Visualize_Tab_Interactive(bool flag)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.Visualize_Tab_Interactive(flag);
        tabUI.gameObject.SetActive(flag);
        
        isShowingTab = flag;

        dialogUI.gameObject.SetActive(false);
        boxUI.gameObject.SetActive(false);
    }
///////////////// override end ////////////////////

    public void Visualize_Tab_Menu(bool flag)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(true);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Menu(flag);
        isShowingTab = flag;

        dialogUI.gameObject.SetActive(false);
        boxUI.gameObject.SetActive(false);
    }

    ///////// override start //////////////////////
    public void Visualize_Tab_Obtain(bool flag)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Obtain(flag);
        isShowingTab = flag;

        dialogUI.gameObject.SetActive(false);
        boxUI.gameObject.SetActive(false);
    }

    public void Visualize_Tab_Obtain(bool flag, bulletItem item)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Obtain(flag , item);
        isShowingTab = flag;

        dialogUI.gameObject.SetActive(false);
        boxUI.gameObject.SetActive(false);
    }

    public void Visualize_Tab_Obtain(bool flag, PotionItem item)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Obtain(flag , item);
        isShowingTab = flag;

        dialogUI.gameObject.SetActive(false);
        boxUI.gameObject.SetActive(false);
    }

    public void Visualize_Tab_Obtain(bool flag, KeyItem item)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        tabUI.Visualize_Tab_Obtain(flag , item);
        isShowingTab = flag;

        dialogUI.gameObject.SetActive(false);
        boxUI.gameObject.SetActive(false);
    }
    ///////// override end //////////////////////
// Tab UI

// BoxUI
    public void Visualize_BoxUI(bool flag)
    {
        if(isShowingTab && flag) return;
        
        boxUI.gameObject.SetActive(flag);
        boxUI.Visualize_BoxUI(flag);
        inGameUI.gameObject.SetActive(!flag);
        isShowingBox = flag;

        dialogUI.gameObject.SetActive(false);
        tabUI.gameObject.SetActive(false);
        
    }

// BoxUI

// Dialog UI
    public void VisualizeDialogUI(bool flag)
    {
        dialogUI.gameObject.SetActive(flag);
        dialogUI.VisualizeDialogUI(flag);
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
        interactiveUI.SetInteractiveDialogText(texts);
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
    public void VisualizeInteractiveUI(bool flag)
    {
        interactiveUI.gameObject.SetActive(flag);
        interactiveUI.VisualizeInteractiveUI(flag);
    }
    public void VisualizeInteractiveUI(bool flag, string ItemName)
    {
        interactiveUI.gameObject.SetActive(flag);
        interactiveUI.VisualizeInteractiveUI(flag,ItemName);
    }
    /////////////////Override////////////////////////



    //////////////////////////////////////////////////
    public void showTalkNPCDialog(bool visited , Dialog dialog)
    {
        dialogUI.gameObject.SetActive(true);
        dialogUI.showTalkNPCDialog(visited,dialog);
    }
    //////////////////////////////////////////////////


    public void SelectOption(int index)
    {
        dialogUI.SelectOption(index);
    }

    // Dialog UI
}
