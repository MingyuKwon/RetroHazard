using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeUILogic
{
    NoticeUI monoBehaviour;
    Text str;
    GameObject noticePanel;
    RectTransform rectTransform;

    int callCount;
    int strCount;

    public NoticeUILogic(NoticeUI monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        
        noticePanel = monoBehaviour.transform.GetChild(0).gameObject;
        rectTransform = noticePanel.GetComponent<RectTransform>();
        str = noticePanel.transform.GetChild(0).gameObject.GetComponent<Text>();

        GameManager.EventManager.showNotice += showNotice;
    }

    public void OnEnable() {
        callCount = 0;
    }

    public void OnDisable() {
        callCount = 0;
    }

    string showingUI;
    public void showNotice(string showingUI ,string[] texts, bool isTyping, int panelWidth, int panelHeight)
    {
        if(texts == null) // 이건 Notice를 닫기를 원할 떄
        {
            if(this.showingUI != showingUI) return;
            monoBehaviour.gameObject.SetActive(false);
        }else
        {
            this.showingUI = showingUI;

            monoBehaviour.gameObject.SetActive(true);

            rectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);

            if(isTyping)
            {
                str.text = "";
                monoBehaviour.StartCoroutine(Typing(texts));
            }else
            {
                str.text = texts[0];
            }
            
        }
    }

    bool isNowTalking;

    IEnumerator Typing(string[] texts)
    {
        GameMangerInput.getInput(InputType.NoticeUIInput);
        GameMangerInput.InputEvent.NoticeCloseEvent += EnterPressed;

        strCount = texts.Length;
        int previousCallcount;

        while(strCount > callCount)
        {
            previousCallcount = callCount;
            isNowTalking = true;
            yield return monoBehaviour.StartCoroutine(TypeTextAnimation(texts[callCount], 1.5f));
            isNowTalking = false;
            while(previousCallcount == callCount)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void EnterPressed()
    {
        if(!isNowTalking) return; // 아직 텍스트 치는거 안끝났으면 이 이후는 받지 않음

        callCount++;

        if(callCount >= strCount)
        {
            GameMangerInput.InputEvent.NoticeCloseEvent -= EnterPressed;
            GameMangerInput.releaseInput(InputType.NoticeUIInput);
            showNotice(this.showingUI, null, false, 100, 100);
        }
        
    }

    IEnumerator TypeTextAnimation(string message, float time)
    {
        //각 프레임이 1/프레임 의 시간동안 지속이 된다
        // 그럼 원하는 시간동안 되도록 프레임을 이용한 방법으로 시간을 맞추기 위해서는

        float timePerChar = time / 60 / Time.unscaledDeltaTime;

        Stack<Char> stack = new Stack<Char>();

        str.text = "";

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
                    str.text +="<b></b>";
               }else if(message[index+1] == 'i')
               {
                    stack.Push('i');
                    str.text +="<i></i>";
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
                    str.text = str.text.Insert(str.text.Length-4, letter.ToString());
                }

            }else
            {
                str.text += letter;
            }

            for(int i=0; i<timePerChar; i++ )
            {
                yield return new WaitForEndOfFrame();
            }

        }
    }
}
