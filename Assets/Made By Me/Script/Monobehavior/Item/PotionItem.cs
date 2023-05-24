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
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_PotionItem_Destroy[transform.GetSiblingIndex()])
        {
            this.gameObject.SetActive(false);
        }
        spriteRenderer.sprite = information.ItemImage;

    }

    public void ObtainPotionItem()
    {      
        SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_PotionItem_Destroy[transform.GetSiblingIndex()] = true;
        GameManager.EventManager.Invoke_Obtain_potion_Item_Event(information);
        this.gameObject.SetActive(false);
    }

}


    
