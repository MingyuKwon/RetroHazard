using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    PlayerHealthUI PlayerHealth;
    SheildDurabilityUI SheildDurability;
    EnergyUI Energy;
    private void Awake() {
        PlayerHealth = GetComponentInChildren<PlayerHealthUI>();
        SheildDurability = GetComponentInChildren<SheildDurabilityUI>();
        Energy = GetComponentInChildren<EnergyUI>();
    }
}
