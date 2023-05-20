using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public void OnScreenPositionChanged(Vector2 screenPosition) {
    if(Camera.main == null) return;
    
    RectTransform canvasRectTransform = transform.parent.GetComponent<Canvas>().GetComponent<RectTransform>();
    Rect rootCanvasRect = canvasRectTransform.rect;

    Vector2 viewportPos = Camera.main.ScreenToViewportPoint(screenPosition);
    viewportPos.x = (viewportPos.x * rootCanvasRect.width) - canvasRectTransform.pivot.x * rootCanvasRect.width;
    viewportPos.y = (viewportPos.y * rootCanvasRect.height) - canvasRectTransform.pivot.y * rootCanvasRect.height;
    (transform as RectTransform).anchoredPosition = viewportPos;
    }
}
