using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class option : MonoBehaviour
{
    public Image image;
    public bool isSelected;

    Color selectedColor = new Color(1f,1f,1f,1f);
    Color unSelectedColor = new Color(1f,1f,1f,0.3f);

    void Awake()
    {
        image = GetComponent<Image>();
        image.color = unSelectedColor;
    }
    public void changeText(string text)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void Select(bool flag)
    {
        isSelected = flag;
        if(isSelected)
        {
            image.color = selectedColor;
        }else
        {
            image.color = unSelectedColor;
        }
    }
}
