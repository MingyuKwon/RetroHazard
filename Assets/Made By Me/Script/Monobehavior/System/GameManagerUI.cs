using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    public static int CurrentContainer;
    public static GameManagerUI instance = null;

    blackOut blackoutUI;
    DialogUI dialogUI;
    InGameUI inGameUI;
    TabUI tabUI;
    BoxUI boxUI;
    IinteractiveUI interactiveUI;
    PauseRootUI PauseUI;


    public bool isDialogUIActive{
        get
        {
            return dialogUI.gameObject.activeInHierarchy;
        }
    }
    public bool isTabUIActive{
        get
        {
            return tabUI.gameObject.activeInHierarchy;
        }
    }
    public bool isinGameUIActive{
        get
        {
            return inGameUI.gameObject.activeInHierarchy;
        }
    }
    public bool isInteractiveUIActive{
        get
        {
            return interactiveUI.gameObject.activeInHierarchy;
        }
    }
    public bool isBoxUIActive{
        get
        {
            return boxUI.gameObject.activeInHierarchy;
        }
    }


    // it is for GameManager not in GameNanagerUI
    public bool isShowingTab;
    public bool isShowingMenu; 
    public bool isShowingBox;


    public bool isShowingESC;
    // it is for GameManager not in GameNanagerUI

    
    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        }else
        {
            Destroy(this.gameObject);
        }

        blackoutUI = UI.instance.blackoutUI;
        dialogUI = UI.instance.dialogUI;
        tabUI = UI.instance.tabUI;
        boxUI = UI.instance.boxUI;
        inGameUI = UI.instance.inGameUI;
        interactiveUI = UI.instance.interactiveUI;
        PauseUI = UI.instance.PauseUI;
    }


    private void OnEnable() {
        GameManager.EventManager.CloseGame_GotoMainMenuEvent += DestroyMyself;
    }

    private void OnDisable() {
        GameManager.EventManager.CloseGame_GotoMainMenuEvent -= DestroyMyself;
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

    public void Visualize_InGameUI(bool flag)
    {
        inGameUI.gameObject.SetActive(flag);
    }

    private void Visualize_PauseUI(bool flag)
    {
        PauseUI.gameObject.SetActive(flag);
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

    public void Visualize_Tab_Interactive(bool flag, InteractiveDialog dialog = null)
    {
        if(isShowingTab && flag) return;
        inGameUI.gameObject.SetActive(!flag);
        tabUI.gameObject.SetActive(flag);
        if(dialog == null)
        {
            tabUI.Visualize_Tab_Interactive(flag);
        }else
        {
            tabUI.Visualize_Tab_Interactive(flag, dialog);
        }
        
        isShowingTab = flag;

        dialogUI.gameObject.SetActive(false);
        boxUI.gameObject.SetActive(false);
    }

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
        dialogUI.VisualizeDialogUI(flag);
    }

    public void showTalkNPCDialog(bool visited , Dialog dialog)
    {
        dialogUI.showTalkNPCDialog(visited,dialog);
    }

// Dialog UI

// Interactive UI
    public void SetInteractiveDialogText(string[] texts)
    {
        interactiveUI.SetInteractiveDialogText(texts);
    }

    public void VisualizeInteractiveUI(bool flag, string ItemName = "")
    {
        interactiveUI.gameObject.SetActive(flag);
        if(ItemName == "")
        {
            interactiveUI.VisualizeInteractiveUI(flag);
        }else
        {   
            interactiveUI.VisualizeInteractiveUI(flag,ItemName);
        }
        
    }

// Interactive UI
}
