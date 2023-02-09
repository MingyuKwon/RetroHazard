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

    [Space]
    [TextArea]
    public string[] ItemDescription;

}
