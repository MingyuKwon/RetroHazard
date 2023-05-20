using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUI : MonoBehaviour
{
    public static MouseUI instance;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        MouseActive(false);
    }

    public void MouseActive(bool flag)
    {
        this.gameObject.SetActive(flag);
    }
}
