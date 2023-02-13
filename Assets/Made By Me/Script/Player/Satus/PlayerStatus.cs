using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerStatus : MonoBehaviour
{
    public static event Action<float, int> SheildDurabilityChangeEvent;

    static public event Action<float,float, int, float, float , int, float, float, int> Update_IngameUI_Event; // Max HP, Current Hp, Energy, EnergyMaganize[Energy], EnergtStore[Energy] , Sheild, SheildMaganize[Sheild] , SheildStore , energyUpgrade

    public static event Action PlayerDeathEvent;
    public static event Action<bool> SheildCrashEvent;
    public static event Action<bool> SheildRecoveryEvent;

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
    public int[] EnergyMaganizeMaximum = {-1, 5 , 4, 3}; // Current sword Energy contain
    public int[] EnergyUpgrade = {-1, 0 , 0, 0}; // Current sword Energy contain

    public int Sheild = 0; // 0-> normal, 1-> parry, 2 -> big
    public float[] SheildMaganize = {0f , 0f, 0f}; // Current Sheild Durability contain
    public int SheildMaganizeMaximum = 4; // Current Sheild Durability contain

    
    [Header("Store")]
    public int[] EnergyStore = {-1, 0 , 0, 0}; // Current sword Energy store in inventory
    public int SheildStore = 0; // Current Sheild Durability store in inventory

    [Header("InGame")]
    public bool parryFrame = false;
    public bool parrySuccess = false;
    public bool isBlocked = false;
    public string blockSuccessEnemy = null;

    private void Awake() {
        Attack = EnergyDamage[Energy];
    }

    private void OnEnable() {
    }

    public void UpdateIngameUI()
    {
        Update_IngameUI_Event?.Invoke(MaxHP, CurrentHP, Energy, EnergyMaganize[Energy], EnergyStore[Energy] ,  Sheild, SheildMaganize[Sheild] , SheildStore, EnergyUpgrade[Energy]);
    }


    public void Obtain_Expansion_Item(ItemInformation itemInformation, int amount)
    {
        if(itemInformation.isEnergy1)
        {
            EnergyMaganizeMaximum[1] += amount;
        }else if(itemInformation.isEnergy2)
        {
            EnergyMaganizeMaximum[2] += amount;
        }else if(itemInformation.isEnergy3)
        {
            EnergyMaganizeMaximum[3] += amount;
        }
        UpdateIngameUI();
    }

    private void Start() {
        UpdateIngameUI();
        SheildDurabilityChange(0);
    }

    public void HealthChange(float damage)
    {
        CurrentHP -= damage;
        if(CurrentHP <=0 )
        {
            CurrentHP = 0;
            PlayerDeathEvent?.Invoke();
        }
        UpdateIngameUI();
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
        
        GameManager.instance.Sheild_Durability_Reducing = false;
        SheildDurabilityChangeEvent?.Invoke(SheildMaganize[Sheild], Sheild);
        if(SheildMaganize[Sheild] <=0 )
        {
            SheildMaganize[Sheild] = 0;
            SheildCrash = true;
            SheildCrashEvent?.Invoke(flag);
        }else
        {
            if(SheildCrash)
            {
                SheildCrash = false;
                SheildRecoveryEvent?.Invoke(flag);
            }
        }
        
    }

    public void EnergyUse(int energyUse, int energyKind)
    {
        if(energyKind == 0) return;
        EnergyMaganize[energyKind] -= energyUse;
        UpdateIngameUI();
    }

}

