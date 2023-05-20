using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExpansionItem : KeyItem
{
    public static event Action<ItemInformation> Obtain_Expansion_Item_Event;

    private void Start() {
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_ExpansionItem_Destroy[transform.GetSiblingIndex()])
        {
            this.gameObject.SetActive(false);
        }
        spriteRenderer.sprite = information.ItemImage;

    }

    public override void EventInvokeOverride()
    {
        SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_ExpansionItem_Destroy[transform.GetSiblingIndex()] = true;
        Obtain_Expansion_Item_Event?.Invoke(information);
        this.gameObject.SetActive(false);
    }
}
