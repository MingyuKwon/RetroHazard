using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Interact Dialog", fileName = " Dialog")]
public class InteractiveDialog : ScriptableObject
{
    public string Interactive_name;

    [TextArea]
    public string Interactive_Situation;

    [TextArea]
    public string[] SucessDialog;

    [TextArea]
    public string[] FailDialog;

}
