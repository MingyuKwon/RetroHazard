using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseRootUILogic 
{
    public int CurrentWindowLayer = 0;

    PauseMainUI pauseMainUI;
    SaveSlotUI saveSlotUI;
    OptionUI optionUI;

    PauseRootUI monoBehaviour;

    public PauseRootUILogic(PauseRootUI monoBehaviour)
    {
        pauseMainUI = UI.instance.PauseUI.pauseMainUI;
        saveSlotUI = UI.instance.PauseUI.saveSlotUI;
        optionUI = UI.instance.PauseUI.optionUI;

        this.monoBehaviour = monoBehaviour;
    }

    public void OnEnable() {
        GameMangerInput.instance.changePlayerInputRule(2);
        GameMangerInput.getInput(InputType.PauseUIInput);

        UI.instance.SetMouseCursorActive(true);
        GameManager.instance.SetPauseGame(true);
        GameManagerUI.instance.Visualize_InGameUI(false);

        CurrentWindowLayer = 0;

        GameManager.EventManager.windowLayer_Change_Event += windowLayer_Check;
        GameMangerInput.InputEvent.PauseUIEnterPressed += Enter_Clicked_Pressed;
        GameMangerInput.InputEvent.PauseUIBackPressed += Back_Clicked_Pressed;
        GameMangerInput.InputEvent.PauseUIUpPressed += Up_Pressed;
        GameMangerInput.InputEvent.PauseUIDownPressed += Down_Pressed;
        GameMangerInput.InputEvent.PauseUIRightPressed += Right_Pressed;
        GameMangerInput.InputEvent.PauseUILefttPressed += Left_Pressed;

        GameManager.EventManager.Invoke_windowLayer_Change_Event();
    }

    public void OnDisable() {
        GameMangerInput.instance.changePlayerInputRule(0);
        GameMangerInput.releaseInput(InputType.PauseUIInput);
        UI.instance.SetMouseCursorActive(false);
        GameManager.instance.SetPauseGame(false);
        CurrentWindowLayer = 0;

        GameManagerUI.instance.Visualize_InGameUI(true);

        GameManager.EventManager.windowLayer_Change_Event -= windowLayer_Check;
        GameMangerInput.InputEvent.PauseUIEnterPressed -= Enter_Clicked_Pressed;
        GameMangerInput.InputEvent.PauseUIBackPressed -= Back_Clicked_Pressed;
        GameMangerInput.InputEvent.PauseUIUpPressed -= Up_Pressed;
        GameMangerInput.InputEvent.PauseUIDownPressed -= Down_Pressed;
        GameMangerInput.InputEvent.PauseUIRightPressed -= Right_Pressed;
        GameMangerInput.InputEvent.PauseUILefttPressed -= Left_Pressed;
    }

    private void Enter_Clicked_Pressed()
    {

    }

    private void Back_Clicked_Pressed()
    {
        if(CurrentWindowLayer == 0 )
        {
            CurrentWindowLayer--;
        }else
        {
            CurrentWindowLayer = 0;
        }
        
        GameManager.EventManager.Invoke_windowLayer_Change_Event();
        
    }

    private void Up_Pressed()
    {
        
    }

    private void Down_Pressed()
    {
        
    }

    private void Right_Pressed()
    {
        
    }

    private void Left_Pressed()
    {
        
    }


    private void windowLayer_Check()
    {
        if(CurrentWindowLayer < 0)
        {
            monoBehaviour.gameObject.SetActive(false);
        }

        if(CurrentWindowLayer == 0)
        {
            pauseMainUI.gameObject.SetActive(true);
            saveSlotUI.gameObject.SetActive(false);
            optionUI.gameObject.SetActive(false);
        }

        if(CurrentWindowLayer == 1)
        {
           pauseMainUI.gameObject.SetActive(false);
           saveSlotUI.gameObject.SetActive(true);
           optionUI.gameObject.SetActive(false);
        }

        if(CurrentWindowLayer == 2)
        {
           pauseMainUI.gameObject.SetActive(false);
           saveSlotUI.gameObject.SetActive(false);
           optionUI.gameObject.SetActive(true);
        }
    }
}
