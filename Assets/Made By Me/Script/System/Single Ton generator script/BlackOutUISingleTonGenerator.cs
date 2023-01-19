using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOutUISingleTonGenerator : MonoBehaviour
{
    [SerializeField] GameObject blackOutUI;
    private void Awake() {
        int count = FindObjectsOfType<blackOut>().Length;
        if(count < 1)
        {
            Instantiate(blackOutUI);
        }
    }
}
