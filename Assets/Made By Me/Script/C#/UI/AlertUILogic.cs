using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class AlertUILogic : MonoBehaviour
{
    Text[] texts;
    CallBackInterface CallbackScript;

    public AlertUILogic(Text[] texts)
    {
        this.texts = texts;
    }

    public void OnEnable() {
        
    }

    public void OnDisable() {
        
    }

    public void ShowAlert(string dialog, CallBackInterface CallbackScript , string Yes = "yes", string No = "No")
    {   
        this.gameObject.SetActive(true);
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
        this.gameObject.SetActive(false);
    }
}
