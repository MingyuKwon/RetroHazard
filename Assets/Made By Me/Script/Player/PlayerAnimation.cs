using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PokemonPlayerMove pm;

    [Header("changable")]

    [Header("For Debug")]
    [SerializeField] bool isAttacking;
    [SerializeField] bool isSheilding; 
    [SerializeField] bool isParrying;

    [SerializeField] float AttackKind  = 0f;

    [Space]
    [SerializeField] float XVelocity;
    [SerializeField] float YVelocity;
    [SerializeField] float LastXInput = 0f;
    [SerializeField] float LastYInput = -1f;
    

    private Player player;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PokemonPlayerMove>();

        player = ReInput.players.GetPlayer(0);
        player.AddInputEventDelegate(UPPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed, "Move Up");
        player.AddInputEventDelegate(DownPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed, "Move Down");
        player.AddInputEventDelegate(RightPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed,"Move Right");
        player.AddInputEventDelegate(LeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed,"Move Left");
    }

    void Update()
    {
        if(GameManager.instance.isPlayerPaused) return;
        
        SetWalkAnimation();
        SetAttackAnimation();
        SetShieldAnimation();
        SetParryAnimation();
    }

    private void SetPlayerMove(bool flag)
    {
        pm.canMove = flag;
    }

    private void SetWalkAnimation()
    {
        XVelocity = rb.velocity.x;
        YVelocity = rb.velocity.y;

        animator.SetFloat("XInput", XVelocity);
        animator.SetFloat("YInput", YVelocity);
        animator.SetFloat("LastXInput", LastXInput);
        animator.SetFloat("LastYInput", LastYInput);
    }

    private void SetAttackAnimation()
    {
        if(player.GetButtonDown("Attack"))
        {
            if(isAttacking) return;
            if(isSheilding) return;
            if(isParrying) return;

            animator.SetFloat("AttackKind", AttackKind);
            animator.SetTrigger("Attack");
            isAttacking = true;
            SetPlayerMove(false);
        }
    }

    private void SetShieldAnimation()
    {
        if(isParrying) return;

        if(player.GetButton("Shield"))
        {
            SetPlayerMove(false);
        }

        if(player.GetButtonUp("Shield"))
        {
            SetPlayerMove(true);
        }


        isSheilding = player.GetButton("Shield");
        animator.SetBool("Shield", isSheilding);
          
    }

    private void SetParryAnimation()
    {
        if(player.GetButton("Shield") && player.GetButtonDown("Interactive"))
        {
            if(isParrying) return;
            animator.SetTrigger("Parry");
            isParrying = true;
        }
    }

    //input Aniamtion Event
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
    //input Aniamtion Event



    //Animation event
    public void SlashStart()
    {

    }

    public void SlashEnd()
    {
        isAttacking = false;
        SetPlayerMove(true);
    }

    public void StabStart()
    {
    }

    public void StabEnd() 
    {
        isAttacking = false;
        SetPlayerMove(true);
    }

    public void ParryStart()
    {

    }

    public void ParryFrameStart()
    {

    }

    public void ParryFrameEnd()
    {

    }

    public void ParryEnd()
    {
        isParrying = false;
        SetPlayerMove(true);
    }

    //Animation event

    private void OnDestroy() {
    }
}
