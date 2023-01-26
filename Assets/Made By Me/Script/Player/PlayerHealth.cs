using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerStatus status;
    private Animator animator;

    private void Awake() {
        status = GetComponentInChildren<PlayerStatus>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.otherCollider.tag == "Player Body" && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            animator.SetTrigger("Stun");
        }
        
    }

    public void StunStart()
    {
        GameManager.instance.isPlayerPaused = true;
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.SetPlayerAnimationBool(false);
    }

    public void StunEnd()
    {
        GameManager.instance.isPlayerPaused = false;
    }
}
