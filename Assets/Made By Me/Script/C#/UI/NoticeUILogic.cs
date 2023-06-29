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


    public void showNotice(string text, int panelWidth, int panelHeight)
    {
        if(text == null) // 이건 Notice를 닫기를 원할 떄
        {
            monoBehaviour.gameObject.SetActive(false);
        }else
        {
            monoBehaviour.gameObject.SetActive(true);
            GameObject noticePanel = monoBehaviour.transform.GetChild(0).gameObject;
            RectTransform rectTransform = noticePanel.GetComponent<RectTransform>();
            Text str = noticePanel.transform.GetChild(0).gameObject.GetComponent<Text>();

            rectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);
            str.text = text;
        }
        
    }
}
