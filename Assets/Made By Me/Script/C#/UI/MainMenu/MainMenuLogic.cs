using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLogic 
{
    MainMenu monoBehaviour;
    public int CurrentWindowLayer = 0; // 0: main, 1 :save Slot, 2 : option


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


        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy)
        {

        }
    }
    private void BackPressed()
    {
        CurrentWindowLayer = 0;
        GameManager.EventManager.Invoke_MainMenuWindowLayer_Change_Event();
    }

    private void LeftPressed()
    {
        Debug.Log("LeftPressed");
        if(MainMenu.instance.mainMenuUI.gameObject.activeInHierarchy)
        {
                
        }else if(MainMenu.instance.saveSlotUI.gameObject.activeInHierarchy)
        {


        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy)
        {

        }
    }

    private void RightPressed()
    {
        Debug.Log("RightPressed");
        if(MainMenu.instance.mainMenuUI.gameObject.activeInHierarchy)
        {
                
        }else if(MainMenu.instance.saveSlotUI.gameObject.activeInHierarchy)
        {


        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy)
        {

        }
    }

    private void UPPressed()
    {
        Debug.Log("UPPressed");
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


        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy)
        {

        }
    }

    private void DownPressed()
    {
        Debug.Log("DownPressed");
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


        }else if(MainMenu.instance.optionUI.gameObject.activeInHierarchy)
        {

        }
    }

    private void RemoveSaveSlot()
    {
        if(MainMenu.instance.saveSlotUI.gameObject.activeInHierarchy)
        {


        }
       // MainMenu.instance.saveSlotUI.Delete();
    }
}
