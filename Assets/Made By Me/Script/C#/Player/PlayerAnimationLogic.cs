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
    Player player;
    Animator animator;

    private PlayerStatus status;

    public PlayerAnimationLogic(Player _player, Animator _playerAnimator)
    {
        player = _player;
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
        isWalkingPress = player.GetButton("Move Up") || player.GetButton("Move Down") || player.GetButton("Move Right") || player.GetButton("Move Left");
        if(GameManager.isPlayerPaused) return;
        
        SetWalkAnimation();
        SetAttackAnimation();
        SetShieldAnimation();
        SetParryAnimation();
    }

    public void delegateFunctions()
    {
        GameManager.EventManager.SheildCrashEvent += SetSheildCrash;
        GameManager.EventManager.SheildRecoveryEvent += SetSheildRecovery;
    }

    public void removeFunctions()
    {
        GameManager.EventManager.SheildCrashEvent -= SetSheildCrash;
        GameManager.EventManager.SheildRecoveryEvent -= SetSheildRecovery;
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
        if(player.GetButtonDown("Attack"))
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

        if(player.GetButtonUp("Shield"))
        {
            if(status.isBlocked) return;
            GameManager.instance.SetPlayerMove(true);
        }

        if(player.GetButton("Shield"))
        {
            GameManager.instance.SetPlayerMove(false);
            animator.SetFloat("Sheild Kind", status.Sheild);
        }

        isSheilding = player.GetButton("Shield");
        animator.SetBool("Shield", isSheilding);
    }

    private void SetParryAnimation()
    {
        if(isParrying || sheildCrash) return;

        if(player.GetButton("Shield") && player.GetButtonDown("Interactive") && status.Sheild != 2)
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
            if(player.GetButton("Move Up"))
            {
                if(!(XInput == 0 && YInput == 1))
                {
                    XInput = 0;
                    YInput = 1;
                }
            }

            if(player.GetButton("Move Down"))
            {
                if(!(XInput == 0 && YInput == -1))
                {
                    XInput = 0;
                    YInput = -1;
                }
            }

            if(player.GetButton("Move Right"))
            {
                if(!(XInput == 1 && YInput == 0))
                {
                    XInput = 1;
                    YInput = 0;
                }
            }

            if(player.GetButton("Move Left"))
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

    ////////////////logical functions///////////////////////////////////


    //////////////////////delegate player input functions///////////////////////////////////
    public void delegateInpuiFunctions()
    {
        player.AddInputEventDelegate(UPPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed, "Move Up");
        player.AddInputEventDelegate(DownPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed, "Move Down");
        player.AddInputEventDelegate(RightPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed,"Move Right");
        player.AddInputEventDelegate(LeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed,"Move Left");

        player.AddInputEventDelegate(UPJustPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Move Up");
        player.AddInputEventDelegate(DownJustPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Move Down");
        player.AddInputEventDelegate(RightJustPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed,"Move Right");
        player.AddInputEventDelegate(LeftJustPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed,"Move Left");

        player.AddInputEventDelegate(UPJustReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "Move Up");
        player.AddInputEventDelegate(DownJustReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "Move Down");
        player.AddInputEventDelegate(RightJustReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased,"Move Right");
        player.AddInputEventDelegate(LeftJustReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased,"Move Left");
    }

    public void removeInpuiFunctions()
    {
        player.RemoveInputEventDelegate(UPPressed);
        player.RemoveInputEventDelegate(DownPressed);
        player.RemoveInputEventDelegate(RightPressed);
        player.RemoveInputEventDelegate(LeftPressed);

        player.RemoveInputEventDelegate(UPJustPressed);
        player.RemoveInputEventDelegate(DownJustPressed);
        player.RemoveInputEventDelegate(RightJustPressed);
        player.RemoveInputEventDelegate(LeftJustPressed);

        player.RemoveInputEventDelegate(UPJustReleased);
        player.RemoveInputEventDelegate(DownJustReleased);
        player.RemoveInputEventDelegate(RightJustReleased);
        player.RemoveInputEventDelegate(LeftJustReleased);
    }

    //input Aniamtion Event
    // keep presseing
    void UPPressed(InputActionEventData data)
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = 0f;
        LastYInput = 1f;
    }

    void DownPressed(InputActionEventData data)
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = 0f;
        LastYInput = -1f;
    }

    void RightPressed(InputActionEventData data)
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = 1f;
        LastYInput = 0f;
    }

    void LeftPressed(InputActionEventData data)
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = -1f;
        LastYInput = 0f;
    }
    // keep presseing


    // just the time press the button
    void UPJustPressed(InputActionEventData data)
    {
        XInput = 0f;
        YInput = 1f;
    }

    void DownJustPressed(InputActionEventData data)
    {
        XInput = 0f;
        YInput = -1f;
    }

    void RightJustPressed(InputActionEventData data)
    {
        XInput = 1f;
        YInput = 0f;
    }

    void LeftJustPressed(InputActionEventData data)
    {
        XInput = -1f;
        YInput = 0f;
    }
    // just the time press the button

    // just the time release the button
    void UPJustReleased(InputActionEventData data)
    {
        if(player.GetButton("Move Right"))
        {
            XInput = 1f;
            YInput = 0f;
        }else if(player.GetButton("Move Left"))
        {
            XInput = -1f;
            YInput = 0f;
        }else if(player.GetButton("Move Down"))
        {
            XInput = 0f;
            YInput = -1f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
        
    }

    void DownJustReleased(InputActionEventData data)
    {        
        if(player.GetButton("Move Right"))
        {
            XInput = 1f;
            YInput = 0f;
        }else if(player.GetButton("Move Left"))
        {
            XInput = -1f;
            YInput = 0f;
        }else if(player.GetButton("Move Up"))
        {
            XInput = 0f;
            YInput = 1f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
    }

    void RightJustReleased(InputActionEventData data)
    {        
        if(player.GetButton("Move Up"))
        {
            XInput = 0f;
            YInput = 1f;
        }else if(player.GetButton("Move Down"))
        {
            XInput = 0f;
            YInput = -1f;
        }else if(player.GetButton("Move Left"))
        {
            XInput = -1f;
            YInput = 0f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
    }

    void LeftJustReleased(InputActionEventData data)
    {        
        if(player.GetButton("Move Up"))
        {
            XInput = 0f;
            YInput = 1f;
        }else if(player.GetButton("Move Down"))
        {
            XInput = 0f;
            YInput = -1f;
        }else if(player.GetButton("Move Right"))
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
