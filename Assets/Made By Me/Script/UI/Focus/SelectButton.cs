using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    Image image;

    Color normalColor = new Color(90f / 255f, 80f / 255f, 80f / 255f, 0f );
    Color selectColor = new Color(90f / 255f, 80f / 255f, 80f / 255f, 0.7f );

    private void Awake() {
        image = GetComponent<Image>();
        image.color = normalColor;
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
