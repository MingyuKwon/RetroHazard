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

    private void Awake() {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemAmount = transform.GetChild(1).GetComponentInChildren<Text>();
        focus = transform.GetChild(2).GetComponent<FocusUI>();

        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void SetItemAmountUI(bool flag)
    {
        transform.GetChild(1).gameObject.SetActive(flag);
    }

    public void SetItemAmountText(String str)
    {
        itemAmount.text = str;
    }
}
