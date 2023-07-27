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
    }

    private void OnDisable() {
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
        Debug.Log("Enemy had damage : " + damage + "\n Current HP : " + enemyManager.enemyStatus.CurrentHP);

        EnemyHealthChange?.Invoke();
        if(CurrentHP <=0 )
        {
            enemyManager.KillEnemy();
        }
    }

}
