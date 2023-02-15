using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Item Information", fileName = " Item Information")]
public class ItemInformation : ScriptableObject
{
    public Sprite ItemImage;
    public string ItemName;
    [Space]

    [Header("Interact with Item when first obtain? (Just Select One)")]
    public bool isKeyItem;
    public bool isNormalItem;

    [Header("Is Key Item for equip? interact? expand? (Just Select One)")]
    public bool isEquipItem;
    public bool isInteractiveItem;
    public bool isExpansionItem;
    public int KeyItemCode;

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
