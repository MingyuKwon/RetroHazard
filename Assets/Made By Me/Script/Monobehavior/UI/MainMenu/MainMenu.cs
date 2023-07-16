using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    MainMenuLogic mainMenuLogic;

    public static MainMenu instance;

    public MainMenuUI mainMenuUI;
    public SaveSlotUI saveSlotUI;
    public OptionUI optionUI;


    public int CurrentWindowLayer {
        get{
            return mainMenuLogic.CurrentWindowLayer;
        }
        set{
            mainMenuLogic.CurrentWindowLayer =  value;
        }
    }
     
    private void Awake() {
        instance = this;

        mainMenuUI = GetComponentInChildren<MainMenuUI>();
        saveSlotUI = GetComponentInChildren<SaveSlotUI>();
        optionUI = GetComponentInChildren<OptionUI>();

        mainMenuLogic = new MainMenuLogic(this);
    }

    private void OnEnable() {
        mainMenuLogic.OnEnable();
        GameAudioManager.instance.PlayBackGroundMusic(BackGroundAudioType.MainMenu);
    }

    private void OnDisable() {
        mainMenuLogic.OnDisable();
        instance = null;
    }
    
}
