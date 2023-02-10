using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Item Information", fileName = " Item Information")]
public class ItemInformation : ScriptableObject
{
    public Sprite ItemImage;
    public string ItemName;
    [Space]

    [Header("Just Select One")]
    public bool isKeyItem;
    public bool isNormalItem;

    [Header("Just Select One")]
    public bool isSheild;
    public bool isEnergy1;
    public bool isEnergy2;
    public bool isEnergy3;

    [Space]
    [TextArea]
    public string[] ItemDescription;

}
