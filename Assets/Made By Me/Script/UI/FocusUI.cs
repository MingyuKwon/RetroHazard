using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FocusUI : MonoBehaviour
{
    Image image;

    private void Awake() {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    public void SetFocus(bool flag)
    {
        image.enabled = flag;
    }
}
