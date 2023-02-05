using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheildDurabilityUI : MonoBehaviour
{
    PlayerStatus playerStatus;
    Image sheildDurabilityImage;
    Text sheildDurabilityText;
    Animator animator;
    float SheildDurability;
    float Sheild;

    private void Awake() {
        sheildDurabilityImage = GetComponentInChildren<Image>();
        sheildDurabilityText = GetComponentInChildren<Text>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        playerStatus = FindObjectOfType<PlayerStatus>();
        SheildDurability = playerStatus.SheildDurability;
        Sheild = playerStatus.Sheild;
        animator.SetFloat("Sheild Durability", SheildDurability);
        animator.SetFloat("Sheild Kind", Sheild);
    }
    private void OnEnable() {
        PlayerStatus.SheildDurabilityChangeEvent += SetSheildDurabilityUI;
        PlayerStatus.SheildCrashEvent += SetSheildCrash;
        PlayerStatus.SheildRecoveryEvent += SetSheildRecovery;
        PlayerStatus.Sheild_Durability_Item_Obtain_Event += SetSheildStoreUI;
        PlayerShield.Sheild_Durability_Reduce_Start_Event += Sheild_Durability_Reduce_Start;
    }

    private void SetSheildDurabilityUI(float CurrentDurability, int SheildKind, float SheildStore)
    {
        SheildDurability = CurrentDurability;
        Sheild = SheildKind;
        sheildDurabilityText.text = "/ " + SheildStore.ToString();
        animator.SetBool("Sheild Durability Reducing", false);
        animator.SetFloat("Sheild Durability", SheildDurability);
        animator.SetFloat("Sheild Kind", SheildKind);
    }
    private void SetSheildCrash(bool ChangeSheild)
    {
        if(ChangeSheild) return;

        animator.SetTrigger("Sheild Crash");
    }

    private void SetSheildRecovery(bool ChangeSheild)
    {
        
    }
    private void Sheild_Durability_Reduce_Start()
    {
        animator.SetBool("Sheild Durability Reducing", true);
    }

    public void SetSheildStoreUI(float SheildObtained, int SheildKind)
    {
        if(Sheild == SheildKind)
        {
            sheildDurabilityText.text = "/ " + SheildObtained.ToString();
        }
    }
}
