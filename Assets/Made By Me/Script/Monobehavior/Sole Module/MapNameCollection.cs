using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNameCollection 
{
    public enum sceneName{
        Tutorial = 0,
    }

    public static string[] getMapNameArray(int num)
    {
        switch(num)
        {
            case 0:
                return tutorialMapName;
        }

        Debug.Log("getMapNameArray NULL");
        return null;
    }

    public static string[] tutorialMapName = {
        "Adeenhau beach",
        "Lasil archaeological site",
        "archaeologists' accommodations",
        "excavation leader's accommodations",
    };
}
