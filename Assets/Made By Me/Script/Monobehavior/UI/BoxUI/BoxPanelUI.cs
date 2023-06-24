using UnityEngine;
using UnityEngine.EventSystems;


public class BoxPanelUI : MonoBehaviour, IPointerEnterHandler
{
    BoxUI boxUI = null;

    private void Awake() {
        boxUI = GetComponentInParent<BoxUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        boxUI.isCursorInBox = true;
        if(boxUI.currentWindowLayer == 1 || boxUI.currentWindowLayer == 2) return;
        boxUI.GotoBox();
    }
}
