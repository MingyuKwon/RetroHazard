using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabShowingImageButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int index;
    public void OnPointerEnter(PointerEventData eventData)
    {
        UI.instance.tabUI.isMouseOnshowingTabIndex = index;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.instance.tabUI.isMouseOnshowingTabIndex = -1;
    }
}
