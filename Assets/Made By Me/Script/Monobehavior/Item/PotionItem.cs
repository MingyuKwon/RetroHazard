using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PotionItem : Item
{
    public static event Action<ItemInformation> Obtain_potion_Item_Event;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_PotionItem_Destroy[transform.GetSiblingIndex()])
        {
            this.gameObject.SetActive(false);
        }
        spriteRenderer.sprite = information.ItemImage;

    }

    public void ObtainPotionItem()
    {      
        SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_PotionItem_Destroy[transform.GetSiblingIndex()] = true;
        Obtain_potion_Item_Event?.Invoke(information);
        this.gameObject.SetActive(false);
    }

}


    
