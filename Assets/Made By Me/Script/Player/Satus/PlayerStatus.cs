using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerStatus : MonoBehaviour
{
    public static event Action<float> PlayerHealthChange;
    public static event Action PlayerDeath;

    [Header("Basic")]
    public float MaxHP = 100;
    public float CurrentHP = 50;
    public float Speed;

    [Header("Battle")]
    public float Attack = 0;
    public float ArmorDefence = 0;

    [Header("Equipped")]
    public int Energy = 0;
    public int[] EnergyDamage = {20, 100 , 200, 150};
    public int Sheild = 0; // 0-> normal, 1-> parry, 2 -> big
    

    [Header("InGame")]
    public bool parryFrame = false;
    public bool isBlocked = false;
    public string blockSuccessEnemy = null;


    public void HealthChange(float damage)
    {
        CurrentHP -= damage;
        if(CurrentHP <=0 )
        {
            PlayerHealthChange?.Invoke(0f);
            PlayerDeath?.Invoke();
        }
        PlayerHealthChange?.Invoke(CurrentHP);
    }
}

