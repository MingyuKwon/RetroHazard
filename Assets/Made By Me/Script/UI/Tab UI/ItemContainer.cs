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
    public FocusUI focus;

    private void Awake() {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        focus = transform.GetChild(1).GetComponent<FocusUI>();
    }
}
