using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    GameObject[] panels;
    Button[] buttons; // 0 : Control, 1 : Display, 2 : Sound, 3 : General

    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
        panels = new GameObject[transform.GetChild(2).childCount];

        for(int i=0; i< panels.Length; i++)
        {
            panels[i] = transform.GetChild(2).GetChild(i).gameObject;
        }
    }

    private void OnEnable() {

        for(int i=0; i<buttons.Length; i++)
        {
            int temp = i;
            buttons[i].onClick.AddListener(() => OnClick(temp));
        }

        OnClick(0);
        
    }
    private void OnDisable() {

        for(int i=0; i<buttons.Length; i++)
        {
            int temp = i;
            buttons[i].onClick.RemoveAllListeners();
        }
    }

    private void OnClick(int n)
    {
        for(int i=0; i< panels.Length; i++)
        {
            panels[i].gameObject.SetActive(false);
        }

        panels[n].gameObject.SetActive(true);
        
    }
    
}
