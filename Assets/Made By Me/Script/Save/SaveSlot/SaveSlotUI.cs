using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public bool isSave;
    Button[] SaveSlots;
    Text[] SaveSlotsTexts;

    private void Awake() {
        SaveSlots = GetComponentsInChildren<Button>();

        SaveSlotsTexts = new Text[SaveSlots.Length];
        for(int i=0; i<SaveSlotsTexts.Length; i++)
        {
            SaveSlotsTexts[i] = SaveSlots[i].transform.GetChild(0).GetComponentInChildren<Text>();
            SaveSlotsTexts[i].text = "Save Slot " + (i + 1);

            SaveSlots[i].transform.GetChild(2).gameObject.SetActive(false); // Where
            SaveSlots[i].transform.GetChild(3).gameObject.SetActive(false); // Current Goal
            SaveSlots[i].transform.GetChild(4).gameObject.SetActive(false); // Date
        }

        for(int i=0; i<SaveSlotsTexts.Length; i++)
        {
            if(SaveSystem.instance.saveSlotInfos[i].saveTime != "Empty")
            {
                SaveSlots[i].transform.GetChild(2).gameObject.SetActive(true);
                SaveSlots[i].transform.GetChild(3).gameObject.SetActive(true);
                SaveSlots[i].transform.GetChild(4).gameObject.SetActive(true);

                SaveSlots[i].transform.GetChild(4).GetComponent<Text>().text = SaveSystem.instance.saveSlotInfos[i].saveTime;
                SaveSlots[i].transform.GetChild(2).GetComponent<Text>().text = SaveSystem.instance.saveSlotInfos[i].saveLocation;
                SaveSlots[i].transform.GetChild(3).GetComponent<Text>().text = SaveSystem.instance.saveSlotInfos[i].saveCurrentGoal;
            }
        }
    }

    private void OnEnable() {
        SaveSystem.SaveEvent += UpdateSaveSlot;
        
        if(isSave)
        {
            for(int i=0; i<SaveSlots.Length; i++)
            {
                int temp = i;
                SaveSlots[i].onClick.AddListener(() => OnSave(temp));
            }
        }else
        {
            for(int i=0; i<SaveSlots.Length; i++)
            {
                int temp = i;
                SaveSlots[i].onClick.AddListener(() => OnLoad(temp));
            }
        }
    }
    private void OnDisable() {
        SaveSystem.SaveEvent -= UpdateSaveSlot;

        for(int i=0; i<SaveSlots.Length; i++)
        {
            SaveSlots[i].onClick.RemoveAllListeners();
        }
        
    }

    private void OnSave(int num)
    {
        SaveSystem.SaveSlotNum = num;
        SaveSystem.instance.Save(0);
    }

    private void OnLoad(int num)
    {
        if(SaveSystem.instance.saveSlotInfos[num].saveTime == "Empty")
        {
            return;
        }

        SaveSystem.SaveSlotNum = num;
        SaveSystem.instance.Load(0);

        transform.parent.gameObject.SetActive(false);
    }

    private void UpdateSaveSlot()
    {
        for(int i=0; i<SaveSlotsTexts.Length; i++)
        {
            if(SaveSystem.instance.saveSlotInfos[i].saveTime != "Empty")
            {
                SaveSlots[i].transform.GetChild(2).gameObject.SetActive(true);
                SaveSlots[i].transform.GetChild(3).gameObject.SetActive(true);
                SaveSlots[i].transform.GetChild(4).gameObject.SetActive(true);

                SaveSlots[i].transform.GetChild(4).GetComponent<Text>().text = SaveSystem.instance.saveSlotInfos[i].saveTime;
                SaveSlots[i].transform.GetChild(2).GetComponent<Text>().text = SaveSystem.instance.saveSlotInfos[i].saveLocation;
                SaveSlots[i].transform.GetChild(3).GetComponent<Text>().text = SaveSystem.instance.saveSlotInfos[i].saveCurrentGoal;
            }
        }
    }
        
}
