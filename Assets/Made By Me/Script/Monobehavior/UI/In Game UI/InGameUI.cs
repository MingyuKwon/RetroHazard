using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

/////// Cleared ////////
public class InGameUI : MonoBehaviour
{
    PlayerHealthUI playerHealthUI;
    SheildDurabilityUI sheildDurabilityUI;
    EnergyUI energyUI;
    MapShowPanel mapShowPanel;

    float showingTime = 1f;

    Vector2 invisiblePosition = new Vector2(200, 70);
    Vector2 fullVisiblePosition = new Vector2(200, -10);

    private RectTransform mapShowPanelRecTransform;
    private Text mapShowPanelText;

    bool canStartCoroutine = true;

    ////////// 보간을 역행하기 위한 배열 /////////////////
    float[] saveYPosition;
    ////////// 보간을 역행하기 위한 배열 /////////////////

    private void Awake() {
        playerHealthUI = GetComponentInChildren<PlayerHealthUI>();
        sheildDurabilityUI = GetComponentInChildren<SheildDurabilityUI>();
        energyUI = GetComponentInChildren<EnergyUI>();
        mapShowPanel = GetComponentInChildren<MapShowPanel>();

        mapShowPanelRecTransform = mapShowPanel.gameObject.GetComponent<RectTransform>();
        mapShowPanelText = mapShowPanel.gameObject.GetComponentInChildren<Text>();

        mapShowPanelRecTransform.anchoredPosition = invisiblePosition;

    }

    private void OnEnable() {
        GameManager.EventManager.Update_IngameUI_Event += Update_IngameUI;
        UpdateIngameUI();
    }

    private void OnDisable() {
        GameManager.EventManager.Update_IngameUI_Event -= Update_IngameUI;
    }

    public void UpdateIngameUI()
    {
        if(this.gameObject.activeInHierarchy)
        {
            StartCoroutine(UpdateUIDelay());
        }
        
    }

    [Button]
    public void ShowMapShowPanel(string mapName)
    {
        if(!canStartCoroutine) return;

        canStartCoroutine = false;
        mapShowPanelRecTransform.anchoredPosition = invisiblePosition;
        mapShowPanelText.text = mapName;
        StartCoroutine(ShowMapShowPanelCoroutine());
    }

    public IEnumerator ShowMapShowPanelCoroutine()
    {
        float fullShowTimeScholar = 0.3f;

        float showingTimeHalf = (showingTime * (1 - fullShowTimeScholar) / 2) / Time.unscaledDeltaTime;
        float fullShowingTime = showingTime * fullShowTimeScholar / Time.unscaledDeltaTime;

        saveYPosition = new float[Mathf.FloorToInt(showingTimeHalf) + 2];
        
        saveYPosition[0] = mapShowPanelRecTransform.anchoredPosition.y;

        for(int i=0; i<Mathf.FloorToInt(showingTimeHalf)+1; i++)
        {
            MoveShowMapShowPanel(0.1f, i, true);
            yield return new WaitForEndOfFrame();
        }

        for(int i=0; i<Mathf.FloorToInt(fullShowingTime); i++)
        {
            yield return new WaitForEndOfFrame();
        }

        for(int i=0; i<Mathf.FloorToInt(showingTimeHalf)+2; i++)
        {
            MoveShowMapShowPanel(0.1f, i, false);
            yield return new WaitForEndOfFrame();
        }

        saveYPosition = null;
        canStartCoroutine = true;
    }

    private void MoveShowMapShowPanel(float C ,int index , bool isShowing) // index 안주면 되돌리기
    {
        if(isShowing)
        {
            float LerpedYValue =  Mathf.Lerp(mapShowPanelRecTransform.anchoredPosition.y, fullVisiblePosition.y, C);
            saveYPosition[index+1] = LerpedYValue;
            mapShowPanelRecTransform.anchoredPosition = new Vector2(mapShowPanelRecTransform.anchoredPosition.x, LerpedYValue);
        
        }else
        {
            int reverseIndex = saveYPosition.Length - index - 1;

            mapShowPanelRecTransform.anchoredPosition = 
                new Vector2(mapShowPanelRecTransform.anchoredPosition.x, saveYPosition[reverseIndex]);
        }

       
    }

    public IEnumerator UpdateUIDelay()
    {
        yield return new WaitForEndOfFrame();
        PlayerStatus status = Player1.instance.playerStatus;

        GameManager.EventManager.Invoke_Update_IngameUI_Event(status.MaxHP, status.CurrentHP, status.Energy, 
        status.EnergyMaganize[status.Energy], status.EnergyStore[status.Energy] ,  status.Sheild, status.SheildMaganize[status.Sheild], 
        status.SheildStore, status.EnergyUpgrade[status.Energy], status.SheildUpgrade[status.Sheild]);
        
        UI.instance.tabUI.itemUI.UpdateInventoryUI();
        UI.instance.boxUI.playerItemUI.UpdateInventoryUI();
    }

    public void Update_IngameUI(float MaxHP, float CurrentHP, int Energy, float EnergyMaganize, float EnergyStore , int Sheild, float SheildMaganize , float SheildStore, int EnergyUpgrade, int SheildUpgrade)
    {
        playerHealthUI.SetPlayerHealthUI(CurrentHP, MaxHP);
        energyUI.SetEnergyStoreUI(EnergyStore , Energy);
        energyUI.SetEnergyUI(EnergyMaganize, Energy);
        energyUI.SetEnergyImageUI(EnergyUpgrade, Energy);
        sheildDurabilityUI.SetSheildStoreUI(SheildStore);
        sheildDurabilityUI.SetSheildDurabilityUI(SheildMaganize, Sheild);
        sheildDurabilityUI.SetSheildDurabilityUIAvailabe(SheildUpgrade);
    }

}
