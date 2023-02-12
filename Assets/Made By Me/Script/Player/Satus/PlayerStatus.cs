using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerStatus : MonoBehaviour
{
    public static event Action<float, float> PlayerHealthChangeEvent;
    public static event Action<float, int> SheildDurabilityChangeEvent;
    public static event Action<float, int> EnergyChangeEvent;

    public static event Action<float> Sheild_Durability_Item_Obtain_Event;
    public static event Action<float, int> Energy_Item_Obtain_Event;

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
    public float EnergyAmount = 0f;
    public int[] EnergyDamage = {8, 30 , 100, 80};
    public float[] EnergyMaganize = {-1f, 0f , 0f, 0f}; // Current sword Energy contain
    public float[] EnergyMaganizeMaximum = {-1f, 5f , 4f, 3f}; // Current sword Energy contain

    public int Sheild = 0; // 0-> normal, 1-> parry, 2 -> big
    public float SheildDurability = 0f;
    public float[] SheildMaganize = {0f , 0f, 0f}; // Current Sheild Durability contain
    public float SheildMaganizeMaximum = 4f; // Current Sheild Durability contain

    
    [Header("Store")]
    public float[] EnergyStore = {-1f, 0f , 0f, 0f}; // Current sword Energy store in inventory
    public float SheildStore = 0f; // Current Sheild Durability store in inventory

    [Header("InGame")]
    public bool parryFrame = false;
    public bool parrySuccess = false;
    public bool isBlocked = false;
    public string blockSuccessEnemy = null;

    private void Awake() {
        Attack = EnergyDamage[Energy];
        SheildDurability = SheildMaganize[Sheild];
        EnergyAmount = EnergyMaganize[Energy];
    }

    private void OnEnable() {
        //bulletItem.Obtain_bullet_Item_Event +=
    }

    public void Use_bullet_Item(ItemInformation itemInformation, int amount)
    {
        if(itemInformation.isSheild)
        {
            Sheild_Item_Obtain(amount, Sheild);
            return;
        }else if(itemInformation.isEnergy1)
        {
            Energy_Item_Obtain(amount, 1);
            return;
        }else if(itemInformation.isEnergy2)
        {
            Energy_Item_Obtain(amount, 2);
            return;
        }else if(itemInformation.isEnergy3)
        {
            Energy_Item_Obtain(amount, 3);
            return;
        }
    }

    public void Use_Expansion_Item(ItemInformation itemInformation, int amount)
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
        EnergyChange(0);
    }



    public void Energy_Item_Obtain(int EnergyObtain, int EnergyKind)
    {
        EnergyStore[EnergyKind] += EnergyObtain;
        Energy_Item_Obtain_Event?.Invoke(EnergyStore[EnergyKind], EnergyKind);
    }
    public void Sheild_Item_Obtain(int SheildObtain, int SheildKind)
    {
        SheildStore += SheildObtain;
        Sheild_Durability_Item_Obtain_Event?.Invoke(SheildStore);
    }

    private void Start() {
        HealthChange(0);
        EnergyChange(0);
        SheildDurabilityChange(0);
    }

    public void HealthChange(float damage)
    {
        CurrentHP -= damage;
        if(CurrentHP <=0 )
        {
            CurrentHP = 0;
            PlayerHealthChangeEvent?.Invoke(CurrentHP, MaxHP);
            PlayerDeathEvent?.Invoke();
        }
        PlayerHealthChangeEvent?.Invoke(CurrentHP, MaxHP);
    }

    public void SheildDurabilityChange(float damage)
    {
        bool flag;
        flag = (damage == 0f) ? true : false;

        if(Sheild == 2 && damage > 0 )
        {
            SheildDurability -= damage * 0.5f;
        }else
        {
            SheildDurability -= damage;
        }
        
        GameManager.instance.Sheild_Durability_Reducing = false;
        SheildMaganize[Sheild] = (int)SheildDurability;
        SheildDurabilityChangeEvent?.Invoke(SheildDurability, Sheild);
        if(SheildDurability <=0 )
        {
            SheildDurability = 0;
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

    public void EnergyChange(float energyReduce)
    {
        if(Energy != 0)
        {
            EnergyAmount -= energyReduce;
            if(EnergyAmount <= 0)
            {
                EnergyAmount = 0;
            }
            EnergyMaganize[Energy] = (int)EnergyAmount;
        }

        EnergyChangeEvent?.Invoke(EnergyAmount, Energy);
    }
}

