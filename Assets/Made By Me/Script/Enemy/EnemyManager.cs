using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool isEnemyPaused = false;
    public bool isParried = false;
    EnemyStatus status;

    private void Awake() {
        status = GetComponentInChildren<EnemyStatus>();
    }

    public void SetEnemyParried()
    {
        
    }

}
