using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class BoxItemContainer : MonoBehaviour
{
    [SerializeField] int containerNum;
    public PlayerItemBox playerItemBox;

    Image backGround;
    Image fadeImage;
    public Image itemImage;
    public Text itemAmount;
    public Image EquipImage;
    public FocusUI focus;

    BoxUI boxUI;
    BoxItemUI boxItemUI;

    public GameObject focusSelectPanel;
    public bool isFocused = false;

    public int selectIndex = 0;

    private void Awake() {
        playerItemBox = FindObjectOfType<PlayerItemBox>();

        backGround = GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemAmount = transform.GetChild(1).GetComponentInChildren<Text>();
        EquipImage = transform.GetChild(2).GetComponent<Image>();
        fadeImage = transform.GetChild(3).GetComponent<Image>();
        focus = transform.GetChild(4).GetComponent<FocusUI>();

        boxUI = transform.parent.transform.parent.GetComponent<BoxUI>();
        boxItemUI = GetComponentInParent<BoxItemUI>();

        transform.GetChild(1).gameObject.SetActive(false);

        focusSelectPanel = focus.gameObject.transform.GetChild(0).gameObject;
        focusSelectPanel.SetActive(false);
        EquipImage.gameObject.SetActive(false);

        
    }    

    private void OnEnable() {
        if(boxItemUI.playerItemBox == null) return;
        if(boxItemUI.playerItemBox.items[containerNum] == null) return;
        selectIndex = 0;
    }

    private void Start() {
        focus.SetselectText(0, "Take item");
        focus.SetselectText(1, "");
        focus.SetselectText(2, "");
    }


    private void Update() {
        CheckWindowLayer();
        CheckContainerFull();
    }

    private void CheckContainerFull()
    {
        if(boxItemUI.playerItemBox == null) return;
        if(boxItemUI.playerItemBox.items[containerNum] == null) return;

        if(boxItemUI.playerItemBox.items[containerNum].isEnergy1)
        {
            if(boxItemUI.playerItemBox.itemsamount[containerNum] == boxItemUI.playerItemBox.Energy1BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }
        }else if(boxItemUI.playerItemBox.items[containerNum].isEnergy2)
        {
            if(boxItemUI.playerItemBox.itemsamount[containerNum] == boxItemUI.playerItemBox.Energy2BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }

        }else if(boxItemUI.playerItemBox.items[containerNum].isEnergy3)
        {
            if(boxItemUI.playerItemBox.itemsamount[containerNum] == boxItemUI.playerItemBox.Energy3BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }

        }else if(boxItemUI.playerItemBox.items[containerNum].isSheild)
        {
            if(boxItemUI.playerItemBox.itemsamount[containerNum] == boxItemUI.playerItemBox.SheildBatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }
        }
    }

    private void CheckWindowLayer()
    {
        if(boxUI.currentWindowLayer == 0)
        {
            SetSelect(false);
            backGround.color = new Color(1f, 1f, 1f, 1f);
            fadeImage.color = new Color(0f, 0f, 0f, 0f);

        }else if(boxUI.currentWindowLayer == 1)
        {
            if(isFocused)
            {
                SetSelect(true);
                backGround.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                fadeImage.color = new Color(0f, 0f, 0f, 0f);
                focus.SetSelect(selectIndex); 
            }else
            {
                SetSelect(false);
                backGround.color = new Color(1f, 1f, 1f, 1f);
                fadeImage.color = new Color(0f, 0f, 0f, 0f);
            }
        }
    }

    public void SetSelectIndex(int index)
    {
        selectIndex = 0;
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
