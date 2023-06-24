using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

public class DialogUI : MonoBehaviour
{
    DialogUILogic dialogUILogic;
    GameObject dialogPanel;
    GameObject speakerPanel;

    option[] Option;
    Text dialogText;
    Text speakerText;

    void Awake() {
        dialogPanel = GetComponentInChildren<DialogPanel>().gameObject;
        speakerPanel = GetComponentInChildren<SpeakerPanel>().gameObject;
        
        dialogText = dialogPanel.gameObject.GetComponentInChildren<Text>();
        speakerText = speakerPanel.gameObject.GetComponentInChildren<Text>();
        
        Option = dialogPanel.GetComponentsInChildren<option>();

        dialogUILogic = new DialogUILogic(dialogText, speakerText, Option, this);
    }

    // for question option select
    private void OnEnable() {
        dialogUILogic.OnEnable();
    }
    private void OnDisable() {
        dialogUILogic.OnDisable();
    }

    public void VisualizeDialogUI(bool flag)
    {
        dialogUILogic.VisualizeDialogUI(flag);
    }

    public void showTalkNPCDialog(bool visited ,Dialog dialog)
    {
        dialogUILogic.showTalkNPCDialog(visited, dialog);
    }

    
}
