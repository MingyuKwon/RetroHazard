using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int index;
    Image image;
    ItemContainer itemContainer;

    Color normalColor = new Color(90f / 255f, 80f / 255f, 80f / 255f, 0f );
    Color selectColor = new Color(90f / 255f, 80f / 255f, 80f / 255f, 0.7f );

    private void Awake() {
        image = GetComponent<Image>();
        image.color = normalColor;
        itemContainer = transform.parent.parent.parent.GetComponent<ItemContainer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemContainer.selectIndex = index;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemContainer.selectIndex = -1;
    }

    public void SetSelect(bool flag)
    {
        if(flag)
        {
            image.color = selectColor;
        }else
        {
            image.color = normalColor;
        }
    }
}
