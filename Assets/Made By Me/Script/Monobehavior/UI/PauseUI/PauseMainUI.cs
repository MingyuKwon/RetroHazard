using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMainUI : MonoBehaviour, CallBackInterface
{
    bool isFirstSelect = true;

    public int currentIndex{
        get{
            return _currentIndex;
        }

        set{
            _currentIndex = value;

            if(_currentIndex == -1)
            {
                for(int i=0; i<4; i++)
                {
                    buttons[i].SetFocus(false);
                }
            }else
            {
                for(int i=0; i<4; i++)
                {
                    buttons[i].SetFocus(false);
                }

                buttons[_currentIndex].SetFocus(true);

                if(isFirstSelect)
                {
                    isFirstSelect = false;
                    return;
                }

                GameAudioManager.instance.PlayUIMusic(UIAudioType.Move);
            }
        }
    }
    int _currentIndex;

    UIButton[] buttons; // 0 : resume, 1 : load,  2 : option, 3 : go to main menu

    private void Awake() {
        buttons = GetComponentsInChildren<UIButton>();
    }

    private void OnEnable() {
        String[] texts = {
            "<i><b>-Input-</b></i>\n\n<b>ENTER</b> : \nspace\n\n<b>BACK</b> : \nbackSpace",
            "<i><b>-입력-</b></i>\n\n<b>확인</b> : \nspace\n\n<b>뒤로가기</b> : \nbackSpace"
        };
        GameManager.EventManager.InvokeShowNotice("PauseUI", texts , false, 200 ,250);
        currentIndex = -1;
    }

    private void OnDisable() {
        GameManager.EventManager.InvokeShowNotice("PauseUI");
        isFirstSelect = true;

    }

    public void MainMenu()
    {
        if(GameAudioManager.LanguageManager.currentLanguage == "E")
        {   
            AlertUI.instance.ShowAlert("Are you sure you want to go back to Main Menu? \n\n <b>(unsaved Data will be deleted)</b>", this);
        }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
        {
            AlertUI.instance.ShowAlert("메인 메뉴로 돌아가고 싶은게 확실하나요? \n\n <b>(저장되지 않은 진행상황은 삭제 됩니다)</b>", this);
        }

        
    }

    public void CallBack()
    {
        SceneManager.LoadScene("Main Menu");
        GameManager.EventManager.Invoke_CloseGame_GotoMainMenuEvent();
    }
}
