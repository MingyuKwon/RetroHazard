using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveMessageUI : MonoBehaviour
{
    Text[] interactMessageTexts;
    public Image image;
    public Text amountText;

    private void Awake() {
        interactMessageTexts = GetComponentsInChildren<Text>();
        image = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(0).GetComponentInChildren<Text>();
    }

    public void SetInteractiveName(string str)
    {
        interactMessageTexts[1].text = "<b>" + str + "</b>";
    }

    public void SetInteractiveSituation(string str)
    {
        interactMessageTexts[2].text = str;
    }

    public void SetItemImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetAmountText(int amount)
    {
        if(amount < 2)
        {
            amountText.transform.parent.gameObject.SetActive(false);
        }else
        {
            amountText.transform.parent.gameObject.SetActive(true);
            amountText.text = amount.ToString();
        }
        
    }
}
