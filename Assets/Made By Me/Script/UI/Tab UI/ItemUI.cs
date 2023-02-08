using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;
public class ItemUI : MonoBehaviour
{
    ItemContainer[] itemContainers;
    int StartContainer = 8;

    private void Awake() {
        itemContainers = GetComponentsInChildren<ItemContainer>();
    }

    private void Start() {
        foreach(ItemContainer itemContainer in itemContainers)
        {
            itemContainer.gameObject.SetActive(false);
        }

        for(int i=0; i<StartContainer; i++)
        {
            itemContainers[i].gameObject.SetActive(true);
        }
    }
}
