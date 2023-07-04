using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class EquipItem : KeyItem
{
    private void Start() {
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_EquipItem_Destroy[transform.GetSiblingIndex()])
        {
            this.gameObject.SetActive(false);
        }
        spriteRenderer.sprite = information.ItemImage;

    }

    public override void EventInvokeOverride()
    {
        SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_EquipItem_Destroy[transform.GetSiblingIndex()] = true;
        GameManager.EventManager.Invoke_Obtain_Equip_Item_Event(information, information.KeyItemCode);
        this.gameObject.SetActive(false);

        if(noticeIndex_ifExist != 0)
        {
            GameManager.EventManager.InvokeInteractNoticeEvent(noticeIndex_ifExist, true);
        }
    }
}
