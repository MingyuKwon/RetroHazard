using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerStatus : MonoBehaviour
{
    private PlayerStatusLogic playerStatusLogic;
    private PlayerAnimation playerAnimation;

    ///"Basic"////
    public float MaxHP{
        get{
            return playerStatusLogic.MaxHP;
        }

        set{
            playerStatusLogic.MaxHP = value;
        }
    }
    public float CurrentHP {
        get{
            return playerStatusLogic.CurrentHP;
        }

        set{
            playerStatusLogic.CurrentHP = value;
        }
    }
    public float Speed{
        get{
            return playerStatusLogic.Speed;
        }

        set{
            playerStatusLogic.Speed = value;
        }
    }

    ///"Battle"////

    public float Attack{
        get{
            return playerStatusLogic.Attack;
        }

        set{
            playerStatusLogic.Attack = value;
        }
    }

    public float ArmorDefence{
        get{
            return playerStatusLogic.ArmorDefence;
        }

        set{
            playerStatusLogic.ArmorDefence = value;
        }
    }
    public bool SheildCrash{
        get{
            return playerStatusLogic.SheildCrash;
        }

        set{
            playerStatusLogic.SheildCrash = value;
        }
    }
    

    ///"Equipped"////
    public int Energy{
        get{
            return playerStatusLogic.Energy;
        }

        set{
            playerStatusLogic.Energy = value;
        }
    }

    public int[] EnergyDamage{
        get{
            return playerStatusLogic.EnergyDamage;
        }

        set{
            playerStatusLogic.EnergyDamage = value;
        }
    }

    public int[] EnergyMaganize {
        get{
            return playerStatusLogic.EnergyMaganize;
        }

        set{
            playerStatusLogic.EnergyMaganize = value;
        }
    }

    public int[] EnergyMaganizeMaximum {
        get{
            return playerStatusLogic.EnergyMaganizeMaximum;
        }

        set{
            playerStatusLogic.EnergyMaganizeMaximum = value;
        }
    }
    
    public int[] EnergyUpgrade {
        get{
            return playerStatusLogic.EnergyUpgrade;
        }

        set{
            playerStatusLogic.EnergyUpgrade = value;
        }
    }

    public int[] EnergyUPgradeUnit {
        get{
            return playerStatusLogic.EnergyUPgradeUnit;
        }

        set{
            playerStatusLogic.EnergyUPgradeUnit = value;
        }
    }

    public int Sheild {
        get{
            return playerStatusLogic.Sheild;
        }

        set{
            playerStatusLogic.Sheild = value;
        }
    }

    public float[] SheildMaganize {
        get{
            return playerStatusLogic.SheildMaganize;
        }

        set{
            playerStatusLogic.SheildMaganize = value;
        }
    }

    public int[] SheildMaganizeMaximum {
        get{
            return playerStatusLogic.SheildMaganizeMaximum;
        }

        set{
            playerStatusLogic.SheildMaganizeMaximum = value;
        }
    }

    public int[] SheildUpgrade {
        get{
            return playerStatusLogic.SheildUpgrade;
        }

        set{
            playerStatusLogic.SheildUpgrade = value;
        }
    }
    
    /////"Store"/////
    public int[] EnergyStore {
        get{
            return playerStatusLogic.EnergyStore;
        }

        set{
            playerStatusLogic.EnergyStore = value;
        }
    }

    public int SheildStore {
        get{
            return playerStatusLogic.SheildStore;
        }

        set{
            playerStatusLogic.SheildStore = value;
        }
    }

    /////"Limit"/////
    public int SheildBatteryLimit{
        get{
            return playerStatusLogic.SheildBatteryLimit;
        }

        set{
            playerStatusLogic.SheildBatteryLimit = value;
        }
    }
    public int Energy1BatteryLimit{
        get{
            return playerStatusLogic.Energy1BatteryLimit;
        }

        set{
            playerStatusLogic.Energy1BatteryLimit = value;
        }
    }
    public int Energy2BatteryLimit{
        get{
            return playerStatusLogic.Energy2BatteryLimit;
        }

        set{
            playerStatusLogic.Energy2BatteryLimit = value;
        }
    }
    public int Energy3BatteryLimit{
        get{
            return playerStatusLogic.Energy3BatteryLimit;
        }

        set{
            playerStatusLogic.Energy3BatteryLimit = value;
        }
    }
    


    /////"InGame"/////
    public bool parryFrame {
        get{
            return playerStatusLogic.parryFrame;
        }

        set{
            playerStatusLogic.parryFrame = value;
        }
    }

    public bool parrySuccess {
        get{
            return playerStatusLogic.parrySuccess;
        }

        set{
            playerStatusLogic.parrySuccess = value;
        }
    }

    public bool isBlocked {
        get{
            return playerStatusLogic.isBlocked;
        }

        set{
            playerStatusLogic.isBlocked = value;
        }
    }

    public string blockSuccessEnemy {
        get{
            return playerStatusLogic.blockSuccessEnemy;
        }

        set{
            playerStatusLogic.blockSuccessEnemy = value;
        }
    }


    private void Awake() {
        playerAnimation = transform.parent.gameObject.GetComponent<PlayerAnimation>();
        playerStatusLogic = new PlayerStatusLogic(playerAnimation);
    }

    private void Start() {
        UI.instance.inGameUI.UpdateIngameUI();
        SheildDurabilityChange(0);
    }

    private void OnEnable() {
        playerStatusLogic.OnEnable();
    }

    private void OnDisable() {
        playerStatusLogic.OnDisable();
    }
    public void LoadSave(PlayerStatSave save)
    {
        playerStatusLogic.LoadSave(save);
    }

    public void ChangeWeapon(int energy)
    {
        playerStatusLogic.ChangeWeapon(energy);
        UI.instance.inGameUI.UpdateIngameUI();
    }

    public void ChangeSheild(int sheildKind)
    {
        playerStatusLogic.ChangeSheild(sheildKind);
        UI.instance.inGameUI.UpdateIngameUI();
    }

    public void SetSheildEquip(int sheildKind, bool isObtain)
    {
        playerStatusLogic.SetSheildEquip(sheildKind, isObtain);
    }

    public void SetEnergyEquipUpgrade(int energyKind,  bool isEquip, bool isObtain,  bool isFirst) 
    {
        playerStatusLogic.SetEnergyEquipUpgrade(energyKind, isEquip, isObtain, isFirst);
    }

    public void HealthChangeDefaultMinus(float damage)
    {
        playerStatusLogic.HealthChangeDefaultMinus(damage);
        UI.instance.inGameUI.UpdateIngameUI();
    }

    public void SheildDurabilityChange(float damage)
    {
        playerStatusLogic.SheildDurabilityChange(damage);
        UI.instance.inGameUI.UpdateIngameUI();
    }

    public void EnergyUse(int energyUse, int energyKind)
    {
        playerStatusLogic.EnergyUse(energyUse, energyKind);
        UI.instance.inGameUI.UpdateIngameUI();
    }

}

