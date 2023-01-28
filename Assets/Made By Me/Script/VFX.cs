using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class VFX : MonoBehaviour
{
    public CapsuleCollider2D BodyCollider;
    public BoxCollider2D attackCollider;
    public BoxCollider2D sheildCollider;

    private EnemyManager enemyManager = null;
    private bool isPlayer;

    private void Awake() {
        enemyManager = transform.parent.GetComponent<EnemyManager>();
        
        if(enemyManager == null)
        {
            isPlayer = true;
        }else
        {
            isPlayer = false;
        }
    }

    public void ParryVFXStart()
    {
        if(isPlayer)
        {
            BodyCollider.enabled = false;
        }
        
    }

    public void ParryVFXEnd()
    {
        if(isPlayer)
        {
            BodyCollider.enabled = true;
        }
        
    }


    public void ParreidVFXStart()
    {
        attackCollider.enabled = false;
        if(isPlayer)
        {
            GameManager.instance.SetPausePlayer(true);
        }else
        {
            enemyManager.SetEnemyParried(true);
            enemyManager.SetEnemyStaggered(true);
        }

    }

    public void ParriedVFXEnd()
    {
        attackCollider.enabled = true;
        if(isPlayer)
        {
            GameManager.instance.SetPausePlayer(false);
        }else
        {
            enemyManager.SetEnemyParried(false);
            enemyManager.SetEnemyStaggered(false);
        }
    }
}
