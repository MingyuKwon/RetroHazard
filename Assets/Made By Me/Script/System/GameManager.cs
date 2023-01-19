using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    PokemonPlayerMove player;

    public int DEFCON = 0;
    public bool isPlayerNearNPC = false;
    public bool isPlayerPaused = false;

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

    void Start() {
        player = FindObjectOfType<PokemonPlayerMove>();
    }

    public void SetPlayerMove(bool flag)
    {
        player.canMove = flag;
    }

    public void SetPausePlayer(bool flag)
    {
        isPlayerPaused = flag;
    }
}
