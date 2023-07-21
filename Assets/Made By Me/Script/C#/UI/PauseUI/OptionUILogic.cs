using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUILogic
{
    OptionUI monoBehaviour;

    public int panelNum = 0; // 0 -> Control, 1 -> Audio, 2-> Vidio. 3-> General

    GameObject[] panels;
    Button[] buttons; // 0 : Control, 1 : Display, 2 : Sound, 3 : General

    public OptionUILogic(OptionUI monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;

        panels = new GameObject[monoBehaviour.transform.GetChild(2).childCount];

        for(int i=0; i< panels.Length; i++)
        {
            panels[i] = monoBehaviour.transform.GetChild(2).GetChild(i).gameObject;
        }
    }

    public void ChangePanel(int index)
    {
        for(int i=0; i< panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        panels[index].SetActive(true);
    }



    
}
