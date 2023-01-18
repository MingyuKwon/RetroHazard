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

    [Space]
    [SerializeField] float XVelocity;
    [SerializeField] float YVelocity;
    [SerializeField] float LastXVelocity = 0f;
    [SerializeField] float LastYVelocity = -1f;
    

    private Player player;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PokemonPlayerMove>();

        player = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
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

        if(XVelocity != 0f)
        {
            if(XVelocity < 0)
            {
                LastXVelocity = -1f;
                LastYVelocity = 0f;
            }else
            {
                LastXVelocity = 1f;
                LastYVelocity = 0f;
            }
        }

        if(YVelocity != 0f)
        {
            if(YVelocity < 0)
            {
                LastXVelocity = 0f;
                LastYVelocity = -1f;
            }else
            {
                LastXVelocity = 0f;
                LastYVelocity = 1f;
            }
        }

        animator.SetFloat("XInput", XVelocity);
        animator.SetFloat("YInput", YVelocity);
        animator.SetFloat("LastXInput", LastXVelocity);
        animator.SetFloat("LastYInput", LastYVelocity);
    }

    private void SetAttackAnimation()
    {
        if(player.GetButtonDown("Attack"))
        {
            if(isAttacking) return;
            if(isSheilding) return;
            if(isParrying) return;

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
        Debug.Log("ParryStart");
    }

    public void ParryFrameStart()
    {
        Debug.Log("ParryFrameStart");
    }

    public void ParryFrameEnd()
    {
        Debug.Log("ParryFrameEnd");
    }

    public void ParryEnd()
    {
        Debug.Log("ParryEnd");
        isParrying = false;
        SetPlayerMove(true);
    }

    //Animation event

    private void OnDestroy() {
    }
}
