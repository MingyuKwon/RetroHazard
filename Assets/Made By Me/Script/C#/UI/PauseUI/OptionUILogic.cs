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
        "<i><b>-입력-</b></i>\n\n<b>확인</b> : space   <b>뒤로가기</b> : backSpace",
    };

    string[] englishButtonText = { 
    "Control" , 
    "Display" ,
    "Audio" ,
    "General" ,};

    string[] koreanButtonText = { 
    "조작" , 
    "화면" ,
    "소리" ,
    "일반" ,};
    

    string englishControlText = "Control";
    string koreanControlText = "조작";
    
    string[] englishControlText1 = { 
    "Game Pad" , 
    "Move" ,
    "Move" ,
    "Pause" ,
    "Tab" ,
    "Interact\n/Ok" ,
    "Rush\n/Back" ,
    "remove" ,
    "Sheild\n/left Tab" ,
    "Sheild\nreload" ,
    "Attack\n/right Tab" ,
    "Energy\nreload" ,
    };


    string[] koreanControlText1 = { 
    "게임 패드" , 
    "이동" ,
    "이동" ,
    "일시정지" ,
    "탭 열기" ,
    "상호작용\n/확인" ,
    "달리기\n/취소" ,
    "삭제하기" ,
    "방어\n/왼쪽 탭이동" ,
    "내구도\n장전" ,
    "공격\n/오른쪽 탭이동" ,
    "에너지\n장전" ,
    };

    string[] englishControlText2 = { 
    "KeyBoard" , 
    "Mouse" ,
    "Move" ,
    "Sheild\nreload" ,
    "Energy\nreload" ,
    "left Tab" ,
    "right Tab" ,
    "remove" ,
    "Pause" ,
    "Tab" ,
    "Rush" ,
    "Back" ,
    "Interact\n/Ok" ,
    "Attack\n/Ok" ,
    "Sheild\n/Back" ,
    };

    string[] koreanControlText2 = { 
    "키보드" , 
    "마우스" ,
    "이동" ,
    "내구도\n장전" ,
    "에너지\n장전" ,
    "왼쪽 \n탭이동" ,
    "오른쪽 \n탭이동" ,
    "삭제하기" ,
    "일시정지" ,
    "탭 열기" ,
    "달리기" ,
    "취소" ,
    "상호작용\n/확인" ,
    "공격\n/확인" ,
    "방어\n/취소" ,
    };


    string[] englishDisplayText = { 
    "Display" , 
    "Brightness" ,
    "Resolution" ,};

    string[] koreanDisplayText = { 
    "화면" , 
    "밝기" ,
    "해상도" ,};

    string[] englishAudioText = { 
    "Audio",
    "Total Volume" , 
    "BackGround Volume" ,
    "UI Volume" ,
    "SFX Volume" ,
    "Environment Volume" ,};

    string[] koreanAudioText = { 
    "소리",
    "총 음량" , 
    "배경 음악 음량" ,
    "UI 음량" ,
    "효과음 음량" ,
    "주변 환경 음량" ,};

    string[] englishGeneralText = { 
    "General",
    "Language" ,};

    string[] koreanGeneralText = { 
    "일반",
    "언어" ,};

    string defaultButtonEnglishText = "Default Value";
    string defaultButtonKoreanText = "기본값";


    Text[] buttonPanelTexts;
    Text[] defaultButtonPanelTexts;

    Text[] ControlTexts;
    Text[] DisPlayTexts;
    Text[] AudioTexts;
    Text[] GeneralTexts;

    //DisPlay//
    Slider VideoSlider;
    Toggle Toggle1080;
    Toggle Toggle900;
    Toggle Toggle720;
    Toggle Toggle540;

    Button VideoResetButton;
    //DisPlay//

    //Audio//
    Slider[] AudioSliders;
    Button AudioResetButton;
    //Audio//

    //General//
    Button GeneralResetButton;
    //General//

    Toggle enlgishToggle;
    Toggle koreanToggle;

    public OptionUILogic(OptionUI monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;

        panels = new GameObject[monoBehaviour.transform.GetChild(2).childCount];

        for(int i=0; i< panels.Length; i++)
        {
            panels[i] = monoBehaviour.transform.GetChild(2).GetChild(i).gameObject;
        }

        ////////Control//////////////////
        
        ////////Control//////////////////

        ////////Video//////////////////
        VideoSlider = panels[1].GetComponentInChildren<Slider>();

        Toggle[] videoToggles = panels[1].GetComponentsInChildren<Toggle>();
        Toggle1080 = videoToggles[0];
        Toggle900 = videoToggles[1];
        Toggle720 = videoToggles[2];
        Toggle540 = videoToggles[3];
        
        VideoResetButton = panels[1].GetComponentInChildren<Button>();

        VideoSlider.maxValue = 1;
        VideoSlider.minValue = 0.2f;

        VideoSlider.value = ScreenBright.ScreenLight;

        VideoSlider.onValueChanged.AddListener(UpdateVideoSlider);

        switch(PlayerPrefs.GetInt("Resolution"))
        {
            case 1080 :
                Toggle1080.isOn = true;
                Toggle900.isOn = false;
                Toggle720.isOn = false;
                Toggle540.isOn = false;
                Toggle1080.interactable = false;
                Toggle900.interactable = true;
                Toggle720.interactable = true;
                Toggle540.interactable = true;
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 1080);
                break;
            case 900 :
                Toggle1080.isOn = false;
                Toggle900.isOn = true;
                Toggle720.isOn = false;
                Toggle540.isOn = false;
                Toggle1080.interactable = true;
                Toggle900.interactable = false;
                Toggle720.interactable = true;
                Toggle540.interactable = true;
                Screen.SetResolution(1600, 900, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 900);
                break;
            case 720 :
                Toggle1080.isOn = false;
                Toggle900.isOn = false;
                Toggle720.isOn = true;
                Toggle540.isOn = false;
                Toggle1080.interactable = true;
                Toggle900.interactable = true;
                Toggle720.interactable = false;
                Toggle540.interactable = true;
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 720);
                break;
            case 540 :
                Toggle1080.isOn = false;
                Toggle900.isOn = false;
                Toggle720.isOn = false;
                Toggle540.isOn = true;
                Toggle1080.interactable = true;
                Toggle900.interactable = true;
                Toggle720.interactable = true;
                Toggle540.interactable = false;
                Screen.SetResolution(960, 540, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 540);
                break;
        }

        Toggle1080.onValueChanged.AddListener(delegate { VideoValueChanged(1080, Toggle1080); });
        Toggle900.onValueChanged.AddListener(delegate { VideoValueChanged(900, Toggle900); });
        Toggle720.onValueChanged.AddListener(delegate { VideoValueChanged(720, Toggle720); });
        Toggle540.onValueChanged.AddListener(delegate { VideoValueChanged(540, Toggle540); });

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

        
        ////////General//////////////////
        Toggle[] toggles = panels[3].transform.GetChild(1).GetComponentsInChildren<Toggle>();
        enlgishToggle = toggles[0];
        koreanToggle = toggles[1];
        GeneralResetButton = panels[3].GetComponentInChildren<Button>();

        switch(GameAudioManager.LanguageManager.currentLanguage)
        {
            case "E" :
                enlgishToggle.isOn = true;
                enlgishToggle.interactable = false;
                koreanToggle.isOn = false;
                koreanToggle.interactable = true;
                break;
            case "K" :
                enlgishToggle.isOn = false;
                enlgishToggle.interactable = true;
                koreanToggle.isOn = true;
                koreanToggle.interactable = false;
                break;
        }

        enlgishToggle.onValueChanged.AddListener(delegate { LanguageValueChanged("E" , enlgishToggle); });
        koreanToggle.onValueChanged.AddListener(delegate { LanguageValueChanged("K", koreanToggle); });

        GeneralResetButton.onClick.AddListener(ResetGenralOption);
        ////////General//////////////////

        //Panel and default button///////////////////
        buttonPanelTexts = monoBehaviour.transform.GetChild(1).GetComponentsInChildren<Text>();
        Button[] defaultButtons = monoBehaviour.transform.GetChild(2).GetComponentsInChildren<Button>();
        defaultButtonPanelTexts = new Text[defaultButtons.Length];
        for(int i=0; i<defaultButtons.Length; i++)
        {
            defaultButtonPanelTexts[i] = defaultButtons[i].GetComponentInChildren<Text>();
        }
        //Panel and default button///////////////////

        ////////Control//////////////////
        Text[] ControlTexts1 = panels[0].transform.GetChild(1).GetChild(0).GetComponentsInChildren<Text>();
        Text[] ControlTexts2 = panels[0].transform.GetChild(1).GetChild(1).GetComponentsInChildren<Text>();
        ControlTexts = new Text[1 + ControlTexts1.Length + ControlTexts2.Length];

        int k = 0;
        ControlTexts[k] = panels[0].transform.GetChild(0).GetComponent<Text>();
        k++;

        for(int j=0; j < ControlTexts1.Length; j++)
        {
            ControlTexts[k] = ControlTexts1[j];
            k++;
        }

        for(int j=0; j < ControlTexts2.Length; j++)
        {
            ControlTexts[k] = ControlTexts2[j];
            k++;
        }
        ////////Control//////////////////

        ///////DisPlay//////////////
        DisPlayTexts = new Text[englishDisplayText.Length];
        DisPlayTexts[0] = panels[1].transform.GetChild(0).GetComponent<Text>();
        DisPlayTexts[1] = panels[1].transform.GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>();
        DisPlayTexts[2] = panels[1].transform.GetChild(1).GetChild(0).GetComponent<Text>();
        ///////DisPlay//////////////

        ///////Audio//////////////
        AudioTexts = new Text[englishAudioText.Length];
        AudioTexts[0] = panels[2].transform.GetChild(0).GetComponent<Text>();
        AudioTexts[1] = panels[2].transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>();
        AudioTexts[2] = panels[2].transform.GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>();
        AudioTexts[3] = panels[2].transform.GetChild(1).GetChild(2).GetChild(3).GetComponent<Text>();
        AudioTexts[4] = panels[2].transform.GetChild(1).GetChild(3).GetChild(3).GetComponent<Text>();
        AudioTexts[5] = panels[2].transform.GetChild(1).GetChild(4).GetChild(3).GetComponent<Text>();
        ///////Audio//////////////

        //////Genral//////////////
        GeneralTexts = new Text[englishGeneralText.Length];
        GeneralTexts[0] = panels[3].transform.GetChild(0).GetComponent<Text>();
        GeneralTexts[1] = panels[3].transform.GetChild(1).GetChild(0).GetComponent<Text>();
        //////Genral//////////////


        changeText(GameAudioManager.LanguageManager.currentLanguage);
        GameAudioManager.LanguageManager.languageChangeEvent += changeText;
    }

    public void OnDestroy() {
        GameAudioManager.LanguageManager.languageChangeEvent -= changeText;
    }

    private void changeText(string language)
    {
        if(language == "E")
        {
            for(int i=0; i<buttonPanelTexts.Length; i++)
            {
                buttonPanelTexts[i].text = englishButtonText[i];
            }

            for(int i=0; i<defaultButtonPanelTexts.Length; i++)
            {
                defaultButtonPanelTexts[i].text = defaultButtonEnglishText;
            }

            int j = 0;
            ControlTexts[j].text = englishControlText;
            j++;

            for(int i=0; i< englishControlText1.Length; i++)
            {
                ControlTexts[j].text = englishControlText1[i];
                j++;
            }

            for(int i=0; i< englishControlText2.Length; i++)
            {
                ControlTexts[j].text = englishControlText2[i];
                j++;
            }

            for(int i=0; i<DisPlayTexts.Length; i++)
            {
                DisPlayTexts[i].text = englishDisplayText[i];
            }

            for(int i=0; i<AudioTexts.Length; i++)
            {
                AudioTexts[i].text = englishAudioText[i];
            }

            for(int i=0; i<GeneralTexts.Length; i++)
            {
                GeneralTexts[i].text = englishGeneralText[i];
            }
            
        }else if(language == "K")
        {
            for(int i=0; i<buttonPanelTexts.Length; i++)
            {
                buttonPanelTexts[i].text = koreanButtonText[i];
            }

            for(int i=0; i<defaultButtonPanelTexts.Length; i++)
            {
                defaultButtonPanelTexts[i].text = defaultButtonKoreanText;
            }

            int j = 0;
            ControlTexts[j].text = koreanControlText;
            j++;

            for(int i=0; i< koreanControlText1.Length; i++)
            {
                ControlTexts[j].text = koreanControlText1[i];
                j++;
            }

            for(int i=0; i< koreanControlText2.Length; i++)
            {
                ControlTexts[j].text = koreanControlText2[i];
                j++;
            }

            for(int i=0; i<DisPlayTexts.Length; i++)
            {
                DisPlayTexts[i].text = koreanDisplayText[i];
            }

            for(int i=0; i<AudioTexts.Length; i++)
            {
                AudioTexts[i].text = koreanAudioText[i];
            }

            for(int i=0; i<GeneralTexts.Length; i++)
            {
                GeneralTexts[i].text = koreanGeneralText[i];
            }
        }
    }


    void VideoValueChanged(int Resolution, Toggle toggle)
    {
        switch(Resolution)
        {
            case 1080 :
            if(!toggle.isOn) return;
                Toggle1080.isOn = true;
                Toggle900.isOn = false;
                Toggle720.isOn = false;
                Toggle540.isOn = false;
                Toggle1080.interactable = false;
                Toggle900.interactable = true;
                Toggle720.interactable = true;
                Toggle540.interactable = true;
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 1080);
                break;
            case 900 :
            if(!toggle.isOn) return;
                Toggle1080.isOn = false;
                Toggle900.isOn = true;
                Toggle720.isOn = false;
                Toggle540.isOn = false;
                Toggle1080.interactable = true;
                Toggle900.interactable = false;
                Toggle720.interactable = true;
                Toggle540.interactable = true;
                Screen.SetResolution(1600, 900, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 900);
                break;
            case 720 :
            if(!toggle.isOn) return;
                Toggle1080.isOn = false;
                Toggle900.isOn = false;
                Toggle720.isOn = true;
                Toggle540.isOn = false;
                Toggle1080.interactable = true;
                Toggle900.interactable = true;
                Toggle720.interactable = false;
                Toggle540.interactable = true;
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 720);
                break;
            case 540 :
            if(!toggle.isOn) return;
                Toggle1080.isOn = false;
                Toggle900.isOn = false;
                Toggle720.isOn = false;
                Toggle540.isOn = true;
                Toggle1080.interactable = true;
                Toggle900.interactable = true;
                Toggle720.interactable = true;
                Toggle540.interactable = false;
                Screen.SetResolution(960, 540, FullScreenMode.Windowed);
                PlayerPrefs.SetInt("Resolution", 540);
                break;
        }
        
    }

    void LanguageValueChanged(string language, Toggle toggle)
    {
        switch(language)
        {
            case "E" :
            if(!toggle.isOn) return;

                koreanToggle.isOn = false;
                koreanToggle.interactable = true;
                enlgishToggle.interactable = false;
                
                GameAudioManager.LanguageManager.Invoke_languageChangeEvent("E");
                break;
            case "K" :
            if(!toggle.isOn) return;

                enlgishToggle.isOn = false;
                enlgishToggle.interactable = true;
                koreanToggle.interactable = false;
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

        Toggle1080.isOn = true;
        Toggle900.isOn = false;
        Toggle720.isOn = false;
        Toggle540.isOn = false;
        Toggle1080.interactable = false;
        Toggle900.interactable = true;
        Toggle720.interactable = true;
        Toggle540.interactable = true;
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
        PlayerPrefs.SetInt("Resolution", 1080);
    }

    private void ResetGenralOption()
    {
        GameAudioManager.LanguageManager.Invoke_languageChangeEvent("E");
        enlgishToggle.isOn = true;
        enlgishToggle.interactable = false;
        koreanToggle.isOn = false;
        koreanToggle.interactable = true;
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
