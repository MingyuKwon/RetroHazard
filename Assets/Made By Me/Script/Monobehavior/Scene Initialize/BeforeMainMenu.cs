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
        SceneManager.LoadScene("Main Menu");
    }
}
