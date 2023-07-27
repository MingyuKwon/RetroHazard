using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLogic 
{
    MainMenu monoBehaviour;
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

    public int _CurrentWindowLayer = 0;// 0: main, 1 :save Slot, 2 : option
    MainMenuUI mainMenuUI;
    SaveSlotUI saveSlotUI;
    OptionUI optionUI;

    public MainMenuLogic(MainMenu monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        mainMenuUI = MainMenu.instance.mainMenuUI;
        saveSlotUI = MainMenu.instance.saveSlotUI;
        optionUI = MainMenu.instance.optionUI;
    }

    public void OnEnable() {
        GameMangerInput.getInput(InputType.MenuUIInput);
        GameManager.EventManager.MainMenuWindowLayer_Change_Event += windowLayer_Check;

        mainMenuUI.gameObject.SetActive(true);
        saveSlotUI.gameObject.SetActive(false);
        optionUI.gameObject.SetActive(false);

        GameMangerInput.InputEvent.MenuUIEnterPressed += EnterPressed;
        GameMangerInput.InputEvent.MenuUIBackPressed += BackPressed;
        GameMangerInput.InputEvent.MenuUIUpPressed += UPPressed;
        GameMangerInput.InputEvent.MenuUIDownPressed += DownPressed;
        GameMangerInput.InputEvent.MenuUIRightPressed += RightPressed;
        GameMangerInput.InputEvent.MenuUILeftPressed += LeftPressed;

        GameMangerInput.InputEvent.RemoveSaveSlot += RemoveSaveSlot;

    }

    public void OnDisable() {
        GameMangerInput.releaseInput(InputType.MenuUIInput);
        GameManager.EventManager.MainMenuWindowLayer_Change_Event -= windowLayer_Check;

        GameMangerInput.InputEvent.MenuUIEnterPressed -= EnterPressed;
        GameMangerInput.InputEvent.MenuUIBackPressed -= BackPressed;
        GameMangerInput.InputEvent.MenuUIUpPressed -= UPPressed;
        GameMangerInput.InputEvent.MenuUIDownPressed -= DownPressed;
        GameMangerInput.InputEvent.MenuUIRightPressed -= RightPressed;
        GameMangerInput.InputEvent.MenuUILeftPressed -= LeftPressed;

        GameMangerInput.InputEvent.RemoveSaveSlot -= RemoveSaveSlot;

    }

    private void windowLayer_Check()
    {
        if(CurrentWindowLayer == 0)
        {
            ActiveUI(0);
        }

        if(CurrentWindowLayer == 1)
        {
           ActiveUI(1);
        }

        if(CurrentWindowLayer == 2)
        {
           ActiveUI(2);
        }
    }

    public void ActiveUI(int index)
    {
        mainMenuUI.gameObject.SetActive(false);
        saveSlotUI.gameObject.SetActive(false);
        optionUI.gameObject.SetActive(false);

        switch(index)
        {
            case 0 :
                mainMenuUI.gameObject.SetActive(true);
                break;
            case 1 :
                saveSlotUI.gameObject.SetActive(true);
                break;
            case 2 :
                optionUI.gameObject.SetActive(true);
                break;
        }
    }

    private void EnterPressed()
    {
        if(MainMenu.instance.mainMenuUI.gameObject.activeInHierarchy)
        {
            if(MainMenu.instance.mainMenuUI.selectIndex >= 0)
            {
                MainMenu.instance.mainMenuUI.OnClick(MainMenu.instance.mainMenuUI.selectIndex);
            }
            
                
        }else if(MainMenu.instance.saveSlotUI.gameObject.activeInHierarchy)
        {
            if(MainMenu.instance.saveSlotUI.saveSlotIndex >= 0)
            {
                MainMenu.instance.saveSlotUI.SlotClick(MainMenu.instance.saveSlotUI.saveSlotIndex);
            }

        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy)
        {
            if(OptionUI.panelNum == 0)
            {
                OptionUI.panelNum = OptionUI.panelsearchNum;
            }else if(OptionUI.panelNum == 1)
            {
                
            }else if(OptionUI.panelNum == 2)
            {
                MainMenu.instance.optionUI.OkVideoOption();
            }else if(OptionUI.panelNum == 3)
            {
                MainMenu.instance.optionUI.OkAudioOption();
            }else if(OptionUI.panelNum == 4)
            {
                MainMenu.instance.optionUI.OkGeneralOption();
            }
        }
    }
    private void BackPressed()
    {
        if(MainMenu.instance.optionUI.gameObject.activeInHierarchy && OptionUI.panelNum == 0)
        {
            
        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy && OptionUI.panelNum != 0)
        {
            int beforeInt = OptionUI.panelsearchNum;
            OptionUI.panelNum = 0;
            OptionUI.panelsearchNum = beforeInt;
            OptionUI.videoIndex = -1;
            OptionUI.audioIndex = -1;
            OptionUI.generalIndex = -1;
            return;
        }

        CurrentWindowLayer = 0;
        GameManager.EventManager.Invoke_MainMenuWindowLayer_Change_Event();
    }

    private void LeftPressed()
    {
        if(MainMenu.instance.mainMenuUI.gameObject.activeInHierarchy)
        {
                
        }else if(MainMenu.instance.saveSlotUI.gameObject.activeInHierarchy)
        {
            int value = MainMenu.instance.saveSlotUI.saveSlotIndex;
            value -= 1;

            MainMenu.instance.saveSlotUI.saveSlotIndex = Mathf.Clamp(value, 0,9);

        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy)
        {

        }
    }

    private void RightPressed()
    {
        if(MainMenu.instance.mainMenuUI.gameObject.activeInHierarchy)
        {
                
        }else if(MainMenu.instance.saveSlotUI.gameObject.activeInHierarchy)
        {
            int value = MainMenu.instance.saveSlotUI.saveSlotIndex;
            value += 1;

            MainMenu.instance.saveSlotUI.saveSlotIndex = Mathf.Clamp(value, 0,9);

        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy)
        {
            
        }
    }

    private void UPPressed()
    {
        if(MainMenu.instance.mainMenuUI.gameObject.activeInHierarchy)
        {
            int value = MainMenu.instance.mainMenuUI.selectIndex;
            value--;
            if(MainMenu.instance.mainMenuUI.continueNum == -1)
            {
                MainMenu.instance.mainMenuUI.selectIndex = Mathf.Clamp(value, 1 ,4);
            }else 
            {
                MainMenu.instance.mainMenuUI.selectIndex = Mathf.Clamp(value, 0 ,4);
            }
            
        }else if(MainMenu.instance.saveSlotUI.gameObject.activeInHierarchy)
        {
            int value = MainMenu.instance.saveSlotUI.saveSlotIndex;
            value -= 2;

            MainMenu.instance.saveSlotUI.saveSlotIndex = Mathf.Clamp(value, 0,9);

        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy)
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

    private void DownPressed()
    {
        if(MainMenu.instance.mainMenuUI.gameObject.activeInHierarchy)
        {
            int value = MainMenu.instance.mainMenuUI.selectIndex;
            value++;
            if(MainMenu.instance.mainMenuUI.continueNum == -1)
            {
                MainMenu.instance.mainMenuUI.selectIndex = Mathf.Clamp(value, 1 ,4);
            }else 
            {
                MainMenu.instance.mainMenuUI.selectIndex = Mathf.Clamp(value, 0 ,4);
            }
                
        }else if(MainMenu.instance.saveSlotUI.gameObject.activeInHierarchy)
        {
            int value = MainMenu.instance.saveSlotUI.saveSlotIndex;
            value += 2;

            MainMenu.instance.saveSlotUI.saveSlotIndex = Mathf.Clamp(value, 0,9);

        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy)
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

    private void RemoveSaveSlot()
    {
        if(MainMenu.instance.saveSlotUI.gameObject.activeInHierarchy)
        {
            MainMenu.instance.saveSlotUI.Delete(MainMenu.instance.saveSlotUI.saveSlotIndex);
        }
        
    }
}
