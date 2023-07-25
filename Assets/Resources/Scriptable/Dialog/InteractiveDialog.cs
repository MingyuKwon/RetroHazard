using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

using UnityEngine;

[CreateAssetMenu(menuName = "Create Interact Dialog", fileName = " Dialog")]
public class InteractiveDialog : ScriptableObject
{
    public string Interactive_name;
    public string Interactive_nameKorean;
    [InfoBox("[Standard InteractCode]\n0 : Medium key-hole Door")]
    [InfoBox("[Tutorial InteractCode]\n200 : Tutorial Rock Pile\n201 : Historic Site Gate")]
    public int InteractCode;


    [InfoBox("[Standard Interactive Item]\n100 : medium key")]
    [InfoBox("[Tutorial Interactive Item]\n200 : Nitroglycerin\n201 : Historic Gate Key")]
    public int[] InteractKeyItems;

    [TextArea]
    public string Interactive_Situation;
    [TextArea]
    public string Interactive_SituationKorean;

    [TextArea]
    public string[] SucessDialog;
    [TextArea]
    public string[] SucessDialogKorean;

}
