using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseRootUILogic 
{
    public int CurrentWindowLayer {
        get{
            return _CurrentWindowLayer;
        }
        set{
            if(_CurrentWindowLayer == 0)
            {
                _CurrentWindowLayer = value;
                GameAudioManager.instance.PlayUIMusic(UIAudioType.Click);

            }else
            {
                _CurrentWindowLayer = value;
            }
        }
    }

    public int _CurrentWindowLayer = 0;

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
        GameMangerInput.getInput(InputType.MenuUIInput);

        UI.instance.SetMouseCursorActive(true);
        GameManager.instance.SetPauseGame(true);
        GameManagerUI.instance.Visualize_InGameUI(false);

        CurrentWindowLayer = 0;

        GameManager.EventManager.PauseWindowLayer_Change_Event += windowLayer_Check;
        GameMangerInput.InputEvent.MenuUIEnterPressed += Enter_Clicked_Pressed;
        GameMangerInput.InputEvent.MenuUIBackPressed += Back_Clicked_Pressed;
        GameMangerInput.InputEvent.MenuUIUpPressed += Up_Pressed;
        GameMangerInput.InputEvent.MenuUIDownPressed += Down_Pressed;
        GameMangerInput.InputEvent.MenuUIRightPressed += Right_Pressed;
        GameMangerInput.InputEvent.MenuUILeftPressed += Left_Pressed;

        GameMangerInput.InputEvent.MenuUIRightPressing += Right_Pressing;
        GameMangerInput.InputEvent.MenuUILeftPressing += Left_Pressing;

        GameMangerInput.InputEvent.RemoveSaveSlot += RemoveSaveSlot;

        GameManager.EventManager.Invoke_PauseWindowLayer_Change_Event();
    }

    public void OnDisable() {
        GameMangerInput.releaseInput(InputType.MenuUIInput);
        UI.instance.SetMouseCursorActive(false);
        GameManager.instance.SetPauseGame(false);
        CurrentWindowLayer = 0;

        GameManagerUI.instance.Visualize_InGameUI(true);

        GameManager.EventManager.PauseWindowLayer_Change_Event -= windowLayer_Check;
        GameMangerInput.InputEvent.MenuUIEnterPressed -= Enter_Clicked_Pressed;
        GameMangerInput.InputEvent.MenuUIBackPressed -= Back_Clicked_Pressed;
        GameMangerInput.InputEvent.MenuUIUpPressed -= Up_Pressed;
        GameMangerInput.InputEvent.MenuUIDownPressed -= Down_Pressed;
        GameMangerInput.InputEvent.MenuUIRightPressed -= Right_Pressed;
        GameMangerInput.InputEvent.MenuUILeftPressed -= Left_Pressed;

        GameMangerInput.InputEvent.MenuUIRightPressing -= Right_Pressing;
        GameMangerInput.InputEvent.MenuUILeftPressing -= Left_Pressing;

        GameMangerInput.InputEvent.RemoveSaveSlot -= RemoveSaveSlot;
    }

    private void RemoveSaveSlot()
    {
        UI.instance.PauseUI.saveSlotUI.Delete(UI.instance.PauseUI.saveSlotUI.saveSlotIndex);
    }

    private void Enter_Clicked_Pressed()
    {
        if(UI.instance.PauseUI.pauseMainUI.gameObject.activeInHierarchy)
        {
            switch (UI.instance.PauseUI.pauseMainUI.currentIndex)
            {
                case 0 :
                    UI.instance.PauseUI.CurrentWindowLayer = -1;
                    break;

                case 1 :
                    UI.instance.PauseUI.CurrentWindowLayer = 1;
                    break;

                case 2 :
                    UI.instance.PauseUI.CurrentWindowLayer = 2;
                    break;

                case 3 :
                    UI.instance.PauseUI.pauseMainUI.MainMenu();
                    break;
            }

            GameManager.EventManager.Invoke_PauseWindowLayer_Change_Event();
                
        }else if(UI.instance.PauseUI.saveSlotUI.gameObject.activeInHierarchy)
        {
            if(UI.instance.PauseUI.saveSlotUI.saveSlotIndex >= 0)
            {
                UI.instance.PauseUI.saveSlotUI.SlotClick(UI.instance.PauseUI.saveSlotUI.saveSlotIndex);
            }

        }else if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy)
        {
            if(OptionUI.panelNum == 0)
            {
                OptionUI.panelNum = OptionUI.panelsearchNum;
            }else if(OptionUI.panelNum == 1)
            {
                
            }else if(OptionUI.panelNum == 2)
            {
                UI.instance.PauseUI.optionUI.OkVideoOption();
            }else if(OptionUI.panelNum == 3)
            {
                UI.instance.PauseUI.optionUI.OkAudioOption();
            }else if(OptionUI.panelNum == 4)
            {
                UI.instance.PauseUI.optionUI.OkGeneralOption();
            }
        
        }
    }

    private void Back_Clicked_Pressed()
    {
        if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy && OptionUI.panelNum == 0)
        {
            
        }else if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy && OptionUI.panelNum != 0)
        {
            int beforeInt = OptionUI.panelsearchNum;
            OptionUI.panelNum = 0;
            OptionUI.panelsearchNum =beforeInt;
            OptionUI.videoIndex = -1;
            OptionUI.audioIndex = -1;
            OptionUI.generalIndex = -1;
            return;
        }

        if(CurrentWindowLayer == 0 )
        {
            CurrentWindowLayer--;
        }else
        {
            CurrentWindowLayer = 0;
        }

        GameManager.EventManager.Invoke_PauseWindowLayer_Change_Event();
    }

    private void Up_Pressed()
    {
        if(UI.instance.PauseUI.pauseMainUI.gameObject.activeInHierarchy)
        {
            int value = UI.instance.PauseUI.pauseMainUI.currentIndex;
            value--;

            UI.instance.PauseUI.pauseMainUI.currentIndex = Mathf.Clamp(value, 0, 3);

            GameManager.EventManager.Invoke_PauseWindowLayer_Change_Event();
            
        }else if(UI.instance.PauseUI.saveSlotUI.gameObject.activeInHierarchy)
        {
            int value = UI.instance.PauseUI.saveSlotUI.saveSlotIndex;
            value -= 2;

            UI.instance.PauseUI.saveSlotUI.saveSlotIndex = Mathf.Clamp(value, 0,9);

        }else if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy)
        {
            if(OptionUI.panelNum == 0)
            {
                int value = OptionUI.panelsearchNum;
                value--;
                OptionUI.panelsearchNum = Mathf.Clamp(value,1,4);
            }else if(OptionUI.panelNum == 1)
            {

            }else if(OptionUI.panelNum == 2)
            {
                int value = OptionUI.videoIndex;
                value--;
                OptionUI.videoIndex = Mathf.Clamp(value,0,5);
            }else if(OptionUI.panelNum == 3)
            {
                int value = OptionUI.audioIndex;
                value--;
                OptionUI.audioIndex = Mathf.Clamp(value,0,5);

            }else if(OptionUI.panelNum == 4)
            {
                int value = OptionUI.generalIndex;
                value--;
                OptionUI.generalIndex = Mathf.Clamp(value,0,2);
            }
        }
    }

    private void Down_Pressed()
    {
        if(UI.instance.PauseUI.pauseMainUI.gameObject.activeInHierarchy)
        {
            int value = UI.instance.PauseUI.pauseMainUI.currentIndex;
            value++;

            UI.instance.PauseUI.pauseMainUI.currentIndex = Mathf.Clamp(value, 0, 3);

            GameManager.EventManager.Invoke_PauseWindowLayer_Change_Event();

        }else if(UI.instance.PauseUI.saveSlotUI.gameObject.activeInHierarchy)
        {
            int value = UI.instance.PauseUI.saveSlotUI.saveSlotIndex;
            value += 2;

            UI.instance.PauseUI.saveSlotUI.saveSlotIndex = Mathf.Clamp(value, 0,9);
        }else if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy)
        {
            if(OptionUI.panelNum == 0)
            {
                int value = OptionUI.panelsearchNum;
                value++;
                OptionUI.panelsearchNum = Mathf.Clamp(value,1,4);
            }else if(OptionUI.panelNum == 1)
            {

            }else if(OptionUI.panelNum == 2)
            {
                int value = OptionUI.videoIndex;
                value++;
                OptionUI.videoIndex = Mathf.Clamp(value,0,5);
            }else if(OptionUI.panelNum == 3)
            {
                int value = OptionUI.audioIndex;
                value++;
                OptionUI.audioIndex = Mathf.Clamp(value,0,5);
            }else if(OptionUI.panelNum == 4)
            {
                int value = OptionUI.generalIndex;
                value++;
                OptionUI.generalIndex = Mathf.Clamp(value,0,2);
            }
        }
    }

    private void Right_Pressed()
    {
        if(UI.instance.PauseUI.pauseMainUI.gameObject.activeInHierarchy)
        {

        }else if(UI.instance.PauseUI.saveSlotUI.gameObject.activeInHierarchy)
        {
            int value = UI.instance.PauseUI.saveSlotUI.saveSlotIndex;
            value += 1;

            UI.instance.PauseUI.saveSlotUI.saveSlotIndex = Mathf.Clamp(value, 0,9);
        }else if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy)
        {
            if(OptionUI.panelNum == 0)
            {
            }else if(OptionUI.panelNum == 1)
            {
                
            }else if(OptionUI.panelNum == 2)
            {
            }else if(OptionUI.panelNum == 3)
            {
            }else if(OptionUI.panelNum == 4)
            {

            }
        }
    }

    private void Right_Pressing()
    {
        if(UI.instance.PauseUI.pauseMainUI.gameObject.activeInHierarchy)
        {

        }else if(UI.instance.PauseUI.saveSlotUI.gameObject.activeInHierarchy)
        {

        }else if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy)
        {
            if(OptionUI.panelNum == 0)
            {
            }else if(OptionUI.panelNum == 1)
            {
                
            }else if(OptionUI.panelNum == 2)
            {
                UI.instance.PauseUI.optionUI.VideoSliderChange(true);
            }else if(OptionUI.panelNum == 3)
            {
                UI.instance.PauseUI.optionUI.AudioSliderChange(true);
            }else if(OptionUI.panelNum == 4)
            {

            }
        }
    }

    private void Left_Pressed()
    {
        if(UI.instance.PauseUI.pauseMainUI.gameObject.activeInHierarchy)
        {

        }else if(UI.instance.PauseUI.saveSlotUI.gameObject.activeInHierarchy)
        {
            int value = UI.instance.PauseUI.saveSlotUI.saveSlotIndex;
            value -= 1;

            UI.instance.PauseUI.saveSlotUI.saveSlotIndex = Mathf.Clamp(value, 0,9);
        }else if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy)
        {
            if(OptionUI.panelNum == 0)
            {
            }else if(OptionUI.panelNum == 1)
            {
                
            }else if(OptionUI.panelNum == 2)
            {
            }else if(OptionUI.panelNum == 3)
            {
            }else if(OptionUI.panelNum == 4)
            {
                
            }
        }
    }

    private void Left_Pressing()
    {
        if(UI.instance.PauseUI.pauseMainUI.gameObject.activeInHierarchy)
        {

        }else if(UI.instance.PauseUI.saveSlotUI.gameObject.activeInHierarchy)
        {

        }else if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy)
        {
            if(OptionUI.panelNum == 0)
            {
            }else if(OptionUI.panelNum == 1)
            {
                
            }else if(OptionUI.panelNum == 2)
            {
                UI.instance.PauseUI.optionUI.VideoSliderChange(false);
            }else if(OptionUI.panelNum == 3)
            {
                UI.instance.PauseUI.optionUI.AudioSliderChange(false);
            }else if(OptionUI.panelNum == 4)
            {
                
            }
        }
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
