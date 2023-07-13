using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerStatusLogic 
{
    private PlayerAnimation playerAnimation;

    [Header("Basic")]
    public float MaxHP = 100;
    public float CurrentHP = 100;
    public float Speed;

    [Header("Battle")]
    public float Attack = 0;
    public float ArmorDefence = 0;
    public bool SheildCrash = false;
    
    [Header("Equipped")]
    public int Energy = 0;
    public int[] EnergyDamage = {8, 30 , 100, 80};
    public int[] EnergyMaganize = {-1, 0 , 0, 0}; // Current sword Energy contain
    public int[] EnergyMaganizeMaximum = {-1, 0 , 0, 0}; // Current sword Energy contain
    public int[] EnergyUpgrade = {-1, 0 , 0, 0}; // Current sword Energy contain

    public int[] EnergyUPgradeUnit = {0, 5, 4, 3};

    public int Sheild = 0; // 0-> normal, 1-> parry, 2 -> big , 3-> none
    public float[] SheildMaganize = {0f , 0f, 0f, -1f}; // Current Sheild Durability contain
    public int[] SheildMaganizeMaximum = {0, 0, 0, 0}; 
    public int[] SheildUpgrade = {0 ,0, 0, 0};
    
    [Header("Store")]
    public int[] EnergyStore = {-1, 0 , 0, 0}; // Current sword Energy store in inventory
    public int SheildStore = 0; // Current Sheild Durability store in inventory

    [Header("Limit")]
    public int SheildBatteryLimit = 8;
    public int Energy1BatteryLimit = 30;
    public int Energy2BatteryLimit = 12;
    public int Energy3BatteryLimit = 9;
    

    [Header("InGame")]
    public bool parryFrame = false;
    public bool parrySuccess = false;
    public bool isBlocked = false;

    public PlayerStatusLogic(PlayerAnimation playerAnimation)
    {
        this.playerAnimation = playerAnimation;
    }

    public void LoadSave(PlayerStatSave save)
    {
        MaxHP = save.MaxHP;
        CurrentHP = save.CurrentHP;
        Speed = save.Speed;

        Attack = save.Attack;
        ArmorDefence = save.ArmorDefence;
        SheildCrash = save.SheildCrash;

        Energy = save.Energy;
        Array.Copy( save.EnergyDamage , EnergyDamage, save.EnergyDamage.Length);
        Array.Copy( save.EnergyMaganize , EnergyMaganize, save.EnergyMaganize.Length);
        Array.Copy( save.EnergyMaganizeMaximum , EnergyMaganizeMaximum, save.EnergyMaganizeMaximum.Length);
        Array.Copy( save.EnergyUpgrade , EnergyUpgrade, save.EnergyUpgrade.Length);

        Array.Copy( save.EnergyUPgradeUnit , EnergyUPgradeUnit, save.EnergyUPgradeUnit.Length);

        Sheild = save.Sheild;
        Array.Copy( save.SheildMaganize , SheildMaganize, save.SheildMaganize.Length);
        Array.Copy( save.SheildMaganizeMaximum , SheildMaganizeMaximum, save.SheildMaganizeMaximum.Length);
        Array.Copy( save.SheildUpgrade , SheildUpgrade, save.SheildUpgrade.Length);

        Array.Copy( save.EnergyStore , EnergyStore, save.EnergyStore.Length);
        SheildStore = save.SheildStore;
    }


    public void ChangeWeapon(int energy)
    {
        Energy = energy;
        Attack = EnergyDamage[Energy];
        
    }

    public void ChangeSheild(int sheildKind)
    {
        Sheild = sheildKind;

        SheildDurabilityChange(0);
        playerAnimation.SetAnimationFlag("Float","Sheild Kind", Sheild);
    }


    public void EnergyUse(int energyUse, int energyKind)
    {
        if(energyKind == 0) return;
        EnergyMaganize[energyKind] -= energyUse;
    }

    public void SheildDurabilityChange(float damage)
    {
        bool flag;
        flag = (damage == 0f) ? true : false;

        if(Sheild == 2 && damage > 0 )
        {
            SheildMaganize[Sheild] -= damage * 0.5f;
        }else
        {
            SheildMaganize[Sheild] -= damage;
        }
        
        GameManager.Sheild_Durability_Reducing = false;
        GameManager.EventManager.Invoke_Sheild_Durability_Change_Event(SheildMaganize[Sheild], Sheild);
        if(SheildMaganize[Sheild] <=0 )
        {
            SheildMaganize[Sheild] = 0;
            SheildCrash = true;
            GameManager.EventManager.Invoke_SheildCrashEvent(flag);
        }else
        {
            if(SheildCrash)
            {
                SheildCrash = false;
                GameManager.EventManager.Invoke_SheildRecoveryEvent(flag);
            }
        }
        
    }



    public void HealthChangeDefaultMinus(float damage)
    {
        if(damage == 0)
            return;

        if(damage == -1)
        {
            CurrentHP = MaxHP;
            return;
        }

        CurrentHP -= damage;
        if(CurrentHP <=0 )
        {
            CurrentHP = 0;
            GameManager.EventManager.Invoke_PlayerDeathEvent();
        }else if(CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }
    }

    public void SetSheildEquip(int sheildKind, bool isObtain)
    {
        if(sheildKind == 3) return;
        if(isObtain)
        {
            SheildUpgrade[sheildKind] = 1;
            
            if(SheildMaganizeMaximum[sheildKind] == 0)
            {
                SheildMaganizeMaximum[sheildKind] = 4;
            }
        }else
        {
            SheildUpgrade[sheildKind] = 0;
        }
    }

    public void SetEnergyEquipUpgrade(int energyKind,  bool isEquip, bool isObtain,  bool isFirst) 
    {
        if(isEquip) // weapon
        {
            if(!isObtain)
            {
                EnergyUpgrade[energyKind] = 0;
            }else
            {
                if(isFirst)
                {
                    EnergyUpgrade[energyKind] = 1;
                    EnergyMaganizeMaximum[energyKind] += EnergyUPgradeUnit[energyKind];
                }else
                {
                    EnergyUpgrade[energyKind] = EnergyMaganizeMaximum[energyKind] / EnergyUPgradeUnit[energyKind];
                }
                
            }
            
        }else // upgrade
        {
            EnergyUpgrade[energyKind]++;
            EnergyMaganizeMaximum[energyKind] += EnergyUPgradeUnit[energyKind];
        }
    }

    public void OnEnable() {
    }

    public void OnDisable() {
    }


}
