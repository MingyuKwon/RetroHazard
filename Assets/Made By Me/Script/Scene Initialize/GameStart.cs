using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] WayPoint wayPoint;

    private void Awake() {
        SaveSystem.instance.Initialize();
    }
    void Start()
    {
        SaveSystem.instance.Load(0);
        
        if(SaveSystem.SaveSlotNum == -1)
        {
            FindObjectOfType<PlayerHealth>().gameObject.transform.position = new Vector3(wayPoint.x, wayPoint.y, 0);
        }
    }

}
