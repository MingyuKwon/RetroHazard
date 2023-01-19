using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleTonGenerator : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void Awake() {
        int count = FindObjectsOfType<PlayerAnimation>().Length;
        if(count < 1)
        {
            Instantiate(player);
        }
    }
}
