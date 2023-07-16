using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InteractiveUILogic 
{
    public bool isCalledByKeyItem = false;
    public int callCount = 0;
    Text interactiveDialogText;
    string[] interactivedialogTexts;

    bool isNowTalking = false;

    IinteractiveUI monoBehavior;

    public InteractiveUILogic(Text interactiveDialogText, IinteractiveUI iinteractiveUI)
    {
        this.interactiveDialogText = interactiveDialogText;
        monoBehavior = iinteractiveUI;
    }

    public void OnEnable() {
        GameMangerInput.InputEvent.InteractiveUIEnterPressed += EnterPressed;
        UI.instance.SetMouseCursorActive(true);
        GameMangerInput.getInput(InputType.InteractiveUIInput);
        GameAudioManager.instance.PlayUIMusic(UIAudioType.Click);
    }

    public void OnDisable() {
        GameMangerInput.releaseInput(InputType.InteractiveUIInput);
        UI.instance.SetMouseCursorActive(false);
        isCalledByKeyItem = false;
        GameMangerInput.InputEvent.InteractiveUIEnterPressed -= EnterPressed;
    }

    public void SetInteractiveDialogText(string[] texts)
    {
        interactivedialogTexts = texts;
    }

    public void EnterPressed()
    {
        if(!isNowTalking)
        {
            GameAudioManager.instance.PlayUIMusic(UIAudioType.Click);
            callCount++;
        }
    }

    public void VisualizeInteractiveUI(bool flag, string ItemName = "")
    {        
        if(flag)
        {
            monoBehavior.gameObject.SetActive(true);

            if(ItemName == "")
            {
                monoBehavior.StartCoroutine(InteractiveDialog());
            }else
            {
                monoBehavior.StartCoroutine(InteractiveDialog(ItemName));
            }
            
        }else
        {
            callCount = 0;
            monoBehavior.gameObject.SetActive(false);
        }

        GameManager.instance.SetPauseGame(flag);
    }

    // 여기 코드가 엔터를 받을 때 마다 텍스트가 바뀌는 것을 구현햐였다
    IEnumerator InteractiveDialog(string ItemName = "")
    {
        bool flag;        

        if(ItemName == "")
        {
            flag = false; //키 아이템 먹은게 아님
        }else
        {
            flag = true; // 키 아이템 먹은것
        }

        while(callCount == 0 && flag)
        {
            interactiveDialogText.text = "You obtained <b><color=blue>\"" + ItemName + "\"</color></b> !";
            yield return new WaitForEndOfFrame();
        }
        
        int strCount = interactivedialogTexts.Length;
        callCount = 0;

        int previousCallcount;

        while(strCount > callCount)
        {
            previousCallcount = callCount;
            isNowTalking = true;
            yield return monoBehavior.StartCoroutine(TypeTextAnimation(interactivedialogTexts[callCount], 1.5f));
            isNowTalking = false;
            while(previousCallcount == callCount)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        if(HiddenInteract.nowHiddenInteracting) HiddenInteract.nowHiddenInteracting = false;
        VisualizeInteractiveUI(false);

        if(!flag) yield return null;
        
        GameManager.instance.SetPlayerAnimationObtainKeyItem(false);
    }


    IEnumerator TypeTextAnimation(string message, float time)
    {
        //각 프레임이 1/프레임 의 시간동안 지속이 된다
        // 그럼 원하는 시간동안 되도록 프레임을 이용한 방법으로 시간을 맞추기 위해서는

        float timePerChar = time / 60 / Time.unscaledDeltaTime;

        Stack<Char> stack = new Stack<Char>();

        interactiveDialogText.text = "";

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
                    interactiveDialogText.text +="<b></b>";
               }else if(message[index+1] == 'i')
               {
                    stack.Push('i');
                    interactiveDialogText.text +="<i></i>";
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
                    interactiveDialogText.text = interactiveDialogText.text.Insert(interactiveDialogText.text.Length-4, letter.ToString());
                }

            }else
            {
                interactiveDialogText.text += letter;
            }

            for(int i=0; i<timePerChar; i++ )
            {
                yield return new WaitForEndOfFrame();
            }

        }
    }
}
