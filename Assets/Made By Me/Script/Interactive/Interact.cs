using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    public GameObject check;
    public InteractiveDialog dialog;
    public bool isItem;
    public bool isKeyItem;

    public bool isInventoryBox;
    public bool isSaveSpot;

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

    private void OnEnable() {
        TabUI.Interact_KeyItem_Success_Event += interactSuccess;
    }

    private void OnDisable() {
        TabUI.Interact_KeyItem_Success_Event -= interactSuccess;
    }

    private void interactSuccess(InteractiveDialog interactiveDialog, int n)
    {
        if(!(this is RealInteract)) return;

        if(interactiveDialog.InteractCode == dialog.InteractCode)
        {
            if(check.activeInHierarchy)
            {

                Debug.Log("RealInteract");
                SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Interact_Destroy[transform.GetSiblingIndex()] = true;
                
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public void SetCheckActive(bool flag)
    {
        check.SetActive(flag);
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
