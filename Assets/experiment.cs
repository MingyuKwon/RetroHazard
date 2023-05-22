using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class experiment : MonoBehaviour
{
    static int count = 1;

    GameObject prefab;

    private void Awake() {
        Debug.Log("Cube Awake : " + this.GetHashCode());
        

    }

    private void OnEnable() {
        Debug.Log("Cube OnEnable : " + this.GetHashCode());
    }

    private void Start() {
        Debug.Log("Cube Start : " + this.GetHashCode());
        
    }

    private void Update() {
        Debug.Log("Cube update : " + this.GetHashCode());
        MakeCube();
    }

    void MakeCube()
    {
        if(count < 2)
        {
            prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Cube.prefab", typeof(GameObject));
            count++;
            Instantiate(prefab);
        }
    }
}
