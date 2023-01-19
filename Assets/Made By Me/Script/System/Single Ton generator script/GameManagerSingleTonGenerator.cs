using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleTonGenerator : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    private void Awake() {
        int count = FindObjectsOfType<GameManager>().Length;
        if(count < 1)
        {
            Instantiate(gameManager);
        }
    }
}
