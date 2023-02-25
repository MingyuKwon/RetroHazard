using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    private Player player;


    [Header("changeable stat")]
    public float moveSpeed = 1f;
    public bool canMove = true;

    public bool InputOk;

    public bool UpInputBlock;
    public bool DownInputBlock;
    public bool RightInputBlock;
    public bool LeftInputBlock;


    [Header("Debug stat")]
    [SerializeField] private float SpeedScholar;
    private float moveForceSchloar = 0f;

    [SerializeField] float HorizontalMoveSpeed;
    [SerializeField] float VerticalMoveSpeed;

    float HorizontalMoveSpeedFinal;
    float VerticalMoveSpeedFinal;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);

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
    private void FixedUpdate() {

        if(canMove && !(GameManager.instance.isPlayerPaused))
        {
            InputOk = true;

            if(!player.GetButton("Move Right") && !player.GetButton("Move Left"))
            {
                HorizontalMoveSpeed = 0f;
            }

            if(!player.GetButton("Move Up") && !player.GetButton("Move Down"))
            {
                VerticalMoveSpeed = 0f;
            }

            if(VerticalMoveSpeed != 0f && HorizontalMoveSpeed != 0f)
            {
                moveForceSchloar = 1 / Mathf.Sqrt(HorizontalMoveSpeed * HorizontalMoveSpeed + VerticalMoveSpeed * VerticalMoveSpeed);
            }else
            {
                moveForceSchloar = 1f;
            }
            
            VerticalMoveSpeedFinal =  VerticalMoveSpeed * moveSpeed * Time.fixedDeltaTime * moveForceSchloar;
            HorizontalMoveSpeedFinal = HorizontalMoveSpeed * moveSpeed * Time.fixedDeltaTime * moveForceSchloar;

            transform.position = new Vector2( transform.position.x + HorizontalMoveSpeedFinal , transform.position.y + VerticalMoveSpeedFinal);
        }else
        {
            InputOk = false;
        }
    }

        // keep presseing
    void UPPressed(InputActionEventData data)
    {
        if(!InputOk ) return;
        if(UpInputBlock) return;
        VerticalMoveSpeed = 1f;
    }

    void DownPressed(InputActionEventData data)
    {
        if(!InputOk ) return;
        if(DownInputBlock) return;
        VerticalMoveSpeed = -1f;
    }

    void RightPressed(InputActionEventData data)
    {
        if(!InputOk ) return;
        if(RightInputBlock) return;
        HorizontalMoveSpeed =  1f;
    }

    void LeftPressed(InputActionEventData data)
    {
        if(!InputOk ) return;
        if(LeftInputBlock) return;
        HorizontalMoveSpeed =  -1f;
    }
    // keep presseing

    void UPJustPressed(InputActionEventData data)
    {
        if(player.GetButton("Move Down"))
        {
            DownInputBlock = true;
        }
        UpInputBlock = false;
    }

    void DownJustPressed(InputActionEventData data)
    {
        if(player.GetButton("Move Up"))
        {
            UpInputBlock = true;
        } 
        DownInputBlock = false;
    }

    void RightJustPressed(InputActionEventData data)
    {
        if(player.GetButton("Move Left"))
        {
            LeftInputBlock = true;
        } 
        RightInputBlock = false;
    }

    void LeftJustPressed(InputActionEventData data)
    {
        if(player.GetButton("Move Right"))
        {
            RightInputBlock = true;
        } 
        LeftInputBlock = false;
    }


    // just the time release the button
    void UPJustReleased(InputActionEventData data)
    {
        DownInputBlock = false;
    }

    void DownJustReleased(InputActionEventData data)
    {        
        UpInputBlock = false;
    }

    void RightJustReleased(InputActionEventData data)
    {        
        LeftInputBlock = false;
    }

    void LeftJustReleased(InputActionEventData data)
    {        
        RightInputBlock = false;
    }
    // just the time release the button

    

}
