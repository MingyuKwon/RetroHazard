using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUILogic
{
    OptionUI monoBehaviour;

    public int panelNum = 0; // 0 -> Control, 1 -> Audio, 2-> Vidio. 3-> General

    GameObject[] panels;
    Button[] buttons; // 0 : Control, 1 : Display, 2 : Sound, 3 : General

    string[] OptionText = {
        "<i><b>-Input-</b></i>\n\n<b>ENTER</b> : space   <b>BACK</b> : backSpace",
    };

    //Audio//
    Slider[] AudioSliders;
    Button AudioResetButton;
    //Audio//

    public OptionUILogic(OptionUI monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;

        panels = new GameObject[monoBehaviour.transform.GetChild(2).childCount];

        for(int i=0; i< panels.Length; i++)
        {
            panels[i] = monoBehaviour.transform.GetChild(2).GetChild(i).gameObject;
        }

        ////////Audio//////////////////
        AudioSliders = panels[2].GetComponentsInChildren<Slider>();
        AudioResetButton = panels[2].GetComponentInChildren<Button>();

        AudioSliders[0].maxValue = 1;
        AudioSliders[0].minValue = 0;
        AudioSliders[0].value = GameAudioManager.totalVolme;
        AudioSliders[0].onValueChanged.AddListener(UpdateSliderValue0);

        AudioSliders[1].maxValue = 1;
        AudioSliders[1].minValue = 0;
        AudioSliders[1].value = GameAudioManager.currentBackGroundVolume;
        AudioSliders[1].onValueChanged.AddListener(UpdateSliderValue1);

        AudioSliders[2].maxValue = 1;
        AudioSliders[2].minValue = 0;
        AudioSliders[2].value = GameAudioManager.currentUIVolume;
        AudioSliders[2].onValueChanged.AddListener(UpdateSliderValue2);

        AudioSliders[3].maxValue = 1;
        AudioSliders[3].minValue = 0;
        AudioSliders[3].value = GameAudioManager.currentSFXVolume;
        AudioSliders[3].onValueChanged.AddListener(UpdateSliderValue3);

        AudioSliders[4].maxValue = 1;
        AudioSliders[4].minValue = 0;
        AudioSliders[4].value = GameAudioManager.currentEnvironmentVolume;
        AudioSliders[4].onValueChanged.AddListener(UpdateSliderValue4);

        AudioResetButton.onClick.AddListener(ResetAudioOption);
        ////////Audio//////////////////
    }

    private void ResetAudioOption()
    {
            GameAudioManager.totalVolme = GameAudioManager.default_totalVolme;
            GameAudioManager.currentBackGroundVolume= GameAudioManager.default_currentBackGroundVolume;
            GameAudioManager.currentUIVolume= GameAudioManager.default_currentUIVolume;
            GameAudioManager.currentSFXVolume= GameAudioManager.default_currentSFXVolume;
            GameAudioManager.currentEnvironmentVolume= GameAudioManager.default_currentEnvironmentVolume;

            AudioSliders[0].value = GameAudioManager.totalVolme;
            AudioSliders[1].value = GameAudioManager.currentBackGroundVolume;
            AudioSliders[2].value = GameAudioManager.currentUIVolume;
            AudioSliders[3].value = GameAudioManager.currentSFXVolume;
            AudioSliders[4].value = GameAudioManager.currentEnvironmentVolume;
    }

    private void UpdateSliderValue0(float value)
    {
        GameAudioManager.totalVolme = value;
    }

    private void UpdateSliderValue1(float value)
    {
        GameAudioManager.currentBackGroundVolume = value;
    }

    private void UpdateSliderValue2(float value)
    {
        GameAudioManager.currentUIVolume = value;
    }

    private void UpdateSliderValue3(float value)
    {
        GameAudioManager.currentSFXVolume = value;
    }

    private void UpdateSliderValue4(float value)
    {
        GameAudioManager.currentEnvironmentVolume = value;
    }

    public void ChangePanel(int index)
    {
        for(int i=0; i< panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        panels[index].SetActive(true);
    }

    public void OnEnable() {
        GameManager.EventManager.InvokeShowNotice("OptionUI", OptionText , false, 1000 ,130);
    }

    public void OnDisable() {
        GameManager.EventManager.InvokeShowNotice("OptionUI");
    }



    
}
