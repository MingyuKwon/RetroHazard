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

    public static string stageSavePath; // 1
    public static string StatusSavePath; // 2
    public static string InventorySavePath; // 3
    public static string ItemBoxSavePath; // 4

    PlayerStatus status;
    PlayerInventory inventory;
    PlayerItemBox itemBox;

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

        
    }

    public StageSave[] ActiveStageSaves;
 
    private void Start() {
        stageSavePath = Path.Combine(Application.persistentDataPath, "StageSaveData.json");
        StatusSavePath = Path.Combine(Application.persistentDataPath, "StatusSaveData.json");
        InventorySavePath = Path.Combine(Application.persistentDataPath, "InventorySaveData.json");
        ItemBoxSavePath = Path.Combine(Application.persistentDataPath, "ItemBoxSaveData.json");
        
        if(File.Exists(stageSavePath))
        {
            Load(1);
        }else
        {
           ActiveStageSaves = new StageSave[SceneManager.sceneCountInBuildSettings];
           Save(1);
        }

        if(File.Exists(StatusSavePath))
        {
            Load(2);
        }else
        {
           Save(2);
        }

        if(File.Exists(InventorySavePath))
        {
            Load(3);
        }else
        {
           Save(3);
        }

        if(File.Exists(ItemBoxSavePath))
        {
            Load(4);
        }else
        {
           Save(4);
        }
    }

    [Button]
    public void Save(int flag)
    {
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
        
    }

    public void Load(int flag)
    {

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
