using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveSlotInfo
{
    public string saveTime = "Empty";
    public MapNameCollection.sceneName saveSceneName;
    public int saveSceneIndex = 0;
    public Vector3 saveLocation = new Vector3(0,0,0);
    public int saveCurrentGoalNum = 0;

    public bool isItemUITutorialDone = false;
    public bool isMiniMapTutorialDone = false;
    public bool isBoxUITutorialDone = false;
    public bool isSaveSlotUITutorialDone = false;

}
