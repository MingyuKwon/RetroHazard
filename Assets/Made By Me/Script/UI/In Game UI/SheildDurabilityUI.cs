using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheildDurabilityUI : MonoBehaviour
{
    PlayerStatus playerStatus;
    Image sheildDurabilityImage;
    Animator animator;
    float SheildDurability;
    float SheildKind;

    private void Awake() {
        sheildDurabilityImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        playerStatus = FindObjectOfType<PlayerStatus>();
        SheildDurability = playerStatus.SheildDurability;
        SheildKind = playerStatus.Sheild;
        animator.SetFloat("Sheild Durability", SheildDurability);
        animator.SetFloat("Sheild Kind", SheildKind);
    }
    private void OnEnable() {
        PlayerStatus.SheildDurabilityChangeEvent += SetSheildDurabilityUI;
        PlayerStatus.SheildCrashEvent += SetSheildCrash;
        PlayerStatus.SheildRecoveryEvent += SetSheildRecovery;
        PlayerShield.Sheild_Durability_Reduce_Start_Event += Sheild_Durability_Reduce_Start;
    }

    private void SetSheildDurabilityUI(float CurrentDurability, int Sheild)
    {
        SheildDurability = CurrentDurability;
        animator.SetBool("Sheild Durability Reducing", false);
        animator.SetFloat("Sheild Durability", SheildDurability);
        animator.SetFloat("Sheild Kind", Sheild);
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
}
