using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangerInputSingleTonGenerator : MonoBehaviour
{
    [SerializeField] GameObject gameMangerInput;
    private void Awake() {
        int count = FindObjectsOfType<GameMangerInput>().Length;
        if(count < 1)
        {
            Instantiate(gameMangerInput);
        }
    }
}
