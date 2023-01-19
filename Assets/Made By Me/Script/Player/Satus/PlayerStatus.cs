using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerStatus : MonoBehaviour
{
    [Header("Basic")]
    public float MaxHP = 100f;
    public float CurrentHP = 50f;
    public float Speed;

    [Header("Battle")]
    public float Attack = 0f;
    public float ArmorDefence = 0f;

    [Header("Equipped")]
    public int Weapon = 1;
    public int Sheild = 101;
    public int Staff = 201;
}
