using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeMainMenu : MonoBehaviour
{
    void Start()
    {
        if(PlayerPrefs.HasKey("Resolution"))
        {
            switch(PlayerPrefs.GetInt("Resolution"))
            {
            case 1080 :
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                break;
            case 900 :
                Screen.SetResolution(1600, 900, FullScreenMode.Windowed);
                break;
            case 720 :
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                break;
            case 540 :
                Screen.SetResolution(960, 540, FullScreenMode.Windowed);
                break;
        }
        }else
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
            PlayerPrefs.SetInt("Resolution", 1080);
        }

        if(PlayerPrefs.HasKey("language"))
        {
            switch(PlayerPrefs.GetString("language"))
            {
            case "E" :
                GameAudioManager.LanguageManager.Invoke_languageChangeEvent("E");
                break;
            case "K" :
                GameAudioManager.LanguageManager.Invoke_languageChangeEvent("K");
                break;
            }
        }else
        {
            GameAudioManager.LanguageManager.Invoke_languageChangeEvent("E");
        }


        SceneManager.LoadSceneAsync("Main Menu");
    }
}
