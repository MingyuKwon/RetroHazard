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

    public static event Action SaveEvent;
    public static event Action LoadEvent;

    public static SaveSystem instance;

    public static string saveSlotInfoPath; // 0
    public static string stageSavePath; // 1
    public static string StatusSavePath; // 2
    public static string InventorySavePath; // 3
    public static string ItemBoxSavePath; // 4


    const int SaveSlotAmount = 10;
    public static string[] SaveDirectorys;
    public SaveSlotInfo[] saveSlotInfos;
    public static int SaveSlotNum = 0; // modify right before player want to save in specific sloat

    PlayerStatus status;
    PlayerInventory inventory;
    PlayerItemBox itemBox;
    public StageSave[] ActiveStageSaves;
    

    private void Awake() {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        status = FindObjectOfType<PlayerStatus>();
        inventory = FindObjectOfType<PlayerInventory>();
        itemBox = FindObjectOfType<PlayerItemBox>();

        SaveDirectorys = new string[SaveSlotAmount];
        saveSlotInfos = new SaveSlotInfo[SaveSlotAmount];

        for(int i=0; i<SaveSlotAmount; i++)
        {
            SaveDirectorys[i] = Path.Combine(Application.persistentDataPath, "SaveSlot " + (i+1));

            if(!Directory.Exists(SaveDirectorys[i]))
            {
                Directory.CreateDirectory(SaveDirectorys[i]);
            }

            saveSlotInfos[i] = new SaveSlotInfo();

            if(File.Exists(Path.Combine(SaveDirectorys[i], "SaveSlotInfo.json")))
            {
                string json = File.ReadAllText(Path.Combine(SaveDirectorys[i], "SaveSlotInfo.json"));
                JsonUtility.FromJsonOverwrite(json , saveSlotInfos[i]);
            }else
            {
                saveSlotInfos[i].saveTime = "Empty";
            }
        }
    }

    private void SetPath()
    {
        string LoadSlotPath = SaveDirectorys[SaveSlotNum];

        saveSlotInfoPath = Path.Combine(LoadSlotPath, "SaveSlotInfo.json");
        stageSavePath = Path.Combine(LoadSlotPath, "StageSaveData.json");
        StatusSavePath = Path.Combine(LoadSlotPath, "StatusSaveData.json");
        InventorySavePath = Path.Combine(LoadSlotPath, "InventorySaveData.json");
        ItemBoxSavePath = Path.Combine(LoadSlotPath, "ItemBoxSaveData.json");
    }
 
    private void Start() {

        SetPath();
        
        if(File.Exists(stageSavePath))
        {
            Load(1);
        }else
        {
           ActiveStageSaves = new StageSave[SceneManager.sceneCountInBuildSettings];
           for(int i=0; i<ActiveStageSaves.Length; i++)
           {
                ActiveStageSaves[i] = new StageSave();
           }
        }

        if(File.Exists(StatusSavePath))
        {
            Load(2);
        }

        if(File.Exists(InventorySavePath))
        {
            Load(3);
        }

        if(File.Exists(ItemBoxSavePath))
        {
            Load(4);
        }
    }

    [Button]
    public void Save(int flag)
    {
        SetPath();

        SaveSlotInfo saveInfo = new SaveSlotInfo();
        saveInfo.saveTime = System.DateTime.Now.ToString("yyyy-MM-dd \nHH:mm:ss");
        saveInfo.saveLocation = SceneManager.GetActiveScene().name;

        saveSlotInfos[SaveSlotNum] = saveInfo;

        string saveInfoJson = JsonUtility.ToJson(saveInfo, true);
        File.WriteAllText(saveSlotInfoPath, saveInfoJson);

        if(flag == 1 || flag == 0)
        {
        //StageSave
            StageArrayWrapper wrapper = new StageArrayWrapper();
            wrapper.array = ActiveStageSaves;
            string json = JsonUtility.ToJson(wrapper, true);
            File.WriteAllText(stageSavePath, json);
        //StageSave
        }

        if(flag == 2 || flag == 0)
        {
        //StatusSave
            PlayerStatSave save = new PlayerStatSave(status);
            string json = JsonUtility.ToJson(save, true);
            File.WriteAllText(StatusSavePath, json);
        //StatusSave
        }

        if(flag == 3 || flag == 0)
        {
        //InventorySave
            PlayerInventorySave save = new PlayerInventorySave(inventory);
            string json = JsonUtility.ToJson(save, true);
            File.WriteAllText(InventorySavePath, json);
        //InventorySave
        }

        if(flag == 4 || flag == 0)
        {
        //ItemBoxSave
            PlayerBoxItemSave save = new PlayerBoxItemSave(itemBox);
            string json = JsonUtility.ToJson(save, true);
            File.WriteAllText(ItemBoxSavePath, json);
        //ItemBoxSave
        }

        SaveEvent.Invoke();
        
    }

    public void Load(int flag)
    {
        SetPath();

        if(flag == 1 || flag == 0)
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

        if(flag == 2 || flag == 0)
        {
            //StatusSave
            if(File.Exists(StatusSavePath))
            {
                string json = File.ReadAllText(StatusSavePath);
                PlayerStatSave save = new PlayerStatSave();
                JsonUtility.FromJsonOverwrite(json , save);
                status.LoadSave(save);
            }else
            {
                Debug.LogError(StatusSavePath + " doesnt have any Status Save json File");
            }
            //StatusSave
        }

        if(flag == 3 || flag == 0)
        {
        //InventorySave
            if(File.Exists(InventorySavePath)) 
            {
                string json = File.ReadAllText(InventorySavePath);
                PlayerInventorySave save = new PlayerInventorySave();
                JsonUtility.FromJsonOverwrite(json , save);
                inventory.LoadSave(save);
            }else
            {
                Debug.LogError(InventorySavePath + " doesnt have any Inventory Save json File");
            }
            
        //InventorySave
        }

        if(flag == 4 || flag == 0)
        {
        //ItemBoxSave
            if(File.Exists(ItemBoxSavePath)) 
            {
                string json = File.ReadAllText(ItemBoxSavePath);
                PlayerBoxItemSave save = new PlayerBoxItemSave();
                JsonUtility.FromJsonOverwrite(json , save);
                itemBox.LoadSave(save);
            }else
            {
                Debug.LogError(ItemBoxSavePath + " doesnt have any Item Box Save json File");
            }
            
        //ItemBoxSave
        }

        if(flag == 0)
        {
            GameManagerUI.instance.BlackOut(1.5f); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            LoadEvent.Invoke();
        }

        
        
    }

    [Button]
    public void ClearSave(int flag)
    {
        if(flag == 1 || flag == 0)
        {
            File.Delete(stageSavePath);
        }

        if(flag == 2 || flag == 0)
        {
            File.Delete(StatusSavePath);
        }

        if(flag == 3 || flag == 0)
        {
            File.Delete(InventorySavePath);
        }

        if(flag == 4 || flag == 0)
        {
            File.Delete(ItemBoxSavePath);
        }
        
        
    }

}
