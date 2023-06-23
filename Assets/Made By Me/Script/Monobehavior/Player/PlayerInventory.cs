using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// ================ code cleaned by making PlayerInventoryLogic ==============================

public class PlayerInventory : MonoBehaviour
{
    public ItemInformation[] items; // items는 그대로 두고, logic에 이 객체 자체를 넘겨 줌으로 
                                    // logic에서 item을 변경 해도 곧바로 inventroy 상황에 반영 되도록 한다
    public int[] itemsamount;
    public bool[] isEquipped;

    public bool isInventoryFull{
        get {
            return playerInventroyLogic.isInventoryFull;
        }
        set {
            playerInventroyLogic.isInventoryFull = value;
        }
    }

    private PlayerInventroyLogic playerInventroyLogic;

    public int CurrentContainerSize {
        get {
            return playerInventroyLogic.CurrentContainerSize;
        }
        set {
            playerInventroyLogic.CurrentContainerSize = value;
        }
    }
    
    public int SheildBatteryLimit {
        get {
            return playerInventroyLogic.SheildBatteryLimit;
        }
        set {
            playerInventroyLogic.SheildBatteryLimit = value;
        }
    }
    public int Energy1BatteryLimit{
        get {
            return playerInventroyLogic.Energy1BatteryLimit;
        }
        set {
            playerInventroyLogic.Energy1BatteryLimit = value;
        }
    }
    public int Energy2BatteryLimit {
        get {
            return playerInventroyLogic.Energy2BatteryLimit;
        }
        set {
            playerInventroyLogic.Energy2BatteryLimit = value;
        }
    }

    public int Energy3BatteryLimit {
        get {
            return playerInventroyLogic.Energy3BatteryLimit;
        }
        set {
            playerInventroyLogic.Energy3BatteryLimit = value;
        }
    }

    private void Awake() {

        items = new ItemInformation[16];
        itemsamount = new int[16];
        isEquipped = new bool[16];

        playerInventroyLogic = new PlayerInventroyLogic(items , itemsamount, isEquipped);
        playerInventroyLogic.Awake();
    }

    // UI instance에서 가져오는 것은 awake 에서 적용시, 아직 awake 작업이 덜 되어서 null을 가져올 수 도 있으니 안됨
    // Start에서 해줘야 모든 awake 작업이 다 끝나고 한다는 보장이 있아
    private void Start() {
        playerInventroyLogic.Start();
    }

    private void Update() {
        playerInventroyLogic.Update();
    }

    public void LoadSave(PlayerInventorySave save)
    {
        playerInventroyLogic.LoadSave(save);
    }

    private void OnEnable() {
       playerInventroyLogic.delegateFunctions();
    }

    private void OnDisable() {
        playerInventroyLogic.removeFunctions();
    }

    public void EnergyReload()
    {
        playerInventroyLogic.EnergyReload();
        
    }

    public void SheildReLoad()
    {
        playerInventroyLogic.SheildReLoad();
    }

}
