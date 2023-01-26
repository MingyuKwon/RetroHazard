using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float reflectForceScholar = 300f;

    PlayerStatus status;
    private Animator animator;
    Rigidbody2D rb;

    private void Awake() {
        status = GetComponentInChildren<PlayerStatus>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.otherCollider.tag == "Player Body" && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log(other.GetContact(0).normal);
            rb.AddForce(other.GetContact(0).normal * reflectForceScholar);
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
