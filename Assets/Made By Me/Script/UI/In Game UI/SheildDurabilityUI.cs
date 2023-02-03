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
    }

    public void SetSheildDurabilityUI(float CurrentDurability)
    {
        sheildDurabilityText.text = CurrentDurability.ToString();
    }
    public void SetSheildCrash()
    {
        sheildDurabilityText.color = Color.red;
    }

    public void SetSheildRecovery()
    {
        sheildDurabilityText.color = Color.white;
    }
}
