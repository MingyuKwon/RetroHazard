using UnityEngine;
using UnityEngine.UI;


public class ItemExplainUI : MonoBehaviour
{
    Text[] itemExplainText; // 0: name, 1 : explain

    private void Awake() {
        itemExplainText = GetComponentsInChildren<Text>();
    }

    public void SetItemName(string str)
    {
        itemExplainText[0].text = "<b>" + str + "</b>";
    }

    public void SetItemExplain(string str)
    {
        itemExplainText[1].text = str;
    }
}
