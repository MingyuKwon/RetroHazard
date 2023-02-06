using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class DialogUI : MonoBehaviour
{
    GameObject dialogPanel;
    GameObject speakerPanel;
    GameObject interactiveDialogPanel;

    private Player player;
    option[] Option;

    Text dialogText;
    Text speakerText;
    Text interactiveDialogText;

    string[] dialogtexts;
    string[] interactivedialogTexts;

    ///
    int callCount = 0;
    ///

    void Awake() {
        player = ReInput.players.GetPlayer(0);

        player.AddInputEventDelegate(EnterPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Enter");
        player.AddInputEventDelegate(UpPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectUp");
        player.AddInputEventDelegate(DownPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectDown");
        player.AddInputEventDelegate(RightPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectRight");
        player.AddInputEventDelegate(LeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectLeft");

        dialogPanel = GetComponentInChildren<DialogPanel>().gameObject;
        speakerPanel = GetComponentInChildren<SpeakerPanel>().gameObject;
        interactiveDialogPanel = GetComponentInChildren<InteractiveDialogPanel>().gameObject;

        dialogText = dialogPanel.gameObject.GetComponentInChildren<Text>();
        speakerText = speakerPanel.gameObject.GetComponentInChildren<Text>();
        interactiveDialogText = interactiveDialogPanel.gameObject.GetComponentInChildren<Text>();

        Option = dialogPanel.GetComponentsInChildren<option>();
    }

        // for question option select
    public void EnterPressed(InputActionEventData data)
    {
        callCount++;
    }
    public void UpPressed(InputActionEventData data)
    {

    }
    public void DownPressed(InputActionEventData data)
    {

    }
    public void RightPressed(InputActionEventData data)
    {
        
    }
    public void LeftPressed(InputActionEventData data)
    {

    }
    // for question option select


    void Start() {
        dialogText.text = "dialog";
        speakerText.text = "speaker";
        VisualizeDialogUI(false, false);
    }

    public void VisualizeDialogUI(bool flag, bool isNPC)
    {
        if(isNPC)
        {
            showSpeakerPanelUI(flag);
            showDialogPanelUI(flag);
            showInteractiveDialogPanelUI(false);
        }else
        {
            showSpeakerPanelUI(false);
            showDialogPanelUI(false);
            showInteractiveDialogPanelUI(flag);
        }
        
        showOptionUI(false);
    }

    public void showOptionUI(bool flag)
    {
        for(int i=0; i<Option.Length; i++)
        {
            Option[i].gameObject.SetActive(flag);
        }
    }
    public void showSpeakerPanelUI(bool flag)
    {
        speakerPanel.SetActive(flag);
    }
    public void showDialogPanelUI(bool flag)
    {
        dialogPanel.SetActive(flag);
    }
    public void showInteractiveDialogPanelUI(bool flag)
    {
        if(flag)
        {
            GameMangerInput.instance.changePlayerInputRule(1);
            dialogPanel.SetActive(false);
            speakerPanel.SetActive(false);
            StartCoroutine(InteractiveDialog());
        }else
        {
            callCount = 0;
            GameMangerInput.instance.changePlayerInputRule(0);
        }

        interactiveDialogPanel.SetActive(flag);
        
    }

    IEnumerator InteractiveDialog()
    {
        int strCount = interactivedialogTexts.Length;
        while(strCount > callCount)
        {
            interactiveDialogText.text = interactivedialogTexts[callCount];
            yield return new WaitForEndOfFrame();
        }

        showInteractiveDialogPanelUI(false);
        GameManager.instance.SetPauseGame(false);
        GameManager.instance.SetPlayerAnimationObtainKeyItem(false);
    }



    public void SetDialogText(string text)
    {
        dialogText.text = text;
    }
    public void SetSpeakerText(string text)
    {
        speakerText.text = text;
    }
    public void SetInteractiveDialogText(string[] texts)
    {
        interactivedialogTexts = texts;
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
