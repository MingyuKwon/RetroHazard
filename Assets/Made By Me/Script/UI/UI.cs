using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;

public class UI : MonoBehaviour
{
    public static UI instance;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

    }

    public void MouseCursor(bool flag)
    {
        transform.GetChild(6).transform.GetChild(0).gameObject.SetActive(flag);
    }
}
