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
    [SerializeField] float LastXVelocity;
    [SerializeField] float LastYVelocity;

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
            animator.SetBool("Slash", isAttacking);
        }
    }

    public void SlashStart()
    {
        Debug.Log("Slash Start");
        Debug.Log(transform.name);
        transform.position = new Vector3(transform.position.x, transform.position.y - pixelRetouch, transform.position.z);
    }

    public void SlashEnd()
    {
        Debug.Log("Slash End");
        isAttacking = false;
        pm.canMove = true;
        transform.position = new Vector3(transform.position.x, transform.position.y + pixelRetouch, transform.position.z);
        animator.SetBool("Slash", isAttacking);
        
    }

    private void OnDestroy() {
        player.RemoveInputEventDelegate(OnAttackPressed);
    }
}
