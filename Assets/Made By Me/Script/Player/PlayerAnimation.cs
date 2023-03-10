using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    private Animator vfxAnimator;
    private Rigidbody2D rb;
    private PlayerMove pm;
    private PlayerStatus status;

    [Header("changable")]

    public bool sheildCrash;

    [Header("For Debug")]
    public bool isAttacking;
    public bool isSheilding; 
    public bool isParrying;    
    public bool isWalkingPress;

    [Space]
    public float LastXInput = 0f;
    public float LastYInput = -1f;
    public float XInput = 0f;
    public float YInput = 0f;
    public float BeforePauseXInput = 0f;
    public float BeforePauseYInput = 0f;
    

    private Player player;

    private void Awake() {
        animator = GetComponent<Animator>();
        vfxAnimator = GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        status = GetComponentInChildren<PlayerStatus>();

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

    private void OnDestroy() {
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

    private void OnEnable() {
        PlayerStatus.SheildCrashEvent += SetSheildCrash;
        PlayerStatus.SheildRecoveryEvent += SetSheildRecovery;
    }

    private void OnDisable() {
        PlayerStatus.SheildCrashEvent -= SetSheildCrash;
        PlayerStatus.SheildRecoveryEvent -= SetSheildRecovery;
    }

    private void SetSheildCrash(bool ChangeSheild)
    {
        sheildCrash = true;
        isSheilding = false;
        animator.SetBool("Shield", isSheilding);
    }

    private void SetSheildRecovery(bool ChangeSheild)
    {
        sheildCrash = false;
        GameManager.instance.SetPlayerMove(true);
    }

    void Update()
    {
        GameManager.instance.isPlayerSheilding = isSheilding;
        isWalkingPress = player.GetButton("Move Up") || player.GetButton("Move Down") || player.GetButton("Move Right") || player.GetButton("Move Left");
        if(GameManager.instance.isPlayerPaused) return;
        
        SetWalkAnimation();
        SetAttackAnimation();
        SetShieldAnimation();
        SetParryAnimation();
    }

    private void SetWalkAnimation()
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

            animator.SetFloat("Energy", status.Energy);
            animator.SetTrigger("Attack");
            isAttacking = true;
            GameManager.instance.SetPlayerMove(false);
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

    public void SetPlayerAnimationIdle()
    {
        SetPlayerFree();
        status.parryFrame = false;
        XInput = 0f;
        YInput = 0f;
    }

    public void SetPlayerFree()
    {
        isAttacking = false;
        isParrying = false;
        isSheilding = false;
        animator.ResetTrigger("Block");
        animator.ResetTrigger("Parry");
    }

    //Animation event
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

    //Animation event

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
}
