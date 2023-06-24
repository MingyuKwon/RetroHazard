using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveUILogic 
{
    public bool isCalledByKeyItem = false;
    public int callCount = 0;
    Text interactiveDialogText;
    string[] interactivedialogTexts;

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
        callCount++;
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

        Interactive_ChangeInput_PauseGame(flag);
    }


    private void Interactive_ChangeInput_PauseGame(bool flag) // 입력 룰을 바꿔주고 게임을 멈춘다
    {
        if(flag)
        {
            GameMangerInput.instance.changePlayerInputRule(1);
        }else
        {
            if(GameManagerUI.instance.isDialogUIActive || GameManagerUI.instance.isTabUIActive 
            || GameManagerUI.instance.isBoxUIActive)
            {

            }else
            {
                GameMangerInput.instance.changePlayerInputRule(0);
            }
            
        }

        GameManager.instance.SetPauseGame(flag);
    }


    // 여기 코드가 엔터를 받을 때 마다 텍스트가 바뀌는 것을 구현햐였다
    IEnumerator InteractiveDialog(string ItemName = "")
    {
        bool flag;
        Debug.Log("InteractiveDialog " + "ItemName : " + ItemName);
        

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

        while(strCount > callCount)
        {
            interactiveDialogText.text = interactivedialogTexts[callCount];
            yield return new WaitForEndOfFrame();
        }
        VisualizeInteractiveUI(false);

        if(!flag) yield return null;
        
        GameManager.instance.SetPlayerAnimationObtainKeyItem(false);
    }
}
