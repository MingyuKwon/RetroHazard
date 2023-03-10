using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuUI : MonoBehaviour, CallBackInterface
{
    Button[] buttons;
    MainMenu mainMenu;
    MouseUI mouseUI;
    int continueNum;

    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
        mainMenu = transform.parent.GetComponent<MainMenu>();
        

    }
    
    private void OnEnable() {

        for(int i=0; i<buttons.Length; i++)
        {
            int temp = i;
            buttons[i].onClick.AddListener(() => OnClick(temp));
        }

        FindContinueSlot();

        if(continueNum == -1)
        {
            buttons[0].gameObject.SetActive(false);
        }

    }
    private void OnDisable() {

        for(int i=0; i<buttons.Length; i++)
        {
            int temp = i;
            buttons[i].onClick.RemoveAllListeners();
        }

        continueNum = -1;
    }

    public void CallBack()
    {
        SaveSystem.SaveSlotNum = -1;
        SaveSystem.instance.Load(0);
    }

    private void FindContinueSlot()
    {
        int latestNum = -1;
        DateTime latestSaveTime = new DateTime();

        for(int i=0; i<SaveSystem.instance.saveSlotInfos.Length; i++)
        {
            if(SaveSystem.instance.saveSlotInfos[i].saveTime == "Empty") continue;

            DateTime dateTime = Convert.ToDateTime(SaveSystem.instance.saveSlotInfos[i].saveTime);
            
            if(DateTime.Compare(dateTime, latestSaveTime) > 0)
            {
                latestSaveTime = dateTime;
                latestNum = i;
            }
        }

        continueNum = latestNum;
            
    }

    private void OnClick(int n)
    {
        if(n == 0)
        {
            SaveSystem.SaveSlotNum = continueNum;
            SaveSystem.instance.Load(0);

        }else if(n == 1) // new Game
        {
            AlertUI.instance.ShowAlert("Are you sure you want to start new Game?", this);

        }else if(n == 2)
        {
            mainMenu.CurrentWindowLayer = 1;
            mainMenu.windowLayer_Change_Invoke();
        }else if(n == 3)
        {
            mainMenu.CurrentWindowLayer = 2;
            mainMenu.windowLayer_Change_Invoke();
        }else if(n == 4)
        {
            Quit();
        }

        
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
            
    }
}
