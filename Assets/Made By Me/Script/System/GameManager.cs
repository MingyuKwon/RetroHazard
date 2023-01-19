using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    blackOut blackoutUI;
    DialogUI dialogUI;
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
        blackoutUI = FindObjectOfType<blackOut>();
        dialogUI = FindObjectOfType<DialogUI>();
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

    public void VisualizeDialogUI(bool flag)
    {
        dialogUI.VisualizeDialogUI(flag);
    }

    public void SetDialogText(string text)
    {
        dialogUI.SetDialogText(text);
    }
    public void SetSpeakerText(string text)
    {
        dialogUI.SetSpeakerText(text);
    }

    public void SetOptionsText(string[] texts)
    {
        dialogUI.SetOptionsText(texts);
        
    }

    public void showOptionUI(bool flag)
    {
        dialogUI.showOptionUI(flag);
    }

    public void SelectOption(int index)
    {
        dialogUI.SelectOption(index);
    }
}
