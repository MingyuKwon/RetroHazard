using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMainUI : MonoBehaviour, CallBackInterface
{
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
            }
        }
    }
    int _currentIndex;

    UIButton[] buttons; // 0 : resume, 1 : load,  2 : option, 3 : go to main menu

    private void Awake() {
        buttons = GetComponentsInChildren<UIButton>();
    }

    private void OnEnable() {
        currentIndex = 0;
    }

    private void OnDisable() {
        
    }

    public void MainMenu()
    {
        AlertUI.instance.ShowAlert("Are you sure you want to go back to Main Menu? \n\n <b>(unsaved Data will be deleted)</b>", this);
    }

    public void CallBack()
    {
        SceneManager.LoadScene("Main Menu");
        GameManager.EventManager.Invoke_CloseGame_GotoMainMenuEvent();
    }
}
