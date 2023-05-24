using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;

public class UI : MonoBehaviour
{
    public static UI instance;

    public PlayerStatus status;
    public BoxUI boxUI;
    public InGameUI inGameUI;
    public TabUI tabUI;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        status = FindObjectOfType<PlayerStatus>();

        boxUI = GetComponentInChildren<BoxUI>();
        inGameUI = GetComponentInChildren<InGameUI>();
        tabUI = GetComponentInChildren<TabUI>();

    }

    public void MouseCursor(bool flag)
    {
        transform.GetChild(6).transform.GetChild(0).gameObject.SetActive(flag);
    }

    private void OnEnable() {
        SaveSystem.LoadEvent += TotalUIUpdate;
        PauseMainUI.GotoMainMenuEvent += DestroyMyself;
    }

    private void OnDisable() {
        SaveSystem.LoadEvent -= TotalUIUpdate;
        PauseMainUI.GotoMainMenuEvent -= DestroyMyself;
    }

    private void DestroyMyself()
    {
        Destroy(this.gameObject);
    }

    private void TotalUIUpdate()
    {
        boxUI.UpdateBoxUI();
        status.UpdateIngameUI();
        tabUI.UpdateTabUI();
    }
}
