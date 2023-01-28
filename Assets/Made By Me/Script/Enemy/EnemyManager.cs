using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool isEnemyPaused = false;
    EnemyStatus status;

    private void Awake() {
        status = GetComponentInChildren<EnemyStatus>();
    }

}
