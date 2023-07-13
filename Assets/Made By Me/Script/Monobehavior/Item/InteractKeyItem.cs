using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractKeyItem : KeyItem
{
    private void Start() {

        if(HiddenInteract.nowYouSeeInTabisHidden)
        {
            return;
        }

        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_KeyItem_Destroy[transform.GetSiblingIndex()])
        {
            this.gameObject.SetActive(false);
        }
        spriteRenderer.sprite = information.ItemImage;

    }

    public override void EventInvokeOverride()
    {
        GameManager.EventManager.Invoke_Obtain_RealKey_Item_Event(information);
        if(HiddenInteract.nowYouSeeInTabisHidden)
        {
            HiddenInteract.instance.RemoveHiddenInteract();
            HiddenInteract.instance = null;
            Destroy(this);
            return;
        }

        SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_KeyItem_Destroy[transform.GetSiblingIndex()] = true;
        this.gameObject.SetActive(false);

        if(noticeIndex_ifExist != 0)
        {
            GameManager.EventManager.InvokeInteractNoticeEvent(noticeIndex_ifExist, true);
        }
    }
}
