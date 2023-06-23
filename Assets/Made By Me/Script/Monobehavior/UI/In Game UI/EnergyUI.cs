using UnityEngine;
using UnityEngine.UI;

//////// Cleared ////////////////
public class EnergyUI : MonoBehaviour
{
    Text[] EnergyText; // 0: now, 1 : store
    Image EnergyImage;

    int EnergyCurrent;

    [SerializeField] Sprite energy0Sprite;
    [SerializeField] Sprite[] energy1Sprite;
    [SerializeField] Sprite[] energy2Sprite;
    [SerializeField] Sprite[] energy3Sprite;

    private void Awake() {
        EnergyText = GetComponentsInChildren<Text>();
        EnergyImage = GetComponentInChildren<Image>();
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
            if(EnergyAmount == Player1.instance.playerStatus.EnergyMaganizeMaximum[EnergyKind])
            {
                EnergyText[0].color = Color.green;
            }else if(EnergyAmount == 0)
            {
                EnergyText[0].color = Color.red;
            }else
            {
                EnergyText[0].color = Color.white;
            }
    
        }
        //EnergyImage.sprite = energySprite[EnergyKind];
    }

    public void SetEnergyStoreUI(float EnergyObtained, int EnergyKind)
    {
        EnergyText[1].text = "| " + EnergyObtained.ToString();
    }

    public void SetEnergyImageUI(int EnergyUpgrade, int EnergyKind)
    {
        EnergyUpgrade--;

        if(EnergyKind == 0 || EnergyUpgrade < 0)
        {
            EnergyImage.sprite = energy0Sprite;
        }else if(EnergyKind == 1)
        {
            EnergyImage.sprite = energy1Sprite[EnergyUpgrade];
        }else if(EnergyKind == 2)
        {
            EnergyImage.sprite = energy2Sprite[EnergyUpgrade];
        }else if(EnergyKind == 3)
        {
            EnergyImage.sprite = energy3Sprite[EnergyUpgrade];
        }
        
    }
}
