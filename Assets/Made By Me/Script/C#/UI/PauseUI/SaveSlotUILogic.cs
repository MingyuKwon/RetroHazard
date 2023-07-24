using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUILogic : CallBackInterface
{
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

    SaveSlotUI monobehaviour;

    public int saveSlotIndex {
        get{
            return _saveSlotIndex;
        }

        set{
            _saveSlotIndex = value;
            for(int i=0; i<SaveSlots.Length; i++)
            {
                SaveSlots[i].transform.GetChild(6).gameObject.SetActive(false);
            }

            if(_saveSlotIndex < 0)
            {

            }else
            {
                SaveSlots[_saveSlotIndex].transform.GetChild(6).gameObject.SetActive(true);
                GameAudioManager.instance.PlayUIMusic(UIAudioType.Move);
            }
        }
    }

    int _saveSlotIndex = 0;

    public bool isDelete = false;



    static public bool isSave = false;
    Button[] SaveSlots;
    Text[] SaveSlotsTexts;

    int saveSlotNum;

    string[] FirstSaveSlotUINoticeTextEnglish = {
                "The way to save is simple",
                "Just press the slot you want to save in!",
                "If you want to delete, you can press the remove button or click the red X button with the mouse",
                };
    
    string[] FirstSaveSlotUINoticeTextKorean = {
                "저장하는 법은 간단해",
                "원하는 슬롯을 눌러서 저장하면 돼",
                "만약 저장 슬롯을 삭제하고 싶다면 슬롯으로 커서를 옮기고 삭제버튼을 누르거나, 위의 X를 누르면 돼",
                };

    string[] SaveSlotUIText = {
            "<i><b>-Input-</b></i>\n\n<b>ENTER</b> : \nspace\n\n<b>BACK</b> : \nbackSpace\n\n<b>REMOVE</b> :\n R",
            "<i><b>-입력-</b></i>\n\n<b>확인</b> : \nspace\n\n<b>뒤로가기</b> : \nbackSpace\n\n<b>삭제하기</b> :\n R",
                };

    public SaveSlotUILogic(SaveSlotUI monobehaviour)
    {
        this.monobehaviour = monobehaviour;
        Awake();
    }

    private void Awake() {
        SaveSlots = new Button[monobehaviour.transform.childCount -3];
        for(int i=0; i<monobehaviour.transform.childCount -3; i++)
        {
            SaveSlots[i] = monobehaviour.transform.GetChild(i+3).GetComponent<Button>();
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

    public void OnEnable() {
        SaveSystem.SaveEvent += UpdateSaveSlot;

        if(isSave)
        {
            monobehaviour.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Save";

            if(!GameManager.TutorialCheck.isSaveSlotUITutorialDone)
            {
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {   
                    GameManager.EventManager.InvokeShowNotice("SaveSlotUI", FirstSaveSlotUINoticeTextEnglish , true ,500 ,300);
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {
                    GameManager.EventManager.InvokeShowNotice("SaveSlotUI", FirstSaveSlotUINoticeTextKorean , true ,500 ,300);
                }
                
                GameManager.TutorialCheck.isSaveSlotUITutorialDone = true;
            }else
            {
                GameManager.EventManager.InvokeShowNotice("SaveSlotUI", SaveSlotUIText , false,200 ,350);
            }
        }else
        {
            monobehaviour.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Load";
            GameManager.EventManager.InvokeShowNotice("SaveSlotUI", SaveSlotUIText , false,200 ,350);
        }
    }

    public void OnDisable() {
        SaveSystem.SaveEvent -= UpdateSaveSlot;

        for(int i=0; i<SaveSlots.Length; i++)
        {
            SaveSlots[i].onClick.RemoveAllListeners();
            SaveSlots[i].transform.GetChild(5).GetComponent<Button>().onClick.RemoveAllListeners();
        }

        isSave = false;

        GameManager.EventManager.InvokeShowNotice("SaveSlotUI");

    }

    public void CallBack()
    {
        if(isSave)
        {
            OnSave(saveSlotNum);
        }else
        {
            OnLoad(saveSlotNum);
        }
    }

    public void SlotClick(int n)
    {
        saveSlotNum = n;
        GameAudioManager.instance.PlayUIMusic(UIAudioType.Click);
        if(isDelete)
        {
            Delete(n);
            return;
        }

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

    public void Delete(int n)
    {
        saveSlotNum = n;

        if(SaveSystem.instance.saveSlotInfos[saveSlotNum].saveTime == "Empty")
        {
            return;
        }

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

        monobehaviour.transform.parent.gameObject.SetActive(false);
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
