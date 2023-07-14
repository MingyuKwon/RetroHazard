using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : ForNoticeBroadCast
{
    public int itemAmount = -1;
    public ItemInformation information;
    public SpriteRenderer spriteRenderer;

    public void Init(ItemInformation info, int itemAmount)
    {
        this.itemAmount = itemAmount;
        MakeInstanceItemInformation(info); // Awake 대신 여기서 호출하도록 변경합니다.
        if(itemAmount != -1)
        {
            information.amount = itemAmount;
        }
    }

    private void MakeInstanceItemInformation(ItemInformation info)
    {
        ItemInformation newItemInformation = ScriptableObject.CreateInstance<ItemInformation>();

        newItemInformation.ItemImage = info.ItemImage;
        newItemInformation.ItemName = info.ItemName;

        newItemInformation.isKeyItem = info.isKeyItem;
        newItemInformation.isNormalItem = info.isNormalItem;

        newItemInformation.isEquipItem = info.isEquipItem;
        newItemInformation.isInteractiveItem = info.isInteractiveItem;
        newItemInformation.isExpansionItem = info.isExpansionItem;

        newItemInformation.KeyItemCode = info.KeyItemCode;
        newItemInformation.NormalItemCode = info.NormalItemCode;
        newItemInformation.combineItems = info.combineItems;
        newItemInformation.combinResultItems = info.combinResultItems;

        newItemInformation.isSheild = info.isSheild;
        newItemInformation.isEnergy1 = info.isEnergy1;
        newItemInformation.isEnergy2 = info.isEnergy2;
        newItemInformation.isEnergy3 = info.isEnergy3;

        newItemInformation.isBullet = info.isBullet;
        newItemInformation.isPotion = info.isPotion;

        newItemInformation.amount = info.amount;
        newItemInformation.expansionAmount = info.expansionAmount;
        newItemInformation.healAmount = info.healAmount;

        newItemInformation.ItemDescription = info.ItemDescription;

        information = newItemInformation;

    }

}
