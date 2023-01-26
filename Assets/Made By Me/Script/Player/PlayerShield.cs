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

    private void Awake() {
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
