using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player singleTon 역할도 하지만, status, inventory, itemBox 참조를 제공해 주기도 한다
public class Player1 : MonoBehaviour
{
    public static Player1 instance;
    public PlayerStatus playerStatus;
    public PlayerInventory playerInventory;
    public PlayerItemBox playerItemBox;

    public PlayerAnimation playerAnimation;

    private void Awake() {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        playerStatus = GetComponentInChildren<PlayerStatus>();
        playerInventory = GetComponentInChildren<PlayerInventory>();
        playerItemBox = GetComponentInChildren<PlayerItemBox>();
        
        playerAnimation = GetComponent<PlayerAnimation>();
    }
}
