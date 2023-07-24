using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
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

    //DisPlay//
    Slider VideoSlider;
    Dropdown VideoDropDown;
    Button VideoResetButton;
    //DisPlay//

    //Audio//
    Slider[] AudioSliders;
    Button AudioResetButton;
    //Audio//

    //General//
    Dropdown LanguageDropDown;
    Button GeneralResetButton;
    //General//

    public OptionUILogic(OptionUI monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;

        panels = new GameObject[monoBehaviour.transform.GetChild(2).childCount];

        for(int i=0; i< panels.Length; i++)
        {
            panels[i] = monoBehaviour.transform.GetChild(2).GetChild(i).gameObject;
        }

        ////////Video//////////////////
        VideoSlider = panels[1].GetComponentInChildren<Slider>();
        VideoDropDown = panels[1].GetComponentInChildren<Dropdown>();
        VideoResetButton = panels[1].GetComponentInChildren<Button>();

        VideoSlider.maxValue = 1;
        VideoSlider.minValue = 0.2f;

        VideoSlider.value = ScreenBright.ScreenLight;

        VideoSlider.onValueChanged.AddListener(UpdateVideoSlider);

        VideoDropDown.options.Clear();
        List<string> resolutionItem = new List<string> {"1920 X 1080","1600 X 900" ,"1280 X 720", "960 X 540"};

        foreach (var item in resolutionItem)
        {
            VideoDropDown.options.Add(new Dropdown.OptionData() { text = item });
        }

        switch(PlayerPrefs.GetInt("Resolution"))
        {
            case 1080 :
                VideoDropDown.value = 0;
                break;
            case 900 :
                VideoDropDown.value = 1;
                break;
            case 720 :
                VideoDropDown.value = 2;
                break;
            case 540 :
                VideoDropDown.value = 3;
                break;
        }
        VideoDropDown.onValueChanged.AddListener(delegate { DropdownVideoValueChanged(VideoDropDown); });

        VideoResetButton.onClick.AddListener(ResetVideoOption);
        ////////Video//////////////////

        ////////Audio//////////////////
        AudioSliders = panels[2].GetComponentsInChildren<Slider>();
        AudioResetButton = panels[2].GetComponentInChildren<Button>();

        AudioSliders[0].maxValue = 1;
        AudioSliders[0].minValue = 0;
        AudioSliders[0].value = GameAudioManager.totalVolme;
        AudioSliders[0].onValueChanged.AddListener(UpdateAudioSliderValue0);

        AudioSliders[1].maxValue = 1;
        AudioSliders[1].minValue = 0;
        AudioSliders[1].value = GameAudioManager.currentBackGroundVolume;
        AudioSliders[1].onValueChanged.AddListener(UpdateAudioSliderValue1);

        AudioSliders[2].maxValue = 1;
        AudioSliders[2].minValue = 0;
        AudioSliders[2].value = GameAudioManager.currentUIVolume;
        AudioSliders[2].onValueChanged.AddListener(UpdateAudioSliderValue2);

        AudioSliders[3].maxValue = 1;
        AudioSliders[3].minValue = 0;
        AudioSliders[3].value = GameAudioManager.currentSFXVolume;
        AudioSliders[3].onValueChanged.AddListener(UpdateAudioSliderValue3);

        AudioSliders[4].maxValue = 1;
        AudioSliders[4].minValue = 0;
        AudioSliders[4].value = GameAudioManager.currentEnvironmentVolume;
        AudioSliders[4].onValueChanged.AddListener(UpdateAudioSliderValue4);

        AudioResetButton.onClick.AddListener(ResetAudioOption);
        ////////Audio//////////////////

        LanguageDropDown = panels[3].GetComponentInChildren<Dropdown>();
        GeneralResetButton = panels[3].GetComponentInChildren<Button>();

        switch(GameAudioManager.LanguageManager.currentLanguage)
        {
            case "E" :
                LanguageDropDown.value = 0;
                break;
            case "K" :
                LanguageDropDown.value = 1;
                break;
        }

        LanguageDropDown.onValueChanged.AddListener(delegate { DropdownLanguageValueChanged(LanguageDropDown); });
    
        GeneralResetButton.onClick.AddListener(ResetGenralOption);
    }


    void DropdownVideoValueChanged(Dropdown change)
    {
        switch(change.value)
        {
            case 0 :
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 1080);
                break;
            case 1 :
                Screen.SetResolution(1600, 900, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 900);
                break;
            case 2 :
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 720);
                break;
            case 3 :
                Screen.SetResolution(960, 540, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 540);
                break;
        }
        
    }

    void DropdownLanguageValueChanged(Dropdown change)
    {
        switch(change.value)
        {
            case 0 :
                GameAudioManager.LanguageManager.Invoke_languageChangeEvent("E");
                break;
            case 1 :
                GameAudioManager.LanguageManager.Invoke_languageChangeEvent("K");
                break;
        }
        
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

    private void ResetVideoOption()
    {
        ScreenBright.ScreenLight = 1f;
        VideoSlider.value = ScreenBright.ScreenLight;

        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
        PlayerPrefs.SetInt("Resolution", 1080);
        VideoDropDown.value = 0;
    }

    private void ResetGenralOption()
    {
        GameAudioManager.LanguageManager.Invoke_languageChangeEvent("E");
        LanguageDropDown.value = 0;
    }


    private void UpdateVideoSlider(float value)
    {
        ScreenBright.ScreenLight = value;
    }

    private void UpdateAudioSliderValue0(float value)
    {
        GameAudioManager.totalVolme = value;
    }

    private void UpdateAudioSliderValue1(float value)
    {
        GameAudioManager.currentBackGroundVolume = value;
    }

    private void UpdateAudioSliderValue2(float value)
    {
        GameAudioManager.currentUIVolume = value;
    }

    private void UpdateAudioSliderValue3(float value)
    {
        GameAudioManager.currentSFXVolume = value;
    }

    private void UpdateAudioSliderValue4(float value)
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
