using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class bulletItem : Item
{
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_BulletItem_Destroy[transform.GetSiblingIndex()])
        {
            this.gameObject.SetActive(false);
        }
        spriteRenderer.sprite = information.ItemImage;

    }

    public void ObtainBulletItem()
    {        
        GameManager.EventManager.Invoke_Obtain_bullet_Item_Event(information, information.amount);
        SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_BulletItem_Destroy[transform.GetSiblingIndex()] = true;
        this.gameObject.SetActive(false);

        if(noticeIndex_ifExist != 0)
        {
            GameManager.EventManager.InvokeInteractNoticeEvent(noticeIndex_ifExist, true);
        }
    }
}
