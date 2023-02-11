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
    public static event Action EnemyDeath;
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
        MaxHP = EnemyStats[GameManager.instance.DEFCON].MaxHP;
        CurrentHP = EnemyStats[GameManager.instance.DEFCON].CurrentHP;
        Speed = EnemyStats[GameManager.instance.DEFCON].Speed;

        Attack = EnemyStats[GameManager.instance.DEFCON].Attack;
        ArmorDefence = EnemyStats[GameManager.instance.DEFCON].ArmorDefence;

        bodyDamageRatio = EnemyStats[GameManager.instance.DEFCON].bodyDamageRatio;
        AttackDamageRatio = EnemyStats[GameManager.instance.DEFCON].AttackDamageRatio;
    }

    public void HealthChange(float damage)
    {
        CurrentHP -= damage;
        EnemyHealthChange?.Invoke();
        if(CurrentHP <=0 )
        {
            EnemyDeath?.Invoke();
        }
    }

    private void DestroySelf()
    {
        enemyManager.KillEnemy();
    }


}
