using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HiddenInteract : RealInteract
{
    static public HiddenInteract instance = null;

    static public bool nowHiddenInteracting = false;
    static public bool nowYouSeeInTabisHidden = false;
    private void Start() {
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Interact_Destroy[transform.GetSiblingIndex()])
        {
            gameObject.SetActive(false);

            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

            if(boxCollider2D != null)
            {
                Destroy(boxCollider2D);
            }

        }else
        {
            Debug.Log("Hidden Interact name : " + this.gameObject.name);

            gameObject.SetActive(true);
        }
    }

    public override void OnEnable() {
    }

    public override void OnDisable() {
    }

    public void Hidden_Interact()
    {
        GameManagerUI.instance.SetInteractiveDialogText(dialog.SucessDialog);
        GameManagerUI.instance.VisualizeInteractiveUI(true);
        nowHiddenInteracting = true;
        StartCoroutine(isDialogEnd());
    }

    IEnumerator isDialogEnd()
    {
        while(nowHiddenInteracting)
        {
            yield return new WaitForEndOfFrame();
        }   

        nowYouSeeInTabisHidden = true;

        if(instance == null)
        {
            instance = this;
        }else
        {
            Debug.Log("SOMETHING IS WRONG. HIDEINTERACT INSTANCE SHOULD BE NULL");
        }
        

        GameObject tempObject = new GameObject("TempObject");

        if(item.information.isBullet)
        {
            bulletItem tempbulletItem = tempObject.AddComponent<bulletItem>();
            tempbulletItem.Init(item.information);
            GameManagerUI.instance.Visualize_Tab_Obtain(true , tempbulletItem);
        }else if(item.information.isPotion)
        {
            PotionItem tempPotionItem = tempObject.AddComponent<PotionItem>();
            tempPotionItem.Init(item.information);
            GameManagerUI.instance.Visualize_Tab_Obtain(true , tempPotionItem );
        }else if(item.information.isInteractiveItem)
        {
            InteractKeyItem tempKeyItem = tempObject.AddComponent<InteractKeyItem>();
            tempKeyItem.Init(item.information);
            GameManagerUI.instance.Visualize_Tab_Obtain(true , tempKeyItem);
        }
    }

    public void RemoveHiddenInteract()
    {
        if(check.activeInHierarchy)
        {
            SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Interact_Destroy[transform.GetSiblingIndex()] = true;
            gameObject.SetActive(false);

            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

            if(boxCollider2D != null)
            {
                Destroy(boxCollider2D);
            }

            if(noticeIndex_ifExist != 0)
            {
                GameManager.EventManager.InvokeInteractNoticeEvent(noticeIndex_ifExist, true);
            }
            
        }
    }
}
