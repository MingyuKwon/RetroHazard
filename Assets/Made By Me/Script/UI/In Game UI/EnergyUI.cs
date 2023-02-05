using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    Text[] EnergyText; // 0: now, 1 : store
    Image EnergyImage;

    int Energy;
    int EnergyCurrent;
    int EnergyStore;


    [SerializeField] Sprite[] energySprite;

    private void Awake() {
        EnergyText = GetComponentsInChildren<Text>();
        EnergyImage = GetComponentInChildren<Image>();
        
    }

    private void OnEnable() {
        PlayerStatus.EnergyChangeEvent += SetEnergyUI;
        PlayerStatus.Energy_Item_Obtain_Event += SetEnergyStoreUI;
    }

    public void SetEnergyUI(float EnergyAmount, int EnergyKind, float EnergyStore)
    {
        if(EnergyAmount == -1)
        {
            EnergyText[0].text = "--";
            EnergyText[1].text = "| --";
        }else
        {
            EnergyText[0].text = EnergyAmount.ToString();
            EnergyText[1].text = "| " + EnergyStore.ToString();
        }
        Energy = EnergyKind;
        EnergyImage.sprite = energySprite[EnergyKind];
    }

    public void SetEnergyStoreUI(float EnergyObtained, int EnergyKind)
    {
        if(Energy == EnergyKind)
        {
            EnergyText[1].text = "| " + EnergyObtained.ToString();
        }
    }
}
