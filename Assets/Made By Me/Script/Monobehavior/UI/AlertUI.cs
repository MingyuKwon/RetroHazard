using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class AlertUI : MonoBehaviour
{
    AlertUILogic alertUILogic;
    public int previousInputRule;

    public static AlertUI instance;

    
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }


        this.gameObject.SetActive(false);

        alertUILogic = new AlertUILogic(GetComponentsInChildren<Text>());
    }

    private void OnEnable() {
        previousInputRule = GameMangerInput.instance.currentInputRule;
        GameMangerInput.instance.changePlayerInputRule(3);
    }

    private void OnDisable() {
        GameMangerInput.instance.changePlayerInputRule(previousInputRule);
        CallbackScript = null;
    }



    public void ShowAlert(string dialog, CallBackInterface CallbackScript , string Yes = "yes", string No = "No")
    {   
        alertUILogic.ShowAlert(dialog, CallbackScript , Yes, No);
    }



    

    private void LeftClicked(InputActionEventData data)
    {

    }


    private void RightClicked(InputActionEventData data)
    {
        CloseAlert();
    }
}
