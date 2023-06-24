using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPanelUI : MonoBehaviour, IPointerEnterHandler
{
    BoxUI boxUI = null;
    
    private void Awake() {
        boxUI = GetComponentInParent<BoxUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        boxUI.isCursorInBox = false;
        if(boxUI.currentWindowLayer == 1 || boxUI.currentWindowLayer == 2) return;
        boxUI.GotoInventory();
    }
}
