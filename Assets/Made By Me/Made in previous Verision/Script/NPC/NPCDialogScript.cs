using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;


public class NPCDialogScript : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    PlayerInput playerInput;

    [SerializeField] int callCount = 0;
    [SerializeField] bool visited = false;
    [SerializeField] bool isChatting = false;

    [SerializeField] bool isShowingOption = false;
    [SerializeField] int SelectOptionindex = 0;

    private void Awake() {
        playerInput = this.GetComponent<PlayerInput>();
        playerInput.enabled = false;
    }

    void OnUP(InputValue value)
    {
        if(isShowingOption)
        {
            SelectOptionindex--;
        }
    }
    void OnDown(InputValue value)
    {
        if(isShowingOption)
        {
            SelectOptionindex++;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        GameManager.instance.isPlayerNearNPC = true;
        playerInput.enabled = true;
    }
    void OnTriggerExit2D(Collider2D other) {
        GameManager.instance.isPlayerNearNPC = false;
        playerInput.enabled = false;
    }

    void StopPlayer_ShowDialog()
    {
        GameManager.instance.SetPausePlayer(true);
        GameManager.instance.VisualizeDialogUI(true);
    }

    void ActivatePlayer_DisappearDialog()
    {
        GameManager.instance.SetPausePlayer(false);
        GameManager.instance.VisualizeDialogUI(false);
        callCount = 0;
        isChatting = false;

    }

    public void showDialog()
    {
        if(isChatting == false)
        {
             StopPlayer_ShowDialog();
             GameManager.instance.SetSpeakerText(dialog.NPCname);
             isChatting = true;
             if(visited == false)
             {
                StartCoroutine(showFirstEncountDialog());
                visited = true;
             }else
             {
                 if(dialog.hasChoiceDialog)
                 {
                     StartCoroutine(showChoiceDialog());
                 }else
                 {
                     StartCoroutine(showRepeatingDialog());
                 }
                
             }

        }else
        {
             callCount++;
        }

    }

    IEnumerator showFirstEncountDialog()
    {
        string[] FirstEncountDialog = dialog.FirstEnCountDialogs;
        int strCount = FirstEncountDialog.Length;
        while(strCount > callCount)
        {
            GameManager.instance.SetDialogText(FirstEncountDialog[callCount]);
            yield return new WaitForEndOfFrame();
        }
        ActivatePlayer_DisappearDialog();
    }

    IEnumerator showRepeatingDialog()
    {
        string[] RepeatingDialog = dialog.ReapeatingDialogs;
        int strCount = RepeatingDialog.Length;
        while(strCount > callCount)
        {
            GameManager.instance.SetDialogText(RepeatingDialog[callCount]);
            yield return new WaitForEndOfFrame();

        }
        ActivatePlayer_DisappearDialog();
    }

    IEnumerator showChoiceDialog()
    {
        string[] ChoiceDialog = dialog.ChoiceDialog;
        int ChoiceQuestionQuantity = dialog.ChoiceQuestionQuantity;
        int strCount = ChoiceDialog.Length;

        string[] question = new string[ChoiceQuestionQuantity];
        string[] choice = new string[strCount - ChoiceQuestionQuantity];

        int index;
        for(index = 0; index<ChoiceQuestionQuantity; index++)
        {
            question[index] = ChoiceDialog[index];
        }

        for(int i = 0; i<strCount - ChoiceQuestionQuantity; i++)
        {
            choice[i] = ChoiceDialog[index];
            index++;
        }

        while(ChoiceQuestionQuantity-1 > callCount)
        {
            GameManager.instance.SetDialogText(question[callCount]);
            yield return new WaitForEndOfFrame();
        }
        GameManager.instance.SetDialogText(question[callCount]);

        callCount = 0;
        StartCoroutine(showSelectButtons(choice));       
    }

    IEnumerator showSelectButtons(string[] choice)
    {
        GameManager.instance.SetOptionsText(choice);
        isShowingOption = true;
        
        while(callCount == 0)
        {
            SelectOptionindex = (int)Mathf.Clamp(SelectOptionindex, 0f, choice.Length-1);
            GameManager.instance.SelectOption(SelectOptionindex);
            yield return new WaitForEndOfFrame();
        }

        GameManager.instance.showOptionUI(false);
        isShowingOption = false;
        callCount = 0;
        StartCoroutine(showOptionDialog(SelectOptionindex));
        SelectOptionindex = 0;
    }

    IEnumerator showEventDialog()
    {
        string[] EventDialog = dialog.EventDialogs;
        int strCount = EventDialog.Length;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator showOptionDialog(int index)
    {
        string[] OptionDialog;
        if(index == 0)
        {
            OptionDialog = dialog.option1Dialog;
        }else if(index == 1)
        {
            OptionDialog = dialog.option2Dialog;
        }else
        {
            OptionDialog = dialog.option3Dialog;
        }

        int strCount = OptionDialog.Length;
        while(strCount > callCount)
        {
            GameManager.instance.SetDialogText(OptionDialog[callCount]);
            yield return new WaitForEndOfFrame();

        }
        ActivatePlayer_DisappearDialog();
    }

}
