using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerItemBox : MonoBehaviour
{
    PlayerItemBoxLogic playerItemBoxLogic;
    public ItemInformation[] items{
        get{
            return playerItemBoxLogic.items;
        }
        set{
            playerItemBoxLogic.items = value;
        }
    }
    public int[] itemsamount{
        get{
            return playerItemBoxLogic.itemsamount;
        }
        set{
            playerItemBoxLogic.itemsamount = value;
        }
    }
    public bool isBoxfull{
        get{
            return playerItemBoxLogic.CheckItemBoxFull();
        }
        set{
            playerItemBoxLogic.isBoxfull = value;
        }
    }

    private void Awake() {        
        playerItemBoxLogic = new PlayerItemBoxLogic();
    }

    private void OnEnable() {
        playerItemBoxLogic.OnEnable();
    }

    private void OnDisable() {
        playerItemBoxLogic.OnDisable();
    }

    public void LoadSave(PlayerBoxItemSave save)
    {
        playerItemBoxLogic.LoadSave(save);
    }

    


}
