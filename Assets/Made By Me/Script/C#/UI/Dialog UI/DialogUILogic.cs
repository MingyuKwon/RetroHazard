using System;

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

    bool isNowTalking = false;

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

        monoBehavior.gameObject.SetActive(flag);
        if(!flag)
        {
            GameManager.EventManager.Invoke_NPCWalkAgainEvent();
        }
    }
    
    public void showTalkNPCDialog(bool visited ,Dialog dialog)
    {
        VisualizeDialogUI(true);
        this.dialog = dialog;
        if(GameAudioManager.LanguageManager.currentLanguage == "E")
        {   
            SetSpeakerText(dialog.NPCname);
        }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
        {
            SetSpeakerText(dialog.NPCnameKorean);
        }
        
        if(visited == false && dialog.hasFirstEncounterDialog)
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
        string[] FirstEncountDialog = null;

        if(GameAudioManager.LanguageManager.currentLanguage == "E")
        {   
            FirstEncountDialog = dialog.FirstEnCountDialogs;
        }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
        {
            FirstEncountDialog = dialog.FirstEnCountDialogsKorean;
        }

        int strCount = FirstEncountDialog.Length;
        int previousCallcount;

        while(strCount > callCount)
        {
            previousCallcount = callCount;
            isNowTalking = true;
            yield return monoBehavior.StartCoroutine(TypeTextAnimation(FirstEncountDialog[callCount], 1f));
            isNowTalking = false;
            while(previousCallcount == callCount)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        VisualizeDialogUI(false);
    }

    IEnumerator showRepeatingDialog()
    {
        string[] RepeatingDialog = null;

        if(GameAudioManager.LanguageManager.currentLanguage == "E")
        {   
            RepeatingDialog = dialog.ReapeatingDialogs;
        }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
        {
            RepeatingDialog = dialog.ReapeatingDialogsKorean;
        }

        int strCount = RepeatingDialog.Length;
        int previousCallcount;

        while(strCount > callCount)
        {
            previousCallcount = callCount;
            isNowTalking = true;
            yield return monoBehavior.StartCoroutine(TypeTextAnimation(RepeatingDialog[callCount], 1f));
            isNowTalking = false;
            while(previousCallcount == callCount)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        VisualizeDialogUI(false);
    }

    IEnumerator TypeTextAnimation(string message, float time)
    {
        //각 프레임이 1/프레임 의 시간동안 지속이 된다
        // 그럼 원하는 시간동안 되도록 프레임을 이용한 방법으로 시간을 맞추기 위해서는

        float timePerChar = time / 100;

        Stack<Char> stack = new Stack<Char>();

        dialogText.text = "";

        int index = -1;
        bool continueLock = false;

        foreach (char letter in message.ToCharArray())
        {
            index++;
            if(letter == '<')
            {
               if(message.Length -1 < index+1)
               {
                    Debug.Log("Incorrect <> type.");
                    yield return null;
               }

               if(message[index+1] == 'b')
               {    
                    stack.Push('b');
                    dialogText.text +="<b></b>";
               }else if(message[index+1] == 'i')
               {
                    stack.Push('i');
                    dialogText.text +="<i></i>";
               }else if(message[index+1] == '/')
               {
                    stack.Pop();
               }

               continueLock = true;
            }else if(letter == '>')
            {
                continueLock = false;
                continue;
            }

            if(continueLock)
            {
                continue;
            }

            if(stack.Count !=0){
                if(stack.Peek() == 'b' || stack.Peek() == 'i')
                {
                    dialogText.text = dialogText.text.Insert(dialogText.text.Length-4, letter.ToString());
                }

            }else
            {
                dialogText.text += letter;
            }
            yield return new WaitForSecondsRealtime(timePerChar);
        }
    }


    IEnumerator showChoiceDialog()
    {
        string[] ChoiceDialog = null;

        if(GameAudioManager.LanguageManager.currentLanguage == "E")
        {   
            ChoiceDialog = dialog.ChoiceDialog;
        }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
        {
            ChoiceDialog = dialog.ChoiceDialogKorean;
        }


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

        int previousCallcount;

        while(ChoiceQuestionQuantity-1 > callCount)
        {
            previousCallcount = callCount;
            isNowTalking = true;
            yield return monoBehavior.StartCoroutine(TypeTextAnimation(question[callCount], 1.5f));
            isNowTalking = false;
            while(previousCallcount == callCount)
            {
                yield return new WaitForEndOfFrame();
            }
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
        string[] EventDialog = null;
        if(GameAudioManager.LanguageManager.currentLanguage == "E")
        {   
            EventDialog = dialog.EventDialogs;
        }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
        {
            EventDialog = dialog.EventDialogsKorean;
        }

        int strCount = EventDialog.Length;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator showOptionDialog(int index)
    {
        string[] OptionDialog = null;
        if(index == 0)
        {
            if(GameAudioManager.LanguageManager.currentLanguage == "E")
            {   
                OptionDialog = dialog.option1Dialog;
            }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
            {
                OptionDialog = dialog.option1DialogKorean;
            }
            
        }else if(index == 1)
        {
            if(GameAudioManager.LanguageManager.currentLanguage == "E")
            {   
                OptionDialog = dialog.option2Dialog;
            }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
            {
                OptionDialog = dialog.option2DialogKorean;
            }
            
        }else
        {
            if(GameAudioManager.LanguageManager.currentLanguage == "E")
            {   
                OptionDialog = dialog.option3Dialog;
            }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
            {
                OptionDialog = dialog.option3DialogKorean;
            }
        }

        int strCount = OptionDialog.Length;
        int previousCallcount;

        while(strCount > callCount)
        {
            previousCallcount = callCount;
            isNowTalking = true;
            yield return monoBehavior.StartCoroutine(TypeTextAnimation(OptionDialog[callCount], 1f));
            isNowTalking = false;
            while(previousCallcount == callCount)
            {
                yield return new WaitForEndOfFrame();
            }
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

        DialogInteract.isTalking = true;


        GameAudioManager.instance.PlayUIMusic(UIAudioType.Click);


        GameMangerInput.InputEvent.DialogUIEnterPressed += EnterPressed;
        GameMangerInput.InputEvent.DialogUIBackPressed += EnterPressed;
        GameMangerInput.InputEvent.DialogUIUpPressed += UpPressed;
        GameMangerInput.InputEvent.DialogUIDownPressed += DownPressed;
        GameMangerInput.InputEvent.DialogUIRightPressed += RightPressed;
        GameMangerInput.InputEvent.DialogUILeftPressed += LeftPressed;
    }
    public void OnDisable() {
        UI.instance.SetMouseCursorActive(false);

        GameMangerInput.releaseInput(InputType.DialogtUIInput);

        DialogInteract.isTalking = false;

        callCount = 0;
        showOptionUI(false);
        isShowingOption = false;
        SelectOptionindex = 0;


        GameMangerInput.InputEvent.DialogUIEnterPressed -= EnterPressed;
        GameMangerInput.InputEvent.DialogUIBackPressed -= EnterPressed;
        GameMangerInput.InputEvent.DialogUIUpPressed -= UpPressed;
        GameMangerInput.InputEvent.DialogUIDownPressed -= DownPressed;
        GameMangerInput.InputEvent.DialogUIRightPressed -= RightPressed;
        GameMangerInput.InputEvent.DialogUILeftPressed -= LeftPressed;
    }

    public void EnterPressed()
    {
        if(!isNowTalking)
        {
            GameAudioManager.instance.PlayUIMusic(UIAudioType.Click);
            callCount++;
        }
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
