using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMainUI : MonoBehaviour, CallBackInterface
{

    public static event Action GotoMainMenuEvent;
    Button[] buttons; // 0 : resume, 1 : load,  2 : option, 3 : go to main menu
    PauseRootUI rootUI;

    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
        rootUI = GetComponentInParent<PauseRootUI>();
    }

    private void Start() {
        buttons[0].onClick.AddListener(Resume);
        buttons[1].onClick.AddListener(Load);
        buttons[2].onClick.AddListener(Option);
        buttons[3].onClick.AddListener(MainMenu);
    }

    private void Resume()
    {
        rootUI.CurrentWindowLayer = -1;
        rootUI.windowLayer_Change_Invoke();
    }

    private void Load()
    {
        rootUI.CurrentWindowLayer = 1;
        rootUI.windowLayer_Change_Invoke();
    }

    private void Option()
    {
        rootUI.CurrentWindowLayer = 2;
        rootUI.windowLayer_Change_Invoke();
    }

    private void MainMenu()
    {
        AlertUI.instance.ShowAlert("Are you sure you want to go back to Main Menu? \n\n <b>(unsaved Data will be deleted)</b>", this);
    }

    public void CallBack()
    {
        SceneManager.LoadScene("Main Menu");
        GotoMainMenuEvent.Invoke();
    }
}
