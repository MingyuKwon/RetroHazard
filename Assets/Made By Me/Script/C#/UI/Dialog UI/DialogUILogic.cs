using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUILogic 
{
    /// talk with NPC /////////
    public int callCount = 0;
    Dialog dialog;
    public int SelectOptionindex = 0;
    public bool isShowingOption = false;

    option[] Option;
    Text dialogText;
    Text speakerText;
    DialogUI monoBehavior;
    /// talk with NPC /////////

    public DialogUILogic(Text dialogText, Text speakerText, option[] Option, DialogUI monoBehavior)
    {
        this.Option = Option;
        this.dialogText = dialogText;
        this.speakerText = speakerText;
        this.monoBehavior = monoBehavior;
    }

    public void VisualizeDialogUI(bool flag)
    {
        GameManager.instance.SetPauseGame(flag);
        if(flag)
        {
            GameMangerInput.instance.changePlayerInputRule(1);
        }else
        {
            GameMangerInput.instance.changePlayerInputRule(0);
        }

        monoBehavior.gameObject.SetActive(flag);
    }
    
    public void showTalkNPCDialog(bool visited ,Dialog dialog)
    {
        VisualizeDialogUI(true);
        this.dialog = dialog;
        SetSpeakerText(dialog.NPCname);
        if(visited == false)
        {
            monoBehavior.StartCoroutine(showFirstEncountDialog());
        }else
        {
            if(dialog.hasChoiceDialog)
            {
                monoBehavior.StartCoroutine(showChoiceDialog());
            }else
            {
                monoBehavior.StartCoroutine(showRepeatingDialog());
            } 
        }
    }

    private void showOptionUI(bool flag)
    {
        for(int i=0; i<Option.Length; i++)
        {
            Option[i].gameObject.SetActive(flag);
        }
    }

    public void SelectOption(int index)
    {
        int optionCount = Option.Length;

        index = (int)Mathf.Clamp(index, 0, optionCount-1);
        for(int i=0; i<optionCount; i++)
        {
            if(i == index)
            {
                Option[i].Select(true);
            }else
            {
                Option[i].Select(false);
            }
        }
    }


    IEnumerator showFirstEncountDialog()
    {
        string[] FirstEncountDialog = dialog.FirstEnCountDialogs;
        int strCount = FirstEncountDialog.Length;
        while(strCount > callCount)
        {
            SetDialogText(FirstEncountDialog[callCount]);
            yield return new WaitForEndOfFrame();
        }

        VisualizeDialogUI(false);
    }

    IEnumerator showRepeatingDialog()
    {
        string[] RepeatingDialog = dialog.ReapeatingDialogs;
        int strCount = RepeatingDialog.Length;
        while(strCount > callCount)
        {
            SetDialogText(RepeatingDialog[callCount]);
            yield return new WaitForEndOfFrame();

        }
        VisualizeDialogUI(false);
    }

    IEnumerator showChoiceDialog()
    {
        string[] ChoiceDialog = dialog.ChoiceDialog;
        int ChoiceQuestionQuantity = dialog.ChoiceQuestionQuantity; 
        // 전체 크기 중에 질문에 대한 길이
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
            SetDialogText(question[callCount]);
            yield return new WaitForEndOfFrame();
        }

        SetDialogText(question[callCount]);

        callCount = 0;
        monoBehavior.StartCoroutine(showSelectButtons(choice));       
    }

    IEnumerator showSelectButtons(string[] choice)
    {
        SetOptionsText(choice);
        isShowingOption = true;
        
        while(callCount == 0)
        {
            SelectOptionindex = (int)Mathf.Clamp(SelectOptionindex, 0f, choice.Length-1);
            SelectOption(SelectOptionindex);
            yield return new WaitForEndOfFrame();
        }

        showOptionUI(false);
        isShowingOption = false;
        callCount = 0;
        monoBehavior.StartCoroutine(showOptionDialog(SelectOptionindex));
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
            SetDialogText(OptionDialog[callCount]);
            yield return new WaitForEndOfFrame();
        }

        VisualizeDialogUI(false);
    }



    public void SetDialogText(string text)
    {
        dialogText.text = text;
    }
    public void SetSpeakerText(string text)
    {
        speakerText.text = text;
    }

    public void SetOptionsText(string[] texts)
    {
        showOptionUI(true);
        for(int i=0; i<Option.Length; i++)
        {
            Option[i].changeText(texts[i]); 
        }
    }


    public void OnEnable() {
        UI.instance.SetMouseCursorActive(true);

        GameMangerInput.getInput(InputType.DialogtUIInput);

        GameMangerInput.InputEvent.DialogUIEnterPressed += EnterPressed;
        GameMangerInput.InputEvent.DialogUIUpPressed += UpPressed;
        GameMangerInput.InputEvent.DialogUIDownPressed += DownPressed;
        GameMangerInput.InputEvent.DialogURightPressed += RightPressed;
        GameMangerInput.InputEvent.DialogULeftPressed += LeftPressed;
    }
    public void OnDisable() {
        UI.instance.SetMouseCursorActive(false);

        GameMangerInput.releaseInput(InputType.DialogtUIInput);

        callCount = 0;
        showOptionUI(false);
        isShowingOption = false;
        SelectOptionindex = 0;

        GameMangerInput.InputEvent.DialogUIEnterPressed -= EnterPressed;
        GameMangerInput.InputEvent.DialogUIUpPressed -= UpPressed;
        GameMangerInput.InputEvent.DialogUIDownPressed -= DownPressed;
        GameMangerInput.InputEvent.DialogURightPressed -= RightPressed;
        GameMangerInput.InputEvent.DialogULeftPressed -= LeftPressed;
    }

    public void EnterPressed()
    {
        callCount++;
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
}
