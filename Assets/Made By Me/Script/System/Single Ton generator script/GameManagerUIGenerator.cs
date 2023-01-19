using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerUIGenerator : MonoBehaviour
{
    [SerializeField] GameObject gameManagerUI;
    private void Awake() {
        int count = FindObjectsOfType<GameManagerUI>().Length;
        if(count < 1)
        {
            Instantiate(gameManagerUI);
        }
    }
}
