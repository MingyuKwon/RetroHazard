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

    private void Awake() {
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }
}
