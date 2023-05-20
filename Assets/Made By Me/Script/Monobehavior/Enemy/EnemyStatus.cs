using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] EnemyStatValue[] EnemyStats;
    public static event Action EnemyHealthChange;
    public static event Action<int> EnemyDeath;
    private EnemyManager enemyManager = null;

    [Header("Basic")]
    public float MaxHP = 100f;
    public float CurrentHP = 50f;
    public float Speed;

    [Header("Battle")]
    public float Attack = 0f;
    public float ArmorDefence = 0f;
    [Space]
    public float bodyDamageRatio;
    public float AttackDamageRatio;

    public bool ParriedWithParrySheild;

    private void OnEnable() {
        EnemyDeath += DestroySelf;
    }

    private void OnDisable() {
        EnemyDeath -= DestroySelf;
    }

    private void Awake() {
        enemyManager = transform.parent.GetComponent<EnemyManager>();
    }

    private void Start() {
        MaxHP = EnemyStats[GameManager.DEFCON].MaxHP;
        CurrentHP = EnemyStats[GameManager.DEFCON].CurrentHP;
        Speed = EnemyStats[GameManager.DEFCON].Speed;

        Attack = EnemyStats[GameManager.DEFCON].Attack;
        ArmorDefence = EnemyStats[GameManager.DEFCON].ArmorDefence;

        bodyDamageRatio = EnemyStats[GameManager.DEFCON].bodyDamageRatio;
        AttackDamageRatio = EnemyStats[GameManager.DEFCON].AttackDamageRatio;
    }

    public void HealthChange(float damage)
    {
        CurrentHP -= damage;
        EnemyHealthChange?.Invoke();
        if(CurrentHP <=0 )
        {
            EnemyDeath?.Invoke(transform.parent.transform.GetSiblingIndex());
        }
    }

    private void DestroySelf(int index)
    {
        if(index != transform.parent.transform.GetSiblingIndex()) return;
        
        enemyManager.KillEnemy();
    }


}
