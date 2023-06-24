using UnityEngine;

////////////////// Cleared //////////////////
public class UI : MonoBehaviour
{
    public static UI instance;

    public blackOut blackoutUI;
    public DialogUI dialogUI;
    public IinteractiveUI interactiveUI;
    public PauseRootUI PauseUI;

    public BoxUI boxUI;
    public InGameUI inGameUI;
    public TabUI tabUI;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        boxUI = GetComponentInChildren<BoxUI>();
        inGameUI = GetComponentInChildren<InGameUI>();
        tabUI = GetComponentInChildren<TabUI>();

        blackoutUI = GetComponentInChildren<blackOut>();
        dialogUI = GetComponentInChildren<DialogUI>();
        interactiveUI = GetComponentInChildren<IinteractiveUI>();
        PauseUI = GetComponentInChildren<PauseRootUI>();

    }

    public void SetMouseCursorActive(bool flag)
    {
        transform.GetChild(6).transform.GetChild(0).gameObject.SetActive(flag);
    }

    private void OnEnable() {
        SaveSystem.LoadEvent += TotalUIUpdate;
        PauseMainUI.GotoMainMenuEvent += DestroyMyself;
    }

    private void OnDisable() {
        SaveSystem.LoadEvent -= TotalUIUpdate;
        PauseMainUI.GotoMainMenuEvent -= DestroyMyself;
    }

    private void DestroyMyself()
    {
        Destroy(this.gameObject);
    }

    public void TotalUIUpdate()
    {
        if(boxUI.gameObject.activeInHierarchy)
        {
            boxUI.UpdateBoxUI();
        }

        if(inGameUI.gameObject.activeInHierarchy)
        {
            inGameUI.UpdateIngameUI();
        }

        if(tabUI.gameObject.activeInHierarchy)
        {
            tabUI.UpdateTabUI();
        }
        
    }
}
