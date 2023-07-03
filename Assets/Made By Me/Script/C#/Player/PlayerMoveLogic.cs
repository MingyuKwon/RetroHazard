using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;
public class PlayerMoveLogic
{
    public float moveSpeed = 5f;
    public float moveSpeedScholar = 1f;

    public bool canMove = true;

    public bool InputOk;
    public bool UpInputBlock;
    public bool DownInputBlock;
    public bool RightInputBlock;
    public bool LeftInputBlock;

    public bool isWalkingPress;

    float HorizontalMoveSpeed;
    float VerticalMoveSpeed;

    public Vector2 playerDirection;


    /////////references and constructor///////////////////////////////////
    private Transform transform;

    public PlayerMoveLogic(Transform _transform)
    {
        transform = _transform;
    }

    /////////references and constructor///////////////////////////////////

    ////////////////logical functions///////////////////////////////////
    public void FixedUpdate()
    {
        isWalkingPress = GameMangerInput.inputCheck.isPressingUP() || GameMangerInput.inputCheck.isPressingDown() 
        || GameMangerInput.inputCheck.isPressingRight() || GameMangerInput.inputCheck.isPressingLeft();

        if(canMove && !(GameManager.isPlayerPaused))
        {
            InputOk = true;

            if(!GameMangerInput.inputCheck.isPressingRight() && !GameMangerInput.inputCheck.isPressingLeft())
            {
                HorizontalMoveSpeed = 0f;
            }

            if(!GameMangerInput.inputCheck.isPressingUP() && !GameMangerInput.inputCheck.isPressingDown())
            {
                VerticalMoveSpeed = 0f;
            }

            playerDirection = new Vector2(HorizontalMoveSpeed, VerticalMoveSpeed).normalized;
            playerDirection = playerDirection * moveSpeed * Time.fixedDeltaTime * moveSpeedScholar;

            if( !(playerDirection.x == 0 && playerDirection.y == 0))
            {
                
            }

            transform.position += (Vector3)playerDirection;
        }else
        {
            InputOk = false;
        }
    }

    public void WhenPauseReleased()
    {

        if(!isWalkingPress)
        {
            UpInputBlock = false;
            DownInputBlock = false;
            RightInputBlock = false;
            LeftInputBlock = false;
            
        }else
        {
            if(GameMangerInput.inputCheck.isPressingUP())
            {
                UpInputBlock = false;
                DownInputBlock = true;
            }

            if(GameMangerInput.inputCheck.isPressingDown())
            {
                DownInputBlock = false;
                UpInputBlock = true;
            }

            if(GameMangerInput.inputCheck.isPressingRight())
            {
                RightInputBlock = false;
                LeftInputBlock = true;
            }

            if(GameMangerInput.inputCheck.isPressingLeft())
            {
                LeftInputBlock = false;
                RightInputBlock = true;
            }

        }
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
    }

    
    // keep presseing
    public void UPPressed()
    {
        if(!InputOk ) return;
        if(UpInputBlock) return;
        VerticalMoveSpeed = 1f;
    }

    public void DownPressed()
    {
        if(!InputOk ) return;
        if(DownInputBlock) return;
        VerticalMoveSpeed = -1f;
    }

    public void RightPressed()
    {
        if(!InputOk ) return;
        if(RightInputBlock) return;
        HorizontalMoveSpeed =  1f;
    }

    public void LeftPressed()
    {
        if(!InputOk ) return;
        if(LeftInputBlock) return;
        HorizontalMoveSpeed =  -1f;
    }
    // keep presseing

    public void UPJustPressed()
    {
        if(GameMangerInput.inputCheck.isPressingDown())
        {
            DownInputBlock = true;
        }
        UpInputBlock = false;
    }

    public void DownJustPressed()
    {
        if(GameMangerInput.inputCheck.isPressingUP())
        {
            UpInputBlock = true;
        } 
        DownInputBlock = false;
    }

    public void RightJustPressed()
    {
        if(GameMangerInput.inputCheck.isPressingLeft())
        {
            LeftInputBlock = true;
        } 
        RightInputBlock = false;
    }

    public void LeftJustPressed()
    {
        if(GameMangerInput.inputCheck.isPressingRight())
        {
            RightInputBlock = true;
        } 
        LeftInputBlock = false;
    }


    // just the time release the button
    public void UPJustReleased()
    {
        DownInputBlock = false;
    }

    public void DownJustReleased()
    {        
        UpInputBlock = false;
    }

    public void RightJustReleased()
    {        
        LeftInputBlock = false;
    }

    public void LeftJustReleased()
    {        
        RightInputBlock = false;
    }
    // just the time release the button

    //////////////////////delegate player input functions///////////////////////////////////

}
