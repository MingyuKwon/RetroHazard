using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStatSave 
{
    public float MaxHP;
    public float CurrentHP ;
    public float Speed;

    public float Attack;
    public float ArmorDefence;
    public bool SheildCrash;
    
    public int Energy;
    public int[] EnergyDamage;
    public int[] EnergyMaganize; // Current sword Energy contain
    public int[] EnergyMaganizeMaximum; // Current sword Energy contain
    public int[] EnergyUpgrade; // Current sword Energy contain

    public int[] EnergyUPgradeUnit;

    public int Sheild; // 0-> normal, 1-> parry, 2 -> big , 3-> none
    public float[] SheildMaganize ; // Current Sheild Durability contain
    public int[] SheildMaganizeMaximum ; 
    public int[] SheildUpgrade ;
    
    public int[] EnergyStore ; // Current sword Energy store in inventory
    public int SheildStore; // Current Sheild Durability store in inventory

    public PlayerStatSave(PlayerStatus status)
    {
        MaxHP = status.MaxHP;
        CurrentHP = status.CurrentHP;
        Speed = status.Speed;

        Attack = status.Attack;
        ArmorDefence = status.ArmorDefence;
        SheildCrash = status.SheildCrash;

        Energy = status.Energy;
        Array.Copy( status.EnergyDamage , EnergyDamage, status.EnergyDamage.Length);
        Array.Copy( status.EnergyMaganize , EnergyMaganize, status.EnergyMaganize.Length);
        Array.Copy( status.EnergyMaganizeMaximum , EnergyMaganizeMaximum, status.EnergyMaganizeMaximum.Length);
        Array.Copy( status.EnergyUpgrade , EnergyUpgrade, status.EnergyUpgrade.Length);

        Array.Copy( status.EnergyUPgradeUnit , EnergyUPgradeUnit, status.EnergyUPgradeUnit.Length);

        Sheild = status.Sheild;
        Array.Copy( status.SheildMaganize , SheildMaganize, status.SheildMaganize.Length);
        Array.Copy( status.SheildMaganizeMaximum , SheildMaganizeMaximum, status.SheildMaganizeMaximum.Length);
        Array.Copy( status.SheildUpgrade , SheildUpgrade, status.SheildUpgrade.Length);

        Array.Copy( status.EnergyStore , EnergyStore, status.EnergyStore.Length);
        SheildStore = status.SheildStore;
  
    }

    public PlayerStatSave()
    {
        
    }
    
}
