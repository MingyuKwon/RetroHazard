using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour, CallBackInterface
{
    public bool isSave = false;
    Button[] SaveSlots;
    Text[] SaveSlotsTexts;

    int saveSlotNum;

    private void Awake() {
        SaveSlots = new Button[transform.childCount -3];
        for(int i=0; i<transform.childCount -3; i++)
        {
            SaveSlots[i] = transform.GetChild(i+3).GetComponent<Button>();
        }

        SaveSlotsTexts = new Text[SaveSlots.Length];
        for(int i=0; i<SaveSlotsTexts.Length; i++)
        {
            SaveSlotsTexts[i] = SaveSlots[i].transform.GetChild(0).GetComponentInChildren<Text>();
            SaveSlotsTexts[i].text = "Save Slot " + (i + 1);

            SaveSlots[i].transform.GetChild(2).gameObject.SetActive(false); // Where
            SaveSlots[i].transform.GetChild(3).gameObject.SetActive(false); // Current Goal
            SaveSlots[i].transform.GetChild(4).gameObject.SetActive(false); // Date
            SaveSlots[i].transform.GetChild(5).gameObject.SetActive(false);
        }

        for(int i=0; i<SaveSlotsTexts.Length; i++)
        {
            if(SaveSystem.instance.saveSlotInfos[i].saveTime != "Empty")
            {
                SaveSlots[i].transform.GetChild(2).gameObject.SetActive(true);
                SaveSlots[i].transform.GetChild(3).gameObject.SetActive(true);
                SaveSlots[i].transform.GetChild(4).gameObject.SetActive(true);
                SaveSlots[i].transform.GetChild(5).gameObject.SetActive(true);

                SaveSlots[i].transform.GetChild(4).GetComponent<Text>().text = SaveSystem.instance.saveSlotInfos[i].saveTime;
                SaveSlots[i].transform.GetChild(2).GetComponent<Text>().text = SaveSystem.instance.saveSlotInfos[i].saveScene;
                SaveSlots[i].transform.GetChild(3).GetComponent<Text>().text = PlayerGoalCollection.PlayerGoals[SaveSystem.instance.saveSlotInfos[i].saveCurrentGoalNum];
            }
        }
    }

    private void OnEnable() {
        SaveSystem.SaveEvent += UpdateSaveSlot;

        for(int i=0; i<SaveSlots.Length; i++)
        {
            int temp = i;
            SaveSlots[i].onClick.AddListener(() => SlotClick(temp));
            SaveSlots[i].transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() => Delete(temp));
        }

        if(isSave)
        {
            transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Save";
        }else
        {
            transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Load";
        }

        
    }

    private void OnDisable() {
        SaveSystem.SaveEvent -= UpdateSaveSlot;

        for(int i=0; i<SaveSlots.Length; i++)
        {
            SaveSlots[i].onClick.RemoveAllListeners();
            SaveSlots[i].transform.GetChild(5).GetComponent<Button>().onClick.RemoveAllListeners();
        }

        isSave = false;
    }

    public void CallBack()
    {
        if(isSave)
        {
            OnSave(saveSlotNum);
        }else
        {
            AlertUI.instance.previousInputRule = 0;
            OnLoad(saveSlotNum);
        }
    }

    public void SlotClick(int n)
    {
        saveSlotNum = n;

        if(isSave)
        {
            AlertUI.instance.ShowAlert("Are you sure you want to save in save slot " + (saveSlotNum + 1) + " ?\n\n<i>(save file will be overwrited if save slot already has one)</i>", this);
        }else
        {
            if(SaveSystem.instance.saveSlotInfos[saveSlotNum].saveTime == "Empty")
            {
                return;
            }

            AlertUI.instance.ShowAlert("Are you sure you want to load from save slot " + (saveSlotNum + 1) + " ?", this);
        }
        
    }

    private class DeleteCallbackClass : CallBackInterface
    {
        int deleteSlotNum;

        public DeleteCallbackClass(int deleteSlotNum)
        {
            this.deleteSlotNum = deleteSlotNum;
        }

        public void CallBack()
        {
            OnClear(deleteSlotNum);
        }

        private void OnClear(int num)
        {
            SaveSystem.SaveSlotNum = num;
            SaveSystem.instance.ClearSave(0);
        }
    }

    public void Delete(int n)
    {
        AlertUI.instance.ShowAlert("Are you sure you want to Delete this save slot " + (saveSlotNum + 1) + " ?", new DeleteCallbackClass(n));
    }

    private void OnSave(int num)
    {
        SaveSystem.SaveSlotNum = num;
        SaveSystem.instance.Save(0);
    }

    

    private void OnLoad(int num)
    {
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
                SaveSlots[i].transform.GetChild(5).gameObject.SetActive(true);

                SaveSlots[i].transform.GetChild(4).GetComponent<Text>().text = SaveSystem.instance.saveSlotInfos[i].saveTime;
                SaveSlots[i].transform.GetChild(2).GetComponent<Text>().text = SaveSystem.instance.saveSlotInfos[i].saveScene;
                SaveSlots[i].transform.GetChild(3).GetComponent<Text>().text = PlayerGoalCollection.PlayerGoals[SaveSystem.instance.saveSlotInfos[i].saveCurrentGoalNum];
            }else
            {
                SaveSlots[i].transform.GetChild(2).gameObject.SetActive(false);
                SaveSlots[i].transform.GetChild(3).gameObject.SetActive(false);
                SaveSlots[i].transform.GetChild(4).gameObject.SetActive(false);
                SaveSlots[i].transform.GetChild(5).gameObject.SetActive(false);
            }
        }
    }
        
}
