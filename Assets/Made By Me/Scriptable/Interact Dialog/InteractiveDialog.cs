using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

using UnityEngine;

[CreateAssetMenu(menuName = "Create Interact Dialog", fileName = " Dialog")]
public class InteractiveDialog : ScriptableObject
{
    public string Interactive_name;
    [InfoBox("[InteractCode]\n0 : Medium key-hole Door")]
    public int InteractCode;


    [InfoBox("[Interactive Item]\n100 : medium key")]
    public int[] InteractKeyItems;

    [TextArea]
    public string Interactive_Situation;

    [TextArea]
    public string[] SucessDialog;

}
