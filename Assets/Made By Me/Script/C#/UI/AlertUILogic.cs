using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class AlertUILogic
{
    AlertUI monoBehaviour;
    Text[] texts;
    CallBackInterface CallbackScript;

    bool openFirstSelect = true;
    public int alertIndex {
        get{
            return _alertIndex;
        }

        set{
            _alertIndex = value;
            if(openFirstSelect)
            {
                openFirstSelect = false;
                return;
            }
            GameAudioManager.instance.PlayUIMusic(UIAudioType.Move);
        }
    }

    int _alertIndex = 0;

    bool isClickYes = false;


    public AlertUILogic(AlertUI monoBehaviour, Text[] texts)
    {
        this.texts = texts;
        this.monoBehaviour = monoBehaviour;
    }

    public void OnEnable() {
        GameMangerInput.InputEvent.AlertUIEnterPressed += ActiveCallback;
        GameMangerInput.InputEvent.AlertUIBackPressed += CloseAlert;
        GameMangerInput.InputEvent.AlertUILeftPressed += LeftPressed;
        GameMangerInput.InputEvent.AlertUIRightPressed += RightPressed;
        GameMangerInput.getInput(InputType.AlertUIInput);
        AlertUI.instance.alertIndex = 0;
        isClickYes = false;

    }

    public void OnDisable() {
        GameMangerInput.releaseInput(InputType.AlertUIInput);
        GameMangerInput.InputEvent.AlertUIEnterPressed -= ActiveCallback;
        GameMangerInput.InputEvent.AlertUIBackPressed -= CloseAlert;
        GameMangerInput.InputEvent.AlertUILeftPressed -= LeftPressed;
        GameMangerInput.InputEvent.AlertUIRightPressed -= RightPressed;
        CallbackScript = null;
        AlertUI.instance.alertIndex = 0;

        openFirstSelect = true;
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
        if(AlertUI.instance.alertIndex == 0)
        {
            CallbackScript.CallBack();
            isClickYes = true;
            CloseAlert();
        }else if(AlertUI.instance.alertIndex == 1)
        {
            isClickYes = false;
            CloseAlert();
        }
        
    }

    public void CloseAlert()
    {
        if(isClickYes)
        {   
            GameAudioManager.instance.PlayUIMusic(UIAudioType.Click);
        }else
        {
            GameAudioManager.instance.PlayUIMusic(UIAudioType.Back);
        }

        monoBehaviour.gameObject.SetActive(false);
    }

    public void LeftPressed()
    {
       int value = AlertUI.instance.alertIndex;
       value--;
       AlertUI.instance.alertIndex = Mathf.Clamp(value, 0 , 1);
    }

    public void RightPressed()
    {
       int value = AlertUI.instance.alertIndex;
       value++;
       AlertUI.instance.alertIndex = Mathf.Clamp(value, 0 , 1);
    }
}
