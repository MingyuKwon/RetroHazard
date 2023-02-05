using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    Text[] EnergyText; // 0: now, 1 : store
    Image EnergyImage;
    PlayerStatus status;

    int Energy;
    int EnergyCurrent;


    [SerializeField] Sprite[] energySprite;

    private void Awake() {
        EnergyText = GetComponentsInChildren<Text>();
        EnergyImage = GetComponentInChildren<Image>();
        status = FindObjectOfType<PlayerStatus>();
        
    }

    private void OnEnable() {
        PlayerStatus.EnergyChangeEvent += SetEnergyUI;
        PlayerStatus.Energy_Item_Obtain_Event += SetEnergyStoreUI;
    }

    public void SetEnergyUI(float EnergyAmount, int EnergyKind)
    {
        if(EnergyAmount == -1)
        {
            EnergyText[0].text = "--";
            EnergyText[0].color = Color.white;
            EnergyText[1].text = "| --";
        }else
        {
            EnergyText[0].text = EnergyAmount.ToString();
            if(EnergyAmount == status.EnergyMaganizeMaximum[EnergyKind])
            {
                EnergyText[0].color = Color.green;
            }else if(EnergyAmount == 0)
            {
                EnergyText[0].color = Color.red;
            }else
            {
                EnergyText[0].color = Color.white;
            }
            
            EnergyText[1].text = "| " + status.EnergyStore[EnergyKind].ToString();
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
