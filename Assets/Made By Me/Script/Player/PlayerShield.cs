using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;


public class PlayerShield : MonoBehaviour
{
    PlayerStatus stat;
    private Animator vfxAnimator;

    private void Awake() {
        vfxAnimator = GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>();
        stat = GetComponentInChildren<PlayerStatus>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("enemy : " + other.GetContact(0).collider);
        Debug.Log("player : " + other.otherCollider);

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
