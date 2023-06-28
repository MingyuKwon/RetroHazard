using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class AlertUILogic
{
    AlertUI monoBehaviour;
    Text[] texts;
    CallBackInterface CallbackScript;

    public AlertUILogic(AlertUI monoBehaviour, Text[] texts)
    {
        this.texts = texts;
        this.monoBehaviour = monoBehaviour;
    }

    public void OnEnable() {
        GameMangerInput.getInput(InputType.AlertUIInput);
    }

    public void OnDisable() {
        GameMangerInput.releaseInput(InputType.AlertUIInput);
        CallbackScript = null;
    }

    public void ShowAlert(string dialog, CallBackInterface CallbackScript , string Yes = "yes", string No = "No")
    {   
        monoBehaviour.gameObject.SetActive(true);
        texts[0].text =  dialog;
        texts[1].text =  Yes;
        texts[2].text =  No;

        this.CallbackScript = CallbackScript;
    }

    public void ActiveCallback() // 이건 버튼에서 yes를 눌렀을 때 실행 될 것
    {
        CallbackScript.CallBack();
        CloseAlert();
    }

    public void CloseAlert()
    {
        monoBehaviour.gameObject.SetActive(false);
    }
}
