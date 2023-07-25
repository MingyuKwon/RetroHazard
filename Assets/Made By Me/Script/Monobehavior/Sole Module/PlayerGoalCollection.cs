using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerGoalCollection : MonoBehaviour
{
    public static event Action goalChangeEvent;

    private static int private_currentGoalIndex = 0;
    public static int currentGoalIndex
    {
        get
        {
            return private_currentGoalIndex;
        }

        set
        {
            ChangeGoalIndex(value);
        }
    }

    public static void ChangeGoalIndex(int n)
    {
        if(n >= PlayerGoals.Length)
        {
            Debug.LogError(n + "is bigger than array index limit " + PlayerGoals.Length);
            return;
        }

        private_currentGoalIndex = n;
        goalChangeEvent?.Invoke();
    }

    public static string[] PlayerGoals {
        get{
            if(GameAudioManager.LanguageManager.currentLanguage == "E")
            {
                return PlayerGoalsEnglish;
            }else
            {
                return PlayerGoalsKorean;
            }
        }
    }

    static string[] PlayerGoalsEnglish = {
        "Entering the Okera region",
        "Find the key to unlock the locked gate",
        "Get out of the archaeological site",
        "Find something that can remove the pile of rocks.",
        "Entering the Open Passage by Removing the Stones",
    };

    static string[] PlayerGoalsKorean = {
        "Okera 지방에 들어가기",
        "잠긴 대문을 열기 위한 키를 찾기",
        "유적지로 부터 나가서 Okera지방에 들어가기",
        "돌 무더기를 치울 수 있는 것을 찾아보기.",
        "돌을 치우고 생긴 길을 통해 Okera 지방에 들어가기",
    };

}
