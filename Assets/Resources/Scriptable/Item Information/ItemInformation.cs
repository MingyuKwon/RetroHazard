using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Create Item Information", fileName = " Item Information")]
public class ItemInformation : ScriptableObject
{
    public string itemPath;
    public Sprite ItemImage;
    public string ItemName;
    public string ItemNameKorean;
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

    [InfoBox("[Standard Interactive Item]\n100 : medium key")]
    [InfoBox("[Tutorial Interactive Item]\n200 : Nitroglycerin\n201 : Historic Gate Key")]
    [InfoBox("[yangHan Interactive Item]\n300 : catacombs Key\n")]
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
    [TextArea]
    public string[] ItemDescriptionKorean;



}
