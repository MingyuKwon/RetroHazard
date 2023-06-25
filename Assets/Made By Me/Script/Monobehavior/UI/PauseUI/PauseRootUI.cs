using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PauseRootUI : MonoBehaviour
{
    PauseRootUILogic pauseRootUILogic;
    
    public PauseMainUI pauseMainUI;
    public SaveSlotUI saveSlotUI;
    public OptionUI optionUI;

    public int CurrentWindowLayer{
        get{
            return pauseRootUILogic.CurrentWindowLayer;
        }

        set{
            pauseRootUILogic.CurrentWindowLayer = value;
        }
    }

    private void Awake() {
        pauseMainUI = GetComponentInChildren<PauseMainUI>();
        saveSlotUI = GetComponentInChildren<SaveSlotUI>();
        optionUI = GetComponentInChildren<OptionUI>();

        pauseRootUILogic = new PauseRootUILogic(this);
    }

    private void OnEnable() {
        pauseRootUILogic.OnEnable();
    }

    private void OnDisable() { // have an error when game is closed ny force while pause Ui is opened
        pauseRootUILogic.OnDisable();
    }

    // 현재 입력은 좌클릭은 그냥 Onclick으로 떄우고 우클릭 했을 때 밖으로 나가는 형식이다
}
