using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNameCollection 
{
    public enum sceneName{
        Tutorial = 0,
        YangHan_Village = 1,
        Noshuc_Royal_Tomb = 2,
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

            case 1:
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {   
                    return startTownMapNameEnglish;
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {   
                    return startTownMapNameKorean;
                }
                break;

            case 2:
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {   
                    return Noshuc_Royal_TombEnglish;
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {   
                    return Noshuc_Royal_TombKoream;
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
            case 1:
                return startTownMapNameEnglish;
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

    public static string[] startTownMapNameEnglish = {
        "YangHan Village",
        "Ben's House", 
        "Martin's House", 
        "Thomas's House", 
        "William's House", 
        "Tourist Information Center",
    };

    public static string[] startTownMapNameKorean = {
        "양한 마을",
        "벤의 집",
        "마르틴의 집",
        "토마스의 집",
        "윌리엄의 집",
        "관광 가이드 센터",
    };

    public static string[] Noshuc_Royal_TombEnglish = {
        "Noshuc Royal Tomb Entrance",
    };

    public static string[] Noshuc_Royal_TombKoream = {
        "노슈크 왕릉 입구",
    };
}
