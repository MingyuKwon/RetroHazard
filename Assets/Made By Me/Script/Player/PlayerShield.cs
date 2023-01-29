using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;


public class PlayerShield : MonoBehaviour
{
    private CapsuleCollider2D playerBodyCollider;
    PlayerStatus stat;
    private Animator vfxAnimator;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        vfxAnimator = GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>();
        stat = GetComponentInChildren<PlayerStatus>();
        playerBodyCollider = GetComponentInChildren<PlayerCollider>().gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.otherCollider.tag == "Sheild")
        {
            if(other.GetContact(0).collider.tag == "Attack" && stat.parryFrame)
            {
                vfxAnimator.SetTrigger("Parry");
                GameManager.instance.SlowMotion();
            }

            animator.SetTrigger("Block");
        }
        
    }

    public void ParryFrameStart()
    {
        stat.parryFrame = true;
    }

    public void ParryFrameEnd()
    {
        stat.parryFrame = false;
    }
}
