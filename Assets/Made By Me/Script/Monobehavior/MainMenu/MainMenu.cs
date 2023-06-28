using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public static event Action windowLayer_Change_Event;

    GameObject[] UIs; // 0: main, 1 :save Slot, 2 : option
    private Player player;

    public int CurrentWindowLayer = 0; // 0: main, 1 :save Slot, 2 : option
     
    private void Awake() {
        UIs = new GameObject[transform.childCount];
        player = ReInput.players.GetPlayer(0);

        for(int i=0; i< UIs.Length; i++)
        {
            UIs[i] = transform.GetChild(i).gameObject;
        }

        player.AddInputEventDelegate(LeftClicked, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "MouseLeftButton");
        player.AddInputEventDelegate(RightClicked, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "MouseRightButton");
    }

    private void OnEnable() {
        GameMangerInput.getInput(InputType.MenuUIInput);
        windowLayer_Change_Event += windowLayer_Check;
    }

    private void OnDisable() {
        GameMangerInput.releaseInput(InputType.MenuUIInput);
        windowLayer_Change_Event -= windowLayer_Check;
    }

    private void OnDestroy() {
        player.RemoveInputEventDelegate(LeftClicked);
        player.RemoveInputEventDelegate(RightClicked);
    }

    public void ActiveUI(int index)
    {
        for(int i=0; i< UIs.Length; i++)
        {
            UIs[i].SetActive(false);
        }

        UIs[index].SetActive(true);
    }

    private void LeftClicked(InputActionEventData data)
    {

    }
    private void RightClicked(InputActionEventData data)
    {

        CurrentWindowLayer = 0;
        
        windowLayer_Change_Event.Invoke();
        
    }

    public void windowLayer_Change_Invoke()
    {
        windowLayer_Change_Event.Invoke();
    }

    private void windowLayer_Check()
    {

        if(CurrentWindowLayer == 0)
        {
            ActiveUI(0);
        }

        if(CurrentWindowLayer == 1)
        {
           ActiveUI(1);
        }

        if(CurrentWindowLayer == 2)
        {
           ActiveUI(2);
        }
    }
}
