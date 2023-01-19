using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUISingleTonGenerator : MonoBehaviour
{
    [SerializeField] GameObject dialogUI;
    private void Awake() {
        int count = FindObjectsOfType<DialogUI>().Length;
        if(count < 1)
        {
            Instantiate(dialogUI);
        }
    }
}
