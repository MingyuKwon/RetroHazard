using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StageSave 
{
    public bool isVisited = false;
    public bool[] is_BulletItem_Destroy;
    public bool[] is_PotionItem_Destroy;
    public bool[] is_KeyItem_Destroy;

    public bool[] is_ExpansionItem_Destroy;
    public bool[] is_EquipItem_Destroy;

    public bool[] is_Interact_Destroy;
    public bool[] is_Notice_Destroy;

    public StageSave()
    {
       
    }

    public StageSave(StageSave stageSave)
    {
        is_BulletItem_Destroy = new bool[stageSave.is_BulletItem_Destroy.Length];
        is_PotionItem_Destroy = new bool[stageSave.is_PotionItem_Destroy.Length];
        is_KeyItem_Destroy = new bool[stageSave.is_KeyItem_Destroy.Length];
        is_ExpansionItem_Destroy = new bool[stageSave.is_ExpansionItem_Destroy.Length];
        is_EquipItem_Destroy = new bool[stageSave.is_EquipItem_Destroy.Length];
        is_Interact_Destroy = new bool[stageSave.is_Interact_Destroy.Length];
        is_Notice_Destroy = new bool[stageSave.is_Notice_Destroy.Length];

        for(int i=0; i<stageSave.is_BulletItem_Destroy.Length; i++)
        {
            is_BulletItem_Destroy[i] = stageSave.is_BulletItem_Destroy[i];
        }

        for(int i=0; i<stageSave.is_PotionItem_Destroy.Length; i++)
        {
            is_PotionItem_Destroy[i] = stageSave.is_PotionItem_Destroy[i];
        }

        for(int i=0; i<stageSave.is_KeyItem_Destroy.Length; i++)
        {
            is_KeyItem_Destroy[i] = stageSave.is_KeyItem_Destroy[i];
        }

        for(int i=0; i<stageSave.is_ExpansionItem_Destroy.Length; i++)
        {
            is_ExpansionItem_Destroy[i] = stageSave.is_ExpansionItem_Destroy[i];
        }

        for(int i=0; i<stageSave.is_EquipItem_Destroy.Length; i++)
        {
            is_EquipItem_Destroy[i] = stageSave.is_EquipItem_Destroy[i];
        }

        for(int i=0; i<stageSave.is_Interact_Destroy.Length; i++)
        {
            is_Interact_Destroy[i] = stageSave.is_Interact_Destroy[i];
        }

        for(int i=0; i<stageSave.is_Notice_Destroy.Length; i++)
        {
            is_Notice_Destroy[i] = stageSave.is_Notice_Destroy[i];
        }

        
    }

    

}
