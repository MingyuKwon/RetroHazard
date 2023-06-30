using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NoticeUILogic
{
    NoticeUI monoBehaviour;

    Text str;
    GameObject noticePanel;
    RectTransform rectTransform;

    public NoticeUILogic(NoticeUI monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        
        noticePanel = monoBehaviour.transform.GetChild(0).gameObject;
        rectTransform = noticePanel.GetComponent<RectTransform>();
        str = noticePanel.transform.GetChild(0).gameObject.GetComponent<Text>();

        GameManager.EventManager.showNotice += showNotice;
    }

    public void OnEnable() {
        
    }

    public void OnDisable() {
        
    }

    string showingUI;
    public void showNotice(string showingUI ,string text, bool isTyping, int panelWidth, int panelHeight)
    {
        if(text == null) // 이건 Notice를 닫기를 원할 떄
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
                monoBehaviour.StartCoroutine(Typing(text));
            }else
            {
                str.text = text;
            }
            
        }
    }

    IEnumerator Typing(string text)
    {
        Debug.Log("Typing Start");
    
        yield return str.DOText(text, 1f).WaitForCompletion();

        Debug.Log("Typing End");

    }
}
