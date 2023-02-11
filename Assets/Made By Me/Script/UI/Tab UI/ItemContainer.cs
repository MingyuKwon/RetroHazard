using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class ItemContainer : MonoBehaviour
{
    public Image itemImage;
    public Text itemAmount;
    public FocusUI focus;

    TabUI tabUI;
    public GameObject focusSelectPanel;

    public bool isFocused = false;

    public int selectIndex = 0;

    private void Awake() {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemAmount = transform.GetChild(1).GetComponentInChildren<Text>();
        focus = transform.GetChild(2).GetComponent<FocusUI>();

        tabUI = transform.parent.transform.parent.GetComponent<TabUI>();

        transform.GetChild(1).gameObject.SetActive(false);

        focusSelectPanel = focus.gameObject.transform.GetChild(0).gameObject;
        focusSelectPanel.SetActive(false);
    }

    private void Update() {
        CheckWindowLayer();
    }

    private void OnEnable() {
        selectIndex = 0;
    }

    private void CheckWindowLayer()
    {
        if(!isFocused)
        {
            return;
        } 

        if(tabUI.currentWindowLayer == 0)
        {
            SetSelect(false);
        }else if(tabUI.currentWindowLayer == 1)
        {
            SetSelect(true);
            focus.SetSelect(selectIndex);
            
        }else if(tabUI.currentWindowLayer == 2)
        {

        }
    }

    public void SetItemAmountUI(bool flag)
    {
        transform.GetChild(1).gameObject.SetActive(flag);
    }

    public void SetItemAmountText(String str)
    {
        itemAmount.text = str;
    }

    private void SetSelect(bool flag)
    {
        focusSelectPanel.SetActive(flag);
    }

    public void SetFocus(bool flag)
    {
        focus.SetFocus(flag);
        isFocused = flag;
    }

}
