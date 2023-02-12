using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

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
        PlayerStatus.Update_IngameUI_Event += Update_IngameUI;
    }

    private void Update_IngameUI(float MaxHP, float CurrentHP, int Energy, float EnergyMaganize, float EnergyStore , int Sheild, float SheildMaganize , float SheildStore)
    {
        playerHealthUI.SetPlayerHealthUI(CurrentHP, MaxHP);
        energyUI.SetEnergyStoreUI(EnergyStore , Energy);
        energyUI.SetEnergyUI(EnergyMaganize, Energy);
        sheildDurabilityUI.SetSheildStoreUI(SheildStore);
    }

}
