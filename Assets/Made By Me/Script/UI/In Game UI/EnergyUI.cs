using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    Text EnergyText;

    private void Awake() {
        EnergyText = GetComponent<Text>();
    }

    private void Start() {
        EnergyText.text =  FindObjectOfType<PlayerStatus>().EnergyAmount.ToString();
    }

    private void OnEnable() {
        PlayerStatus.EnergyChangeEvent += SetEnergyUI;
    }

    public void SetEnergyUI(float EnergyAmount)
    {
        EnergyText.text = EnergyAmount.ToString();
    }
}
