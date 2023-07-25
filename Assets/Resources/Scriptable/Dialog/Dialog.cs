using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create NPC's Dialog", fileName = "'s Dialog")]
public class Dialog : ScriptableObject
{
    public string NPCname;
    public string NPCnameKorean;

    public bool hasFirstEncounterDialog;
    public bool hasChoiceDialog;
    public int ChoiceQuestionQuantity;

    [TextArea]
    public string[] FirstEnCountDialogs;
    [TextArea]
    public string[] FirstEnCountDialogsKorean;

    [TextArea]
    public string[] ReapeatingDialogs;
    [TextArea]
    public string[] ReapeatingDialogsKorean;

    [TextArea]
    public string[] EventDialogs;
    [TextArea]
    public string[] EventDialogsKorean;

    [TextArea]
    public string[] ChoiceDialog;
    [TextArea]
    public string[] ChoiceDialogKorean;

    [TextArea]
    public string[] option1Dialog;
    [TextArea]
    public string[] option1DialogKorean;

    [TextArea]
    public string[] option2Dialog;
    [TextArea]
    public string[] option2DialogKorean;

    [TextArea]
    public string[] option3Dialog;
    [TextArea]
    public string[] option3DialogKorean;

    
}
