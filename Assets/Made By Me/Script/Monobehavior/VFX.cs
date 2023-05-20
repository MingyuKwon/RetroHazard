using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class VFX : MonoBehaviour
{
    public CapsuleCollider2D BodyCollider;
    public BoxCollider2D attackCollider;
    public BoxCollider2D sheildCollider;
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
        PlayerStatus.SheildCrashEvent += SheildCrash;
        PlayerShield.Recovery_VFX_Start_Event += SheildRecovery;
    }

    private void OnDisable() {
        PlayerStatus.SheildCrashEvent -= SheildCrash;
        PlayerShield.Recovery_VFX_Start_Event -= SheildRecovery;
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
        if(isPlayer)
        {
            BodyCollider.enabled = false;
        }
        
    }

    public void ParryVFXEnd()
    {
        if(isPlayer)
        {
            BodyCollider.enabled = true;
        }
        
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
