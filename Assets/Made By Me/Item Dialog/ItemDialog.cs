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
    public bool isInteractiveItem;

    [Space]
    [TextArea]
    public string[] ItemDescription;
    public string[] InteractiveDialog;


}
