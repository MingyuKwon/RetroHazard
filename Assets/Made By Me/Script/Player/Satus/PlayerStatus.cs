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
    public static event Action<float> EnergyChangeEvent;

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
    public float SheildDurability = 0;
    public bool SheildCrash = false;
    public float EnergyAmount = 0;

    [Header("Equipped")]
    public int Energy = 0;
    public int[] EnergyDamage = {20, 100 , 200, 150};
    public int[] EnergyMaganize = {-1, 0 , 0, 0};
    public int[] EnergyMaganizeMaximum = {-1, 10 , 8, 5};

    public int Sheild = 0; // 0-> normal, 1-> parry, 2 -> big
    public int[] SheildMaganize = {1 , 0, 0};
    public int[] SheildMaganizeMaximum = { 4 , 4, 4};
    

    [Header("InGame")]
    public bool parryFrame = false;
    public bool parrySuccess = false;
    public bool isBlocked = false;
    public string blockSuccessEnemy = null;

    private void Awake() {
        Attack = EnergyDamage[Energy];
        SheildDurability = SheildMaganize[Energy];
        EnergyAmount = EnergyMaganize[Energy];
    }

    private void Start() {
        PlayerHealthChangeEvent?.Invoke(CurrentHP, MaxHP);
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

        EnergyChangeEvent?.Invoke(EnergyAmount);
    }
}

