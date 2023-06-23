using UnityEngine;
using UnityEngine.UI;

public class SheildDurabilityUI : MonoBehaviour
{    Image[] sheildDurabilityImages;
    Text sheildDurabilityText;
    Animator UIanimator;

    float SheildDurability;
    float Sheild;

    private void Awake() {
        sheildDurabilityImages = GetComponentsInChildren<Image>();
        sheildDurabilityText = GetComponentInChildren<Text>();
        UIanimator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        
        SheildDurability = Player1.instance.playerStatus.SheildMaganize[Player1.instance.playerStatus.Sheild];
        Sheild = Player1.instance.playerStatus.Sheild;
        UIanimator.SetFloat("Sheild Durability", SheildDurability);
        UIanimator.SetFloat("Sheild Kind", Sheild);
    }
    //
    private void OnEnable() {
        GameManager.EventManager.Sheild_Durability_Change_Event += SetSheildDurabilityUI;
        GameManager.EventManager.SheildCrashEvent += SetSheildCrash;
        GameManager.EventManager.Sheild_Durability_Reduce_Start_Event += Sheild_Durability_Reduce_Start;
    }

    private void OnDisable() {
        GameManager.EventManager.Sheild_Durability_Change_Event -= SetSheildDurabilityUI;
        GameManager.EventManager.SheildCrashEvent -= SetSheildCrash;
        GameManager.EventManager.Sheild_Durability_Reduce_Start_Event -= Sheild_Durability_Reduce_Start;
    }

    public void SetSheildDurabilityUIAvailabe(int SheildUpgrade)
    {
        if(SheildUpgrade == 0)
        {
            sheildDurabilityImages[0].enabled = false;
            sheildDurabilityImages[1].enabled = false;
            sheildDurabilityText.enabled = false;
        }else
        {
            sheildDurabilityImages[0].enabled = true;
            sheildDurabilityImages[1].enabled = true;
            sheildDurabilityText.enabled = true;
        }
    }

    public void SetSheildDurabilityUI(float CurrentDurability, int SheildKind)
    {
        SheildDurability = CurrentDurability;
        Sheild = SheildKind;
        sheildDurabilityText.text = "/ " + Player1.instance.playerStatus.SheildStore.ToString();
        UIanimator.SetBool("Sheild Durability Reducing", false);
        UIanimator.SetFloat("Sheild Durability", SheildDurability);
        UIanimator.SetFloat("Sheild Kind", SheildKind);
    }
    private void SetSheildCrash(bool ChangeSheild)
    {
        if(ChangeSheild) return;

        UIanimator.SetTrigger("Sheild Crash");
    }

    private void SetSheildRecovery(bool ChangeSheild)
    {
        
    }
    private void Sheild_Durability_Reduce_Start()
    {
        UIanimator.SetBool("Sheild Durability Reducing", true);
    }

    public void SetSheildStoreUI(float SheildObtained)
    {
        sheildDurabilityText.text = "/ " + SheildObtained.ToString();
    }
}
