using System.Collections;
using UnityEngine;

/////// Cleared ////////
public class InGameUI : MonoBehaviour
{
    PlayerHealthUI playerHealthUI;
    SheildDurabilityUI sheildDurabilityUI;
    EnergyUI energyUI;
    private void Awake() {
        playerHealthUI = GetComponentInChildren<PlayerHealthUI>();
        sheildDurabilityUI = GetComponentInChildren<SheildDurabilityUI>();
        energyUI = GetComponentInChildren<EnergyUI>();
    }

    private void OnEnable() {
        GameManager.EventManager.Update_IngameUI_Event += Update_IngameUI;
    }

    private void OnDisable() {
        GameManager.EventManager.Update_IngameUI_Event -= Update_IngameUI;
    }

    public void UpdateIngameUI()
    {
        StartCoroutine(UpdateUIDelay());
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
