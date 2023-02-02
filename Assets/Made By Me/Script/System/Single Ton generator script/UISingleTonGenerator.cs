using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISingleTonGenerator : MonoBehaviour
{
    [SerializeField] GameObject UI;
    private void Awake() {
        int count = FindObjectsOfType<UI>().Length;
        if(count < 1)
        {
            Instantiate(UI);
        }
    }
}
