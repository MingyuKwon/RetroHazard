using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionButton : MonoBehaviour
{
    [SerializeField] int index;

    public GameObject focus;

    public void OnClick()
    {
        OptionUI.panelNum = index;
    }
}
