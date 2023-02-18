using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject check;
    public InteractiveDialog dialog;
    public bool isItem;
    public bool isKeyItem;

    public bool isInventoryBox;

    public bool triggerCheckActive = true;

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

    public void SetCheckActive(bool flag)
    {
        check.gameObject.SetActive(flag);
        triggerCheckActive = flag;
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            playerCollider2D = other;

            if(triggerCheckActive)
            {
                check.SetActive(true);
            }else
            {
                check.SetActive(false);
            }
            
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
            if(item.information.isBullet)
            {
                GameManagerUI.instance.Visualize_Tab_Obtain(true , transform.parent.gameObject.GetComponent<bulletItem>());
            }else
            {
                GameManagerUI.instance.Visualize_Tab_Obtain(true , transform.parent.gameObject.GetComponent<PotionItem>());
            }
            
        }
    }
}
