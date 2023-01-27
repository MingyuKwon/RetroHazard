using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerStatus : MonoBehaviour
{
    [Header("Basic")]
    public int MaxHP = 100;
    public int CurrentHP = 50;
    public int Speed;

    [Header("Battle")]
    public int Attack = 0;
    public int ArmorDefence = 0;

    [Header("Equipped")]
    public int Weapon = 1;
    public int Sheild = 101;

    [Header("InGame")]
    public bool parryFrame = false;
}

