using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerAnimationLogic
{
    //flags and values///////////////////////////////////
    public bool sheildCrash; // is Sheild is broken and cannot use

    public bool isAttacking; // is Player is now doing Attack Animation
    public bool isSheilding; // is Player is now doing Sheild Animation
    public bool isParrying; // is Player is now doing Parrying Animation

    public bool isWalkingPress;

    public float LastXInput = 0f;
    public float LastYInput = -1f;

     public float XInput = 0f;
     public float YInput = 0f;

     public float BeforePauseXInput = 0f;
     public float BeforePauseYInput = 0f;
    //flags and values///////////////////////////////////


    /////////references and constructor///////////////////////////////////
    Animator animator;

    private PlayerStatus status;

    public PlayerAnimationLogic(Animator _playerAnimator)
    {
        animator = _playerAnimator;
    }
    
    /////////references and constructor///////////////////////////////////

    //monobehavior function//////////////////////////////////////////////////////////////////
    public void Start() {
        status = Player1.instance.playerStatus;
        
    }

    public void Update()
    {
        GameManager.isPlayerSheilding = isSheilding;
        isWalkingPress = GameMangerInput.inputCheck.isPressingUP()|| GameMangerInput.inputCheck.isPressingDown() 
        || GameMangerInput.inputCheck.isPressingRight() || GameMangerInput.inputCheck.isPressingLeft();

        if(GameManager.isPlayerPaused) return;
        
        SetWalkAnimation();
        SetAttackAnimation();
        SetShieldAnimation();
        SetParryAnimation();
    }

     //monobehavior function//////////////////////////////////////////////////////////////////

    ////////////////logical functions///////////////////////////////////
    public void SetWalkAnimation()
    {
        animator.SetFloat("XInput", XInput);
        animator.SetFloat("YInput", YInput);
        animator.SetFloat("LastXInput", LastXInput);
        animator.SetFloat("LastYInput", LastYInput);
    }

    private void SetAttackAnimation()
    {
        if(status.EnergyMaganize[status.Energy] == 0) return;
        if(GameMangerInput.inputCheck.isAttackButtonDown())
        {
            if(isAttacking) return;
            if(isSheilding) return;
            if(isParrying) return;

            animator.SetTrigger("Attack");
            isAttacking = true;
            GameManager.instance.SetPlayerMove(false);
            animator.SetFloat("Energy", status.Energy);
            status.EnergyUse(1, status.Energy);
        }
    }

    private void SetShieldAnimation()
    {
        if(isParrying) return;
        if(sheildCrash) return;
        if(GameMangerInput.inputCheck.isShieldButtonUp())
        {
            if(status.isBlocked) return;
            GameManager.instance.SetPlayerMove(true);
        }

        if(GameMangerInput.inputCheck.isPressingShield())
        {
            GameManager.instance.SetPlayerMove(false);
            animator.SetFloat("Sheild Kind", status.Sheild);
        }
        isSheilding = GameMangerInput.inputCheck.isPressingShield();
        animator.SetBool("Shield", isSheilding);
    }

    private void SetParryAnimation()
    {
        if(isParrying || sheildCrash) return;

        if(GameMangerInput.inputCheck.isPressingShield() && GameMangerInput.inputCheck.isInteractiveButtonDown() && status.Sheild != 2)
        {
            animator.SetTrigger("Parry");
            isParrying = true;
        }
    }



    public void StunAnimationStart()
    {
        animator.SetTrigger("Stun");
    }

    public void SetSheildCrash(bool ChangeSheild)
    {
        sheildCrash = true;
        isSheilding = false;
        animator.SetBool("Shield", isSheilding);
    }

    public void SetSheildRecovery(bool ChangeSheild)
    {
        sheildCrash = false;
        GameManager.instance.SetPlayerMove(true);
    }

    public void WhenPauseReleased()
    {
        if(!isWalkingPress)
        {
            XInput = 0;
            YInput = 0;
        }else
        {
            if(GameMangerInput.inputCheck.isPressingUP())
            {
                if(!(XInput == 0 && YInput == 1))
                {
                    XInput = 0;
                    YInput = 1;
                }
            }

            if(GameMangerInput.inputCheck.isPressingDown())
            {
                if(!(XInput == 0 && YInput == -1))
                {
                    XInput = 0;
                    YInput = -1;
                }
            }

            if(GameMangerInput.inputCheck.isPressingRight())
            {
                if(!(XInput == 1 && YInput == 0))
                {
                    XInput = 1;
                    YInput = 0;
                }
            }

            if(GameMangerInput.inputCheck.isPressingLeft())
            {
                if(!(XInput == -1 && YInput == 0))
                {
                    XInput = -1;
                    YInput = 0;
                }
            }

        }
    }


    public void ResetPlayerAnimationState() // called by animation
    {
        isAttacking = false;
        isParrying = false;
        isSheilding = false;
        animator.ResetTrigger("Block");
        animator.ResetTrigger("Parry");

        if(status == null) return;

        status.parryFrame = false;
    }

    public void ResetPlayerAnimationState_CalledByGameManager() // called by gamemanger (when talk with someone, you should stop and not walking on the spot)
    {
        ResetPlayerAnimationState();
        animator.SetFloat("XInput", 0);
        animator.SetFloat("YInput", 0);
    }

    public void SlashStart()
    {
        
    }

    public void SlashEnd()
    {
        isAttacking = false;
        GameManager.instance.SetPlayerMove(true);
    }

    public void StabStart()
    {
        
    }

    public void StabEnd() 
    {
        isAttacking = false;
        GameManager.instance.SetPlayerMove(true);
    }

    public void EnergyReloadEnd()
    {
        Player1.instance.playerInventory.EnergyReload();
        GameManager.instance.ResetPlayerAnimationState();
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.SetPausePlayer(false);
    }

    public void SheildReloadEnd()
    {
        Player1.instance.playerInventory.SheildReLoad();
        GameManager.instance.ResetPlayerAnimationState();
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.SetPausePlayer(false);
    }

    ////////////////logical functions///////////////////////////////////


    //////////////////////delegate player input functions///////////////////////////////////
    public void OnEnable() {
        GameMangerInput.InputEvent.UPPressed += UPPressed;
        GameMangerInput.InputEvent.DownPressed += DownPressed;
        GameMangerInput.InputEvent.RightPressed += RightPressed;
        GameMangerInput.InputEvent.LeftPressed += LeftPressed;

        GameMangerInput.InputEvent.UPJustPressed += UPJustPressed;
        GameMangerInput.InputEvent.DownJustPressed += DownJustPressed;
        GameMangerInput.InputEvent.RightJustPressed += RightJustPressed;
        GameMangerInput.InputEvent.LeftJustPressed += LeftJustPressed;

        GameMangerInput.InputEvent.UPJustReleased += UPJustReleased;
        GameMangerInput.InputEvent.DownJustReleased += DownJustReleased;
        GameMangerInput.InputEvent.RightJustReleased += RightJustReleased;
        GameMangerInput.InputEvent.LeftJustReleased += LeftJustReleased;

        GameManager.EventManager.SheildCrashEvent += SetSheildCrash;
        GameManager.EventManager.SheildRecoveryEvent += SetSheildRecovery;
    }

    public void OnDisable() {
        GameMangerInput.InputEvent.UPPressed -= UPPressed;
        GameMangerInput.InputEvent.DownPressed -= DownPressed;
        GameMangerInput.InputEvent.RightPressed -= RightPressed;
        GameMangerInput.InputEvent.LeftPressed -= LeftPressed;

        GameMangerInput.InputEvent.UPJustPressed -= UPJustPressed;
        GameMangerInput.InputEvent.DownJustPressed -= DownJustPressed;
        GameMangerInput.InputEvent.RightJustPressed -= RightJustPressed;
        GameMangerInput.InputEvent.LeftJustPressed -= LeftJustPressed;

        GameMangerInput.InputEvent.UPJustReleased -= UPJustReleased;
        GameMangerInput.InputEvent.DownJustReleased -= DownJustReleased;
        GameMangerInput.InputEvent.RightJustReleased -= RightJustReleased;
        GameMangerInput.InputEvent.LeftJustReleased -= LeftJustReleased;

        GameManager.EventManager.SheildCrashEvent -= SetSheildCrash;
        GameManager.EventManager.SheildRecoveryEvent -= SetSheildRecovery;
    }

    //input Aniamtion Event
    // keep presseing
    void UPPressed()
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = 0f;
        LastYInput = 1f;
    }

    void DownPressed()
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = 0f;
        LastYInput = -1f;
    }

    void RightPressed()
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = 1f;
        LastYInput = 0f;
    }

    void LeftPressed()
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = -1f;
        LastYInput = 0f;
    }
    // keep presseing


    // just the time press the button
    void UPJustPressed()
    {
        XInput = 0f;
        YInput = 1f;
    }

    void DownJustPressed()
    {
        XInput = 0f;
        YInput = -1f;
    }

    void RightJustPressed()
    {
        XInput = 1f;
        YInput = 0f;
    }

    void LeftJustPressed()
    {
        XInput = -1f;
        YInput = 0f;
    }
    // just the time press the button

    // just the time release the button
    void UPJustReleased()
    {
        if(GameMangerInput.inputCheck.isPressingRight())
        {
            XInput = 1f;
            YInput = 0f;
        }else if(GameMangerInput.inputCheck.isPressingLeft())
        {
            XInput = -1f;
            YInput = 0f;
        }else if(GameMangerInput.inputCheck.isPressingDown())
        {
            XInput = 0f;
            YInput = -1f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
        
    }

    void DownJustReleased()
    {        
        if(GameMangerInput.inputCheck.isPressingRight())
        {
            XInput = 1f;
            YInput = 0f;
        }else if(GameMangerInput.inputCheck.isPressingLeft())
        {
            XInput = -1f;
            YInput = 0f;
        }else if(GameMangerInput.inputCheck.isPressingUP())
        {
            XInput = 0f;
            YInput = 1f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
    }

    void RightJustReleased()
    {        
        if(GameMangerInput.inputCheck.isPressingUP())
        {
            XInput = 0f;
            YInput = 1f;
        }else if(GameMangerInput.inputCheck.isPressingDown())
        {
            XInput = 0f;
            YInput = -1f;
        }else if(GameMangerInput.inputCheck.isPressingLeft())
        {
            XInput = -1f;
            YInput = 0f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
    }

    void LeftJustReleased()
    {        
        if(GameMangerInput.inputCheck.isPressingUP())
        {
            XInput = 0f;
            YInput = 1f;
        }else if(GameMangerInput.inputCheck.isPressingDown())
        {
            XInput = 0f;
            YInput = -1f;
        }else if(GameMangerInput.inputCheck.isPressingRight())
        {
            XInput = 1f;
            YInput = 0f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
    }
    // just the time release the button

    //input Aniamtion Event

    //////////////////////delegate player input functions///////////////////////////////////





    


    
}
