using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheildDurabilityUI : MonoBehaviour
{
    Text sheildDurabilityText;

    private void Awake() {
        sheildDurabilityText = GetComponent<Text>();
    }

    private void Start() {
        sheildDurabilityText.text =  FindObjectOfType<PlayerStatus>().SheildDurability.ToString();
    }
    private void OnEnable() {
        PlayerStatus.SheildDurabilityChangeEvent += SetSheildDurabilityUI;
        PlayerStatus.SheildCrashEvent += SetSheildCrash;
        PlayerStatus.SheildRecoveryEvent += SetSheildRecovery;
        PlayerShield.Sheild_Durability_Reduce_Start_Event += Sheild_Durability_Reduce_Start;
    }

    private void SetSheildDurabilityUI(float CurrentDurability)
    {
        sheildDurabilityText.text = CurrentDurability.ToString();
        sheildDurabilityText.color = Color.white;
    }
    private void SetSheildCrash(bool ChangeSheild)
    {
        sheildDurabilityText.color = Color.red;
    }

    private void SetSheildRecovery(bool ChangeSheild)
    {
        sheildDurabilityText.color = Color.white;
    }
    private void Sheild_Durability_Reduce_Start()
    {
        sheildDurabilityText.color = Color.gray;
    }
}
