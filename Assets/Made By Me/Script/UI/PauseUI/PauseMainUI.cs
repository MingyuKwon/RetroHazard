using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMainUI : MonoBehaviour
{
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
        GameManager.instance.SetPauseGameInput();
    }

    private void Load()
    {
        GameManagerUI.instance.Visualize_SaveUI(true, false);
        rootUI.CurrentWindowLayer++;
    }

    private void Option()
    {

    }

    private void MainMenu()
    {

    }
}
