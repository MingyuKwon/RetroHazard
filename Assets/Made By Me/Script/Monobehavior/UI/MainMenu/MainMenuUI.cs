using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuUI : MonoBehaviour, CallBackInterface
{
    bool openFirstSelect = true;

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

                if(openFirstSelect)
                {
                    openFirstSelect = false;
                    return;
                }
                GameAudioManager.instance.PlayUIMusic(UIAudioType.Move);
            }
        }
    }

    int _selectIndex;
    Button[] buttons;

    Text[] buttonTexts;
    string[] englishText = { 
    "Continue" , 
    "New Game" ,
    "Load Game" ,
    "Option" ,
    "Quit" ,};

    string[] koreanText = { 
    "계속하기" , 
    "처음부터" ,
    "이어하기" ,
    "설정" ,
    "나가기" ,};

    public int continueNum; // continue 눌렀을 때 시작할 세이브 슬롯 번호

    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
        buttonTexts = transform.GetChild(2).GetComponentsInChildren<Text>();

        changeText(GameAudioManager.LanguageManager.currentLanguage);

        GameAudioManager.LanguageManager.languageChangeEvent += changeText;
    }

    private void OnDestroy() {
        GameAudioManager.LanguageManager.languageChangeEvent -= changeText;
    }

    private void changeText(string language)
    {
        if(language == "E")
        {
            for(int i=0; i<buttonTexts.Length; i++)
            {
                buttonTexts[i].text = englishText[i];
            }
        }else if(language == "K")
        {
            for(int i=0; i<buttonTexts.Length; i++)
            {
                buttonTexts[i].text = koreanText[i];
            }
        }
    }
    
    private void OnEnable() {
        selectIndex = -1;

        FindContinueSlot();

        if(continueNum == -1)
        {
            buttons[0].gameObject.SetActive(false);
        }

        String[] texts = {
            "<i><b>-Input-</b></i>\n\n<b>ENTER</b> : \nspace\n\n<b>BACK</b> : \nbackSpace",
            "<i><b>-입력-</b></i>\n\n<b>확인</b> : \nspace\n\n<b>뒤로가기</b> : \nbackSpace"
        };

        GameManager.EventManager.InvokeShowNotice("MainMenuUI", texts , false, 200 ,250);
    }
    private void OnDisable() {
        for(int i=0; i<buttons.Length; i++)
        {
            int temp = i;
            buttons[i].onClick.RemoveAllListeners();
        }

        openFirstSelect = true;

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
            if(GameAudioManager.LanguageManager.currentLanguage == "E")
        {   
            AlertUI.instance.ShowAlert("Are you sure you want to start new Game?", this);
        }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
        {
            AlertUI.instance.ShowAlert("새로운 게임을 시작하시겠습니까?", this);
        }

            

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
