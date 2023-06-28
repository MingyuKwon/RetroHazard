using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class AlertUI : MonoBehaviour
{
    AlertUILogic alertUILogic;
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
        alertUILogic = new AlertUILogic(this, GetComponentsInChildren<Text>());
        this.gameObject.SetActive(false);

        
    }

    private void OnEnable() {
        alertUILogic.OnEnable();
    }

    private void OnDisable() {
        alertUILogic.OnDisable();
    }

    public void ShowAlert(string dialog, CallBackInterface CallbackScript , string Yes = "yes", string No = "No")
    {   
        alertUILogic.ShowAlert(dialog, CallbackScript , Yes, No);
    }


}
