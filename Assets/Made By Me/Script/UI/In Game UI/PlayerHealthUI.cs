using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    Text healthText;

    private void Awake() {
        healthText = GetComponent<Text>();
    }

    private void Start() {
        healthText.text =  FindObjectOfType<PlayerStatus>().CurrentHP.ToString();
    }
    private void OnEnable() {
        PlayerStatus.PlayerHealthChangeEvent += SetPlayerHealthUI;
    }

    public void SetPlayerHealthUI(float CurrentHP)
    {
        healthText.text = CurrentHP.ToString();
    }
}
