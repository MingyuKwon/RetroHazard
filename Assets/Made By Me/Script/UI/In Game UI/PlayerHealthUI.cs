using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelsoftGames.PixelUI;

public class PlayerHealthUI : MonoBehaviour
{
    Text healthText;
    RectTransform UItransform;
    UIStatBar healthBar;

     

    private void Awake() {
        UItransform = GetComponent<RectTransform>();
        healthText = GetComponentInChildren<Text>();
        healthBar = GetComponentInChildren<UIStatBar>();
    }
    
    private void OnEnable() {
        PlayerStatus.PlayerHealthChangeEvent += SetPlayerHealthUI;
    }
    private void OnDisable() {
        PlayerStatus.PlayerHealthChangeEvent -= SetPlayerHealthUI;
    }

    public void SetPlayerHealthUI(float CurrentHP, float MaxHP)
    {
        healthText.text = CurrentHP.ToString() + " | " + MaxHP.ToString();
        healthBar.SetValue(CurrentHP, MaxHP);
        UItransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, MaxHP * 3);
    }
}
