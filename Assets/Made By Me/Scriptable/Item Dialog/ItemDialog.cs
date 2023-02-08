using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Item Dialog", fileName = " Dialog")]
public class ItemDialog : ScriptableObject
{
    public string ItemName;
    [Space]

    [Header("Just Select One")]
    public bool isKeyItem;
    public bool isNormalItem;

    [Space]
    [TextArea]
    public string[] ItemDescription;

}
