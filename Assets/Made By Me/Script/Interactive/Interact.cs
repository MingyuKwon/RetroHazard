using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject check;
    public InteractiveDialog dialog;
    public bool isItem;
    public bool isKeyItem;
    Item item = null;

    Collider2D playerCollider2D;

    private void Awake() {
        check.SetActive(false);

        item = transform.parent.GetComponent<Item>();

        if(item == null)
        {
            isItem = false;
        }else
        {
            isItem = true;
            isKeyItem = item.information.isKeyItem;
        }
        
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            playerCollider2D = other;
            check.SetActive(true);
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
            transform.parent.gameObject.GetComponent<KeyItem>().ObtainKeyItem(playerCollider2D);
        }else
        {
            GameManagerUI.instance.Visualize_Tab_Obtain(true , transform.parent.gameObject.GetComponent<bulletItem>());
        }
    }
}
