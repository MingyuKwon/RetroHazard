using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
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
    }

    void Start() {
        
        dialogText.text = "dialog";
        speakerText.text = "speaker";
        VisualizeDialogUI(false);
    }

    public void showOptionUI(bool flag)
    {
        for(int i=0; i<Option.Length; i++)
        {
            Option[i].gameObject.SetActive(flag);
        }
    }


    public void VisualizeDialogUI(bool flag)
    {
        dialogPanel.SetActive(flag);
        speakerPanel.SetActive(flag);
        GameManagerUI.instance.showOptionUI(false);
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
}
