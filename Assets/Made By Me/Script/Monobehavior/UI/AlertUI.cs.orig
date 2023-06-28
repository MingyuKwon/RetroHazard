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

    public int alertIndex{
        get{
            return alertUILogic.alertIndex;
        }

        set{
            alertUILogic.alertIndex = value;

            if(alertUILogic.alertIndex == -1)
            {
                for(int i=0; i<2; i++)
                {
                    buttons[i].SetFocus(false);
                }
            }else
            {
                for(int i=0; i<2; i++)
                {
                    buttons[i].SetFocus(false);
                }

                buttons[alertUILogic.alertIndex].SetFocus(true);
            }
        }
    }

    UIButton[] buttons; // 0 : Yes, 1 : No

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }
        buttons = GetComponentsInChildren<UIButton>();

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
