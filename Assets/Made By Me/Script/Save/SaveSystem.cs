using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private class StageArrayWrapper
    {
        public StageSave[] array;
    }

    public static SaveSystem instance;
    public static string stageSavePath;

    private void Awake() {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }
    }

    public StageSave[] ActiveStageSaves;
 
    private void Start() {
        stageSavePath = Path.Combine(Application.persistentDataPath, "StageSaveData.json");
        if(File.Exists(stageSavePath))
        {
            Load();
        }else
        {
           ActiveStageSaves = new StageSave[SceneManager.sceneCountInBuildSettings];
           Save();
        }
    }

    public void Save()
    {
        //StageSave
        StageArrayWrapper wrapper = new StageArrayWrapper();
        wrapper.array = ActiveStageSaves;
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(stageSavePath, json);
        //StageSave
    }

    public void Load()
    {
        //StageSave
        if(File.Exists(stageSavePath))
        {
            string json = File.ReadAllText(stageSavePath);
            StageArrayWrapper wrapper = new StageArrayWrapper();
            JsonUtility.FromJsonOverwrite(json , wrapper);
            ActiveStageSaves = wrapper.array;
        }else
        {
            Debug.LogError(stageSavePath + " doesnt have any Stage save json File");
        }
        //StageSave
    }

    [Button]
    public void ClearSave()
    {
        File.Delete(stageSavePath);
    }

    private void OnDestroy() {
        if(File.Exists(stageSavePath))
        {
            Save();
        }else
        {

        }
        
    }
}
