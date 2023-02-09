using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveMessageUI : MonoBehaviour
{
    Text[] interactMessageTexts;
    public Image image;

    private void Awake() {
        interactMessageTexts = GetComponentsInChildren<Text>();
        image = transform.GetChild(0).GetComponent<Image>();
    }

    public void SetInteractiveName(string str)
    {
        interactMessageTexts[0].text = "<b>" + str + "</b>";
    }

    public void SetInteractiveSituation(string str)
    {
        interactMessageTexts[1].text = str;
    }

    public void SetItemImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
