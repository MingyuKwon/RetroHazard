using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeUILogic
{
    NoticeUI monoBehaviour;

    public NoticeUILogic(NoticeUI monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        GameManager.EventManager.showNotice += showNotice;
    }

    public void OnEnable() {
        
    }

    public void OnDisable() {
        
    }

    string showingUI;
    public void showNotice(string showingUI ,string text, int panelWidth, int panelHeight)
    {
        if(text == null) // 이건 Notice를 닫기를 원할 떄
        {
            if(this.showingUI != showingUI) return;
            monoBehaviour.gameObject.SetActive(false);
        }else
        {
            this.showingUI = showingUI;

            monoBehaviour.gameObject.SetActive(true);
            GameObject noticePanel = monoBehaviour.transform.GetChild(0).gameObject;
            RectTransform rectTransform = noticePanel.GetComponent<RectTransform>();
            Text str = noticePanel.transform.GetChild(0).gameObject.GetComponent<Text>();

            rectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);
            str.text = text;
        }
        
    }
}
