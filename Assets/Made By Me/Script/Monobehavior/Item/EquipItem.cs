using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class EquipItem : KeyItem
{
    public static event Action<ItemInformation, int> Obtain_Equip_Item_Event;

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
        Obtain_Equip_Item_Event?.Invoke(information, information.KeyItemCode);
        this.gameObject.SetActive(false);
    }
}
