using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] WayPoint wayPoint;

    private void Awake() {
        SaveSystem.instance.Initialize();
        Debug.Log("GameStart Awake");
        SaveSystem.instance.Load(0);
        
    }

}
