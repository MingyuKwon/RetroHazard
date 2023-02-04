using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    Text[] EnergyText; // 0: now, 1 : store
    Image EnergyImage;

    [SerializeField] Sprite[] energySprite;

    private void Awake() {
        EnergyText = GetComponentsInChildren<Text>();
        EnergyImage = GetComponentInChildren<Image>();
        
    }

    private void OnEnable() {
        PlayerStatus.EnergyChangeEvent += SetEnergyUI;
    }

    public void SetEnergyUI(float EnergyAmount, int Energy)
    {
        if(EnergyAmount == -1)
        {
            EnergyText[0].text = "--";
        }else
        {
            EnergyText[0].text = EnergyAmount.ToString();
        }
        
        EnergyImage.sprite = energySprite[Energy];
    }
}
