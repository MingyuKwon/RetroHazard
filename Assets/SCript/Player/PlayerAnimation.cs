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
    [SerializeField] float pixelRetouch;

    [Header("For Debug")]
    [SerializeField] bool isAttacking;
    [SerializeField] float XVelocity;
    [SerializeField] float YVelocity;
    [SerializeField] float LastXVelocity = 0f;
    [SerializeField] float LastYVelocity = -1f;
    

    private Player player;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        pm = GetComponentInParent<PokemonPlayerMove>();

        player = ReInput.players.GetPlayer(0);
        player.AddInputEventDelegate(OnAttackPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed , "Attack");
    }

    void Update()
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

    private void OnAttackPressed(InputActionEventData data)
    {
        if(!isAttacking)
        {
            isAttacking = true;
            pm.canMove = false;
            animator.SetBool("Stab", isAttacking);
        }
    }

    public void SlashStart()
    {
        Debug.Log("Slash Start");
    }

    public void SlashEnd()
    {
        Debug.Log("Slash End");
        isAttacking = false;
        pm.canMove = true;
        animator.SetBool("Slash", isAttacking);
        
    }

    public void StabStart()
    {
        Debug.Log("Stab Start");
    }

    public void StabEnd()
    {
        Debug.Log("Stab End");
        isAttacking = false;
        pm.canMove = true;
        animator.SetBool("Stab", isAttacking);
        
    }

    private void OnDestroy() {
        player.RemoveInputEventDelegate(OnAttackPressed);
    }
}
