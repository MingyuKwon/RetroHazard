using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int index;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OptionUI.panelNum = index;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
