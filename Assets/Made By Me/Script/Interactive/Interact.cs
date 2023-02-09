using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject check;
    public InteractiveDialog dialog;
    public bool isItem;
    public bool isKeyItem;

    Collider2D playerCollider2D;

    private void Start() {
        check.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            check.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            playerCollider2D = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            check.SetActive(false);
        }
    }

    public void ObtainItem()
    {
        if(isKeyItem)
        {
            transform.parent.gameObject.GetComponent<ExpansionItem>().ObtainKeyItem(playerCollider2D);
        }else
        {
            GameManagerUI.instance.Visualize_Tab_Obtain(true , transform.parent.gameObject.GetComponent<bulletItem>());
        }
    }
}
