using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Create Enemy Status Value", fileName = "Enemy Stat Value")]
public class EnemyStatValue : ScriptableObject
{
    [Header("Basic")]
    public float MaxHP;
    public float CurrentHP;
    public float Speed;

    [Header("Battle")]
    public float Attack;
    public float ArmorDefence;

    [Space]
    public float bodyDamageRatio;
    public float AttackDamageRatio;
}
