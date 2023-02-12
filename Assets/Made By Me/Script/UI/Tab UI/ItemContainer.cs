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
    [SerializeField] int containerNum;

    Image backGround;
    public Image itemImage;
    public Text itemAmount;
    public FocusUI focus;

    TabUI tabUI;
    ItemUI itemUI;

    public GameObject focusSelectPanel;

    public bool isFocused = false;

    int indexLimitMin = 0;
    int indexLimitMax = 2;

    public int selectIndex = 0;

    private void Awake() {
        backGround = GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemAmount = transform.GetChild(1).GetComponentInChildren<Text>();
        focus = transform.GetChild(2).GetComponent<FocusUI>();

        tabUI = transform.parent.transform.parent.GetComponent<TabUI>();
        itemUI = GetComponentInParent<ItemUI>();

        transform.GetChild(1).gameObject.SetActive(false);

        focusSelectPanel = focus.gameObject.transform.GetChild(0).gameObject;
        focusSelectPanel.SetActive(false);
    }

    private void Update() {
        CheckWindowLayer();
    }

    private void OnEnable() {
        if(itemUI != null)
        {
            if(itemUI.playerInventory == null) return;

            if(itemUI.playerInventory.items[containerNum] == null) return;

            if(itemUI.playerInventory.items[containerNum].isKeyItem)
            {
                if(!tabUI.isInteractive)
                {
                    indexLimitMin = 1;
                    focus.selectButtons[0].gameObject.SetActive(false);
                }else
                {
                    indexLimitMin = 0;
                    focus.selectButtons[0].gameObject.SetActive(true);
                }
                
                indexLimitMax = 1;
                focus.selectButtons[2].gameObject.SetActive(false);
            }
        }

        selectIndex = indexLimitMin;
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
            backGround.color = new Color(1f, 1f, 1f, 1f);
        }else if(tabUI.currentWindowLayer == 1)
        {
            SetSelect(true);
            backGround.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            focus.SetSelect(selectIndex);
            
        }else if(tabUI.currentWindowLayer == 2)
        {

        }
    }

    public void SetSelectIndex(int index)
    {
        selectIndex = Mathf.Clamp(index, indexLimitMin, indexLimitMax);
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
