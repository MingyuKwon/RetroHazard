using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNameCollection 
{
    public enum sceneName{
        Tutorial = 0,
    }

    public static sceneName currentSceneName;
    public static int currentSceneIndex;

    public static string[] getMapNameArray(int num)
    {
        switch(num)
        {
            case 0:
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {   
                    return tutorialMapNameEnglish;
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {   
                    return tutorialMapNameKorean;
                }
                break;
                
        }

        Debug.Log("getMapNameArray NULL");
        return null;
    }

    public static string[] getMapNameArrayEnglish(int num)
    {
        switch(num)
        {
            case 0:
                return tutorialMapNameEnglish;
        }

        Debug.Log("getMapNameArray NULL");
        return null;
    }

    public static string[] tutorialMapNameEnglish = {
        "Adeenhau beach",
        "Lasil archaeological site",
        "archaeologists' accommodations",
        "excavation leader's accommodations",
    };

    public static string[] tutorialMapNameKorean = {
        "아딘하우 해변",
        "라실 고대 유적지",
        "발굴자들의 숙소",
        "발굴대장의 숙소",
    };
}
