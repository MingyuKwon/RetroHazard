using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

using UnityEngine;

[CreateAssetMenu(menuName = "Create Interact Dialog", fileName = " Dialog")]
public class InteractiveDialog : ScriptableObject
{
    public string Interactive_name;
    public string Interactive_nameKorean;

    [InfoBox("[Standard InteractCode]\n100 : level 0 key\n101 : level 1 key\n102 : level 2 key\n103 : level 3 key\n110 : WW 40")]
    [InfoBox("[Tutorial InteractCode]\n200 : Tutorial Rock Pile\n201 : Historic Site Gate")]
    [InfoBox("[yangHan InteractCode]\n300 : catacombs gate\n")]
    public int InteractCode;

    public bool isItemRemainAfterInteract;

    [InfoBox("[Standard Interactive Item]\n100 : level 0 key\n101 : level 1 key\n102 : level 2 key\n103 : level 3 key\n110 : WW 40")]
    [InfoBox("[Tutorial Interactive Item]\n200 : Nitroglycerin\n")]
    [InfoBox("[yangHan Interactive Item]\n300 : catacombs Key\n")]
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
