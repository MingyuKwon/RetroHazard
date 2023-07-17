using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class VFX : MonoBehaviour
{
    private Animator vfxAnimator;

    private EnemyManager enemyManager = null;
    private bool isPlayer;

    private void Awake() {
        enemyManager = transform.parent.GetComponent<EnemyManager>();
        
        if(enemyManager == null)
        {
            isPlayer = true;
        }else
        {
            isPlayer = false;
        }

        vfxAnimator = GetComponent<Animator>();
    }

    private void OnEnable() {
        GameManager.EventManager.SheildCrashEvent += SheildCrash;
        GameManager.EventManager.Recovery_VFX_Start_Event += SheildRecovery;
    }

    private void OnDisable() {
        GameManager.EventManager.SheildCrashEvent -= SheildCrash;
        GameManager.EventManager.Recovery_VFX_Start_Event -= SheildRecovery;
    }

    private void SheildCrash(bool ChangeSheild)
    {
        if(ChangeSheild) return;
        if(isPlayer)
        vfxAnimator.SetTrigger("Sheild Crash");
    }

    private void SheildRecovery()
    {
        if(isPlayer)
        vfxAnimator.SetTrigger("Sheild Recovery");
    }


    public void ParryVFXStart()
    {

        
    }

    public void ParryVFXEnd()
    {

    }


    public void ParreidVFXStart()
    {
    }

    public void ParriedVFXEnd()
    {
    }

    public void DeathStart()
    {

    }

    public void DeathEnd()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
