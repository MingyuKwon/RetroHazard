using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    SaveSlotUILogic saveSlotUILogic;

    public int saveSlotIndex{
        get{
            return saveSlotUILogic.saveSlotIndex;
        }
        set{
            saveSlotUILogic.saveSlotIndex = value;
        }
    }

    public bool isSave{
        get{
            return saveSlotUILogic.isSave;
        }

        set{
            saveSlotUILogic.isSave = value;
        }
    }

    private void Awake() { 
        saveSlotUILogic = new SaveSlotUILogic(this);
    }

    private void OnEnable() {
        saveSlotUILogic.OnEnable();
    }

    private void OnDisable() {
        saveSlotUILogic.OnDisable();
    }

}
