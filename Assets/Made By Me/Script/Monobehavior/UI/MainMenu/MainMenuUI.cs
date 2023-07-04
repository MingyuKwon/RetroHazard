using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuUI : MonoBehaviour, CallBackInterface
{
    public int selectIndex{
        get{
            return _selectIndex;
        }

        set{
            _selectIndex = value;
            for(int i=0; i < buttons.Length; i++)
            {
                buttons[i].transform.GetChild(1).gameObject.SetActive(false);
            }

            if(_selectIndex >= 0)
            {
                buttons[_selectIndex].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    int _selectIndex;
    Button[] buttons;
    public int continueNum; // continue 눌렀을 때 시작할 세이브 슬롯 번호

    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
    }
    
    private void OnEnable() {
        selectIndex = 0;

        FindContinueSlot();

        if(continueNum == -1)
        {
            buttons[0].gameObject.SetActive(false);
        }

        String[] texts = {
            "<i><b>-Input-</b></i>\n\n<b>ENTER</b> : \nspace\n\n<b>BACK</b> : \nbackSpace"
        };

        GameManager.EventManager.InvokeShowNotice("MainMenuUI", texts , false, 200 ,250);
    }
    private void OnDisable() {
        for(int i=0; i<buttons.Length; i++)
        {
            int temp = i;
            buttons[i].onClick.RemoveAllListeners();
        }

        continueNum = -1;
        GameManager.EventManager.InvokeShowNotice("MainMenuUI");
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

    public void OnClick(int n)
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
            MainMenu.instance.CurrentWindowLayer = 1;
            GameManager.EventManager.Invoke_MainMenuWindowLayer_Change_Event();
        }else if(n == 3)
        {
            MainMenu.instance.CurrentWindowLayer = 2;
            GameManager.EventManager.Invoke_MainMenuWindowLayer_Change_Event();
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
