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
    [InfoBox("[KeyItem]\n\n0 : Energy1, 1 : Energy2, 2 : Energy3, 3 : normal Sheild, 4 : parry Sheild, 5 : Big Sheild, 6: energy1 expansion, 7: energy2 expansion, 8: energy3 expansion")]
    [InfoBox("\n9 : Energy1-1Upgrade, 10 : Energy2-1Upgrade, 11 : Energy3-1Upgrade, 12 : Energy1-2Upgrade, 13 : Energy2-2Upgrade, 14 : Energy3-2Upgrade , 15 : inventory expansion")]

    [InfoBox("[Interactive Item]\n100 : medium key\n200 : Nitroglycerin")]
    public int KeyItemCode;

    [InfoBox("[NormalItem]\n\n0 : heal potion, 1 : upgrade porion, 2 : double heal potion, 3 : elixer potion \n 4 : energy1 bullet, 5 : energy2 bullet, 6 : energy3 bullet, 7 : sheild bullet")]
    public int NormalItemCode;

    [Header("combine with")]
    public int[] combineItems;
    public ItemInformation[] combinResultItems;

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
