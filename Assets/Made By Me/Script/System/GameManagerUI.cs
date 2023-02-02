using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    public static GameManagerUI instance = null;

    blackOut blackoutUI;
    DialogUI dialogUI;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        blackoutUI = FindObjectOfType<blackOut>();
        dialogUI = FindObjectOfType<DialogUI>();
    }

    public void BlackOut(int index)
    {
        blackoutUI.BlackOut(index);
    }

    public void VisualizeDialogUI(bool flag)
    {
        dialogUI.VisualizeDialogUI(flag);
    }

    public void SetDialogText(string text)
    {
        dialogUI.SetDialogText(text);
    }
    public void SetSpeakerText(string text)
    {
        dialogUI.SetSpeakerText(text);
    }

    public void SetOptionsText(string[] texts)
    {
        dialogUI.SetOptionsText(texts);
        
    }

    public void showOptionUI(bool flag)
    {
        dialogUI.showOptionUI(flag);
    }

    public void SelectOption(int index)
    {
        dialogUI.SelectOption(index);
    }
}
