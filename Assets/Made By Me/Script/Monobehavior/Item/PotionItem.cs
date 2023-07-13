using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PotionItem : Item
{
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        if(HiddenInteract.nowYouSeeInTabisHidden)
        {
            return;
        }

        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_PotionItem_Destroy[transform.GetSiblingIndex()])
        {
            this.gameObject.SetActive(false);
        }
        spriteRenderer.sprite = information.ItemImage;

    }

    public void ObtainPotionItem()
    {      
        GameManager.EventManager.Invoke_Obtain_potion_Item_Event(information);

        if(HiddenInteract.nowYouSeeInTabisHidden)
        {
            HiddenInteract.instance.RemoveHiddenInteract();
            HiddenInteract.instance = null;
            Destroy(this);
            return;
        }

        SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_PotionItem_Destroy[transform.GetSiblingIndex()] = true;
        this.gameObject.SetActive(false);

        if(noticeIndex_ifExist != 0)
        {
            GameManager.EventManager.InvokeInteractNoticeEvent(noticeIndex_ifExist, true);
        }
    }

}


    
