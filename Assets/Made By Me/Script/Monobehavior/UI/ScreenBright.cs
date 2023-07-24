using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBright : MonoBehaviour
{
    public static ScreenBright instance;
    public static float ScreenLight{
        get{
            return screenLight;
        }

        set{
            screenLight = value;
            screenLight = Mathf.Clamp(screenLight, 0f, 1f);

            overlayImage.color = new Color(overlayImage.color.r, overlayImage.color.g, overlayImage.color.b, 1 - screenLight);
            PlayerPrefs.SetFloat("Brightness", screenLight);
        }
    }

    static float screenLight;

    static Image overlayImage;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
            return;
        }

        overlayImage = GetComponentInChildren<Image>();

        if(!PlayerPrefs.HasKey("Brightness")){
            ScreenLight = 1f;
        }else
        {
            ScreenLight = PlayerPrefs.GetFloat("Brightness");
        }
    }
}
