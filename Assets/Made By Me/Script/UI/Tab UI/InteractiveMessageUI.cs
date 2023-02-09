using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveMessageUI : MonoBehaviour
{
    Text[] interactMessageTexts;

    private void Awake() {
        interactMessageTexts = GetComponentsInChildren<Text>();
    }

    public void SetInteractiveName(string str)
    {
        interactMessageTexts[0].text = "<b>" + str + "</b>";
    }

    public void SetInteractiveSituation(string str)
    {
        interactMessageTexts[1].text = str;
    }
}
