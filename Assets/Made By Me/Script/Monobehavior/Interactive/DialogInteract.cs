using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteract : MonoBehaviour
{
    [SerializeField] GameObject check;

    static public bool isTalking = false;

    bool flag = false;

    private void Awake() {
        check.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            flag = true;
        }
    }

    private void Update() {
        if(!flag) return;

            if(isTalking)
            {
                check.SetActive(false);
            }else
            {
                check.SetActive(true);
            }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            flag = false;
            check.SetActive(false);
        }
    }
}
