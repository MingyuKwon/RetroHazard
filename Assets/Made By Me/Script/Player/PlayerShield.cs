using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;


public class PlayerShield : MonoBehaviour
{
    private CapsuleCollider2D playerBodyCollider;
    PlayerStatus status;
    private Animator vfxAnimator;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        vfxAnimator = GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>();
        status = GetComponentInChildren<PlayerStatus>();
        playerBodyCollider = GetComponentInChildren<PlayerCollider>().gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.otherCollider.tag == "Sheild" && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(other.GetContact(0).collider.tag == "Attack" && status.parryFrame)
            {
                vfxAnimator.SetTrigger("Parry");
                GameManager.instance.SlowMotion();
            }else
            {
                animator.SetTrigger("Block");
                status.blockSuccessEnemy = other.GetContact(0).collider.transform.parent.transform.parent.name;
            }
            
        }
        
    }

    public void ParryFrameStart()
    {
        status.parryFrame = true;
    }

    public void ParryFrameEnd()
    {
        status.parryFrame = false;
    }

    public void BlockStart()
    {
        GameManager.instance.SetPlayerMove(false);
    }

    public void BlockEnd()
    {
        GameManager.instance.SetPlayerMove(true);
        status.blockSuccessEnemy = null;
    }
}
