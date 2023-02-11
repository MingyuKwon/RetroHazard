using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class DialogUI : MonoBehaviour
{
    public bool inputOk = false;

    GameObject dialogPanel;
    GameObject speakerPanel;

    private Player player;
    option[] Option;

    Text dialogText;
    Text speakerText;

    string[] dialogtexts;

    /// talk with NPC /////////
    public int callCount = 0;
    Dialog dialog;
    public int SelectOptionindex = 0;
    public bool isShowingOption = false;
    /// talk with NPC /////////

    void Awake() {
        player = ReInput.players.GetPlayer(0);

        player.AddInputEventDelegate(EnterPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Enter");
        player.AddInputEventDelegate(UpPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectUp");
        player.AddInputEventDelegate(DownPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectDown");
        player.AddInputEventDelegate(RightPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectRight");
        player.AddInputEventDelegate(LeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectLeft");

        dialogPanel = GetComponentInChildren<DialogPanel>().gameObject;
        speakerPanel = GetComponentInChildren<SpeakerPanel>().gameObject;
        

        dialogText = dialogPanel.gameObject.GetComponentInChildren<Text>();
        speakerText = speakerPanel.gameObject.GetComponentInChildren<Text>();
        

        Option = dialogPanel.GetComponentsInChildren<option>();
    }

    // for question option select
    public void EnterPressed(InputActionEventData data)
    {
        if(!inputOk) return;
        callCount++;

    }
    public void UpPressed(InputActionEventData data)
    {
        if(!inputOk) return;
        if(isShowingOption)
        {
            SelectOptionindex--;
        }
    }
    public void DownPressed(InputActionEventData data)
    {
        if(!inputOk) return;
        if(isShowingOption)
        {
            SelectOptionindex++;
        }
    }
    public void RightPressed(InputActionEventData data)
    {
        if(!inputOk) return;
    }
    public void LeftPressed(InputActionEventData data)
    {
        if(!inputOk) return;
    }
    // for question option select
    private void OnEnable() {
        IinteractiveUI.interact_Input_Rlease_Event += InputGetBack;
    }
    private void OnDisable() {
        IinteractiveUI.interact_Input_Rlease_Event -= InputGetBack;
    }

    private void InputGetBack()
    {
        Debug.Log(this.gameObject.transform.parent.GetInstanceID() + "sdfsf");

        if(this.gameObject.activeInHierarchy != true) return;
        inputOk = true;
    }

    void Start() {
        dialogText.text = "dialog";
        speakerText.text = "speaker";
    }

    public void showOptionUI(bool flag)
    {
        for(int i=0; i<Option.Length; i++)
        {
            Option[i].gameObject.SetActive(flag);
        }
    }
    public void SetSpeakerPanelUI(bool flag)
    {
        speakerPanel.SetActive(flag);
    }
    public void SetDialogPanelUI(bool flag)
    {
        dialogPanel.SetActive(flag);
    }


    public void VisualizeDialogUI(bool flag)
    {
        GameManager.instance.SetPauseGame(flag);
        inputOk = flag;

        if(flag)
        {
            GameMangerInput.instance.changePlayerInputRule(1);
        }else
        {
            GameMangerInput.instance.changePlayerInputRule(0);
        }


        SetSpeakerPanelUI(flag);
        SetDialogPanelUI(flag);

        
        showOptionUI(false);

    }



    //Talk with NPC//////////////
    public void showTalkNPCDialog(bool visited ,Dialog dialog)
    {
        VisualizeDialogUI(true);
        this.dialog = dialog;
        SetSpeakerText(dialog.NPCname);
        if(visited == false)
        {
            StartCoroutine(showFirstEncountDialog());
        }else
        {
            if(dialog.hasChoiceDialog)
            {
                StartCoroutine(showChoiceDialog());
            }else
            {
                StartCoroutine(showRepeatingDialog());
            } 
        }
    }

    void DisappearTalkNPCDialog()
    {
        callCount = 0;
        GameManagerUI.instance.VisualizeDialogUI(false);
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

        DisappearTalkNPCDialog();
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
        DisappearTalkNPCDialog();
    }

    IEnumerator showChoiceDialog()
    {
        string[] ChoiceDialog = dialog.ChoiceDialog;
        int ChoiceQuestionQuantity = dialog.ChoiceQuestionQuantity;
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
        GameManagerUI.instance.SetDialogText(question[callCount]);

        callCount = 0;
        StartCoroutine(showSelectButtons(choice));       
    }

    IEnumerator showSelectButtons(string[] choice)
    {
        GameManagerUI.instance.SetOptionsText(choice);
        isShowingOption = true;
        
        while(callCount == 0)
        {
            SelectOptionindex = (int)Mathf.Clamp(SelectOptionindex, 0f, choice.Length-1);
            GameManagerUI.instance.SelectOption(SelectOptionindex);
            yield return new WaitForEndOfFrame();
        }

        GameManagerUI.instance.showOptionUI(false);
        isShowingOption = false;
        callCount = 0;
        StartCoroutine(showOptionDialog(SelectOptionindex));
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
            GameManagerUI.instance.SetDialogText(OptionDialog[callCount]);
            yield return new WaitForEndOfFrame();

        }
        DisappearTalkNPCDialog();
    }

    //Talk with NPC//////////////

    

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
