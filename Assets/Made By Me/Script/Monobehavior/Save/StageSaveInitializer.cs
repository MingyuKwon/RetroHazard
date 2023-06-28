using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSaveInitializer : MonoBehaviour
{
    private void Awake() {
        if(!SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].isVisited) // first initialize
        {
            Debug.Log("Initialize");
            StageSave temp = new StageSave();

            temp.isVisited = true;

            int n = FindObjectsOfType<bulletItem>().Length;
            temp.is_BulletItem_Destroy = new bool[n];

            n = FindObjectsOfType<PotionItem>().Length;
            temp.is_PotionItem_Destroy = new bool[n];

            n = FindObjectsOfType<InteractKeyItem>().Length;
            temp.is_KeyItem_Destroy = new bool[n];

            n = FindObjectsOfType<ExpansionItem>().Length;
            Debug.Log("ExpansionItem length " + n );
            temp.is_ExpansionItem_Destroy = new bool[n];

            n = FindObjectsOfType<EquipItem>().Length;
            temp.is_EquipItem_Destroy = new bool[n];

            n = FindObjectsOfType<RealInteract>().Length;
            temp.is_Interact_Destroy = new bool[n];

            SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex] = temp;
        }
    }
}
