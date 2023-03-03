using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.EventSystems;

public class NoUI : MonoBehaviour, IPointerEnterHandler
{
    TabUI tabUI;

    private void Awake() {
        tabUI = GetComponentInParent<TabUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!tabUI.isOpenedItem) return;
        tabUI.yesNoChoice = false;
    }
}
