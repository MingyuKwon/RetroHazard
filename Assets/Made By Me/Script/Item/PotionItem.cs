using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;

public class PotionItem : Item
{
    public static event Action<ItemInformation> Obtain_potion_Item_Event;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        spriteRenderer.sprite = information.ItemImage;
    }

    public void ObtainPotionItem()
    {      
        Obtain_potion_Item_Event?.Invoke(information);
        Destroy(this.gameObject);
    }
}
