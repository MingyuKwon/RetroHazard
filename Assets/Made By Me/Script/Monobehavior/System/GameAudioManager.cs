using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AudioType{
    BackGround = 0,
    sfx = 1,
    ui = 2,
}

public enum BackGroundAudioType{
    Adeenhau_beach = 0,
    Lasil_archaeological_site = 1,
}

public class GameAudioManager : MonoBehaviour
{
    static public GameAudioManager instance;

    static AudioSource[] backGroundSources;
    static AudioSource[] sfxSource;
    static AudioSource[] UISource;

    static AudioSource CurrentBackGroundSources;

    static AudioSource BackGroundSources1 = null;
    static AudioSource BackGroundSources2 = null;

    static AudioSource CurrentSfxSource;
    static AudioSource CurrentUISource;

    public static void PlayBackGroundMusic(BackGroundAudioType audioType)
    {
        backGroundSources[(int)audioType]
    }

    public static void PlaySFXMusic()
    {

    }

    public static void PlayUIMusic()
    {

    }

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        backGroundSources = transform.GetChild(0).GetComponentsInChildren<AudioSource>();
        sfxSource = transform.GetChild(1).GetComponentsInChildren<AudioSource>();
        UISource = transform.GetChild(2).GetComponentsInChildren<AudioSource>();
    }

    void Start()
    {

    }

}
