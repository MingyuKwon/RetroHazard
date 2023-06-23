using UnityEngine;

////////////////// Cleared //////////////////
public class UI : MonoBehaviour
{
    public static UI instance;

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
        boxUI.UpdateBoxUI();
        inGameUI.UpdateIngameUI();
        tabUI.UpdateTabUI();
    }
}
