using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;


public class NPCDialogScript : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    [SerializeField] int callCount = 0;
    [SerializeField] bool visited = false;
    [SerializeField] bool isChatting = false;

    public bool isShowingOption = false;
    public int SelectOptionindex = 0;

    private void Awake() {
        
    }

    // for question option select
    public void EnterPressed()
    {
        showDialog();
    }
    public void UpPressed()
    {
        if(isShowingOption)
        {
            SelectOptionindex--;
        }
    }
    public void DownPressed()
    {
        if(isShowingOption)
        {
            SelectOptionindex++;
        }
    }
    public void RightPressed()
    {
        
    }
    public void LeftPressed()
    {

    }
    // for question option select


    void showDialog()
    {
        if(isChatting == false)
        {
            GameManagerUI.instance.VisualizeDialogUI(true);
            GameManagerUI.instance.SetSpeakerText(dialog.NPCname);
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

    void ActivatePlayer_DisappearDialog()
    {
        GameMangerInput.instance.changePlayerInputRule(0);
        GameManagerUI.instance.VisualizeDialogUI(false);
        GameManagerUI.instance.showOptionUI(false);
        callCount = 0;
        isChatting = false;
    }

    IEnumerator showFirstEncountDialog()
    {
        string[] FirstEncountDialog = dialog.FirstEnCountDialogs;
        int strCount = FirstEncountDialog.Length;
        while(strCount > callCount)
        {
            GameManagerUI.instance.SetDialogText(FirstEncountDialog[callCount]);
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
            GameManagerUI.instance.SetDialogText(RepeatingDialog[callCount]);
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
            GameManagerUI.instance.SetDialogText(question[callCount]);
            yield return new WaitForEndOfFrame();
        }
        GameManagerUI.instance.SetDialogText(question[callCount]);

        callCount = 0;
        StartCoroutine(showSelectButtons(choice));       
    }

    IEnumerator showSelectButtons(string[] choice)
    {
        GameManagerUI.instance.SetOptionsText(choice);
        isShowingOption = true;
        
        while(callCount == 0)
        {
            SelectOptionindex = (int)Mathf.Clamp(SelectOptionindex, 0f, choice.Length-1);
            GameManagerUI.instance.SelectOption(SelectOptionindex);
            yield return new WaitForEndOfFrame();
        }

        GameManagerUI.instance.showOptionUI(false);
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
            GameManagerUI.instance.SetDialogText(OptionDialog[callCount]);
            yield return new WaitForEndOfFrame();

        }
        ActivatePlayer_DisappearDialog();
    }

}
