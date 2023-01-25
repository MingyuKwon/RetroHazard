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

    public bool isSlowMotion = false;
    public const float defalutSlowScale = 0.5f;
    public float slowMotionTimer = 0f;
    public const float defaultSlowMotionTime = 0.8f;
    public float slowMotionTime = 0.8f;

    public bool isPlayerNearNPC = false;
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
        player = FindObjectOfType<PokemonPlayerMove>();
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

    public void SetPlayerMove(bool flag)
    {
        player.canMove = flag;
    }

    public void SetPausePlayer(bool flag)
    {
        isPlayerPaused = flag;
    }

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
}
