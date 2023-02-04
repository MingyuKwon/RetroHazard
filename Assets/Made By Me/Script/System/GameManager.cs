using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static event Action GameOver;

    PlayerMove player;
    PlayerAnimation playerAnimation;

    [Header("Difficulty")]
    public int DEFCON = 0;

    [Header("Slow Motion")]
    public bool isSlowMotion = false;
    public const float defalutSlowScale = 0.3f;
    public float slowMotionTimer = 0f;
    public const float defaultSlowMotionTime = 0.8f;
    public float slowMotionTime = 0.8f;

    [Header("Flag field")]
    public bool isPlayerNearNPC = false;
    public bool isPlayerSheilding = false;
    public bool Sheild_Durability_Reducing = false;
    public bool isPlayerPaused = false; // Every player script refer to this value 

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
        player = FindObjectOfType<PlayerMove>();
        playerAnimation = FindObjectOfType<PlayerAnimation>();
    }

    private void OnEnable() {
        PlayerShield.Sheild_Durability_Reduce_Start_Event += Set_Sheild_Durability_Reducing;
    }

    private void Set_Sheild_Durability_Reducing()
    {
        Sheild_Durability_Reducing = true;
    }

    private void Update() {

        if(isSlowMotion)
        {
            if(slowMotionTimer < slowMotionTime)
            {
                slowMotionTimer += (1f / slowMotionTime) * Time.unscaledDeltaTime;
            }
            else
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                isSlowMotion = false;
            }
        }else
        {
            slowMotionTimer = 0f;
        }
         
    }

    // dont move position while doing another action
    public void SetPlayerMove(bool flag)
    {
        player.canMove = flag;
    }

    public void SetPlayerAnimationIdle()
    {
        playerAnimation.SetPlayerAnimationIdle();

    }

    public void SetPlayerFree()
    {
        playerAnimation.SetPlayerFree();
    }

    public void SetPausePlayer(bool flag)
    {
        isPlayerPaused = flag;
    }

    //SlowMotion
    public void SlowMotion()
    {
        Time.timeScale = defalutSlowScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        this.slowMotionTime = defaultSlowMotionTime;
        isSlowMotion = true;
    }

    public void SlowMotion(float slowMotionTime)
    {
        Time.timeScale = defalutSlowScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        this.slowMotionTime = slowMotionTime;
        isSlowMotion = true;
    }
    //SlowMotion
}
