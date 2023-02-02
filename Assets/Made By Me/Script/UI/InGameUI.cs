using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    Text PlayerHealth;
    private void Awake() {
        PlayerHealth = GetComponentInChildren<Text>();
    }

    private void OnEnable() {
        PlayerStatus.PlayerHealthChange += SetPlayerHealthUI;
    }

    public void SetPlayerHealthUI(float CurrentHP)
    {
        PlayerHealth.text = CurrentHP.ToString();
    }
}
