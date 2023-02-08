using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;

public class TabUI : MonoBehaviour
{
    public bool isShowing = false;

    ItemUI itemUI;
    ItemExplainUI itemExplainUI;
    CurrentGoalUI currentGoalUI;
    InteractiveMessageUI interactiveMessageUI;
    EquippedUI equippedUI;

    private void Awake() {
        itemUI = GetComponentInChildren<ItemUI>();
        itemExplainUI = GetComponentInChildren<ItemExplainUI>();
        currentGoalUI = GetComponentInChildren<CurrentGoalUI>();
        interactiveMessageUI = GetComponentInChildren<InteractiveMessageUI>();
        equippedUI = GetComponentInChildren<EquippedUI>();
    }

    public void Visualize_Tab_Interactive(bool flag)
    {
        if(isShowing && flag) return;

        if(flag)
        {

            
        }
        else
        {

        }

        isShowing = flag;
        itemUI.gameObject.SetActive(flag);
        itemExplainUI.gameObject.SetActive(flag);
        interactiveMessageUI.gameObject.SetActive(flag);
        currentGoalUI.gameObject.SetActive(false);
        equippedUI.gameObject.SetActive(false);

    }

    public void Visualize_Tab_Menu(bool flag)
    {
        if(isShowing && flag) return;

        if(flag)
        {

        }
        else
        {
            
        }

        isShowing = flag;
        itemUI.gameObject.SetActive(flag);
        itemExplainUI.gameObject.SetActive(flag);
        currentGoalUI.gameObject.SetActive(flag);
        equippedUI.gameObject.SetActive(flag);
        interactiveMessageUI.gameObject.SetActive(false);
    }
}
