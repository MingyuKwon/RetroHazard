using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Create Item Information", fileName = " Item Information")]
public class ItemInformation : ScriptableObject
{
    public Sprite ItemImage;
    public string ItemName;
    [Space]

    [Header("Interact with Item when first obtain? (Just Select One)")]
    public bool isKeyItem;
    public bool isNormalItem;

    [Header("Is Key Item for equip? interact? expand? (If item is KeyItem, Just Select One. If not, Select Nothing)")]
    public bool isEquipItem;
    public bool isInteractiveItem;
    public bool isExpansionItem;
    

    [Header("Item Code. Key item is seperated by upward Bool flag, bullet items is integrate and start from 0")]
    [InfoBox("[KeyItem]\n\n[isEquipItem] 0 : Energy1, 1 : Energy2, 2 : Energy3, 3 : normal Sheild, 4 : parry Sheild, 5 : Big Sheild")]
    public int KeyItemCode;

    [InfoBox("[NormalItem]\n\n0 : heal potion, 1 : upgrade porion, 2 : double heal potion, 3 : elixer potion")]
    public int NormalItemCode;

    [Header("combine with")]
    public int[] combineItems;

    [Header("what kind of item? (Just Select One)")]
    public bool isSheild;
    public bool isEnergy1;
    public bool isEnergy2;
    public bool isEnergy3;

    [Header("if it is bullet item, can be used in Menu tab? (Just Select One)")]
    public bool isBullet;
    public bool isPotion;

    [Header("amount")]
    public int amount;
    public int expansionAmount;
    public int healAmount;

    [Space]
    [TextArea]
    public string[] ItemDescription;



}
