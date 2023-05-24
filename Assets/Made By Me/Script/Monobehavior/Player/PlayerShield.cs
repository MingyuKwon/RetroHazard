using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

// ================ code cleaned by making PlayerSheildLogic ==============================

public class PlayerShield : MonoBehaviour
{
    PlayerAnimation playerAnimation;

    private PlayerSheildLogic playerSheildLogic;

    private void Awake() {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerSheildLogic = new PlayerSheildLogic(playerAnimation);
    }

    private void Start() {
        playerSheildLogic.Start();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        playerSheildLogic.CollisionEnter2D(other);
    }


    public void RecoveryVFXStart()
    {
        playerSheildLogic.RecoveryVFXStart();
    }

    public void ParryFrameStart()
    {
        playerSheildLogic.ParryFrameStart();
    }

    public void ParryFrameEnd()
    {
       playerSheildLogic.ParryFrameEnd();
    }

    public void ParryStart()
    {
        playerSheildLogic.ParryStart();
    }

    public void ParryEnd()
    {
        playerSheildLogic.ParryEnd();
    }


    public void BlockStart()
    {
        playerSheildLogic.BlockStart();
    }

    public void BlockEnd()
    {
        playerSheildLogic.BlockEnd();
    }
}
