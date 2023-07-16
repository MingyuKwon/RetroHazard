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
    MainMenu = 0,
    Adeenhau_beach = 1,
    Lasil_archaeological_site = 2,
    House_theme1 = 3,
    House_theme2 = 4,
}

public enum UIAudioType{
    Click = 0,
    Back = 1,
    Move = 2,
    Open = 3,    
}

public class GameAudioManager : MonoBehaviour
{
    static public GameAudioManager instance;
    static public float currentBackGroundVolume = 0.5f;
    static public float currentSFXVolume = 0.5f;
    static public float currentUIVolume = 0.5f;

    public AudioClip[] backGroundAudioClip;
    public AudioClip[] sfxAudioClip;
    public AudioClip[] UIAudioClip;

    bool isNowChanging = false;
    AudioSource BackGroundSources1 = null; // 이 2개 옮겨 다니면서 연속적인 배경음악 바꾸기를 할 것이다
    AudioSource BackGroundSources2 = null;

    AudioSource CurrentSfxSource;
    AudioSource CurrentUISource;

    static Stack<AudioClip> playRequestStack;

    public void PlayBackGroundMusic(BackGroundAudioType audioType)
    {
        AudioClip targetAudioClip = backGroundAudioClip[(int)audioType];
        playRequestStack.Push(targetAudioClip);

        if(isNowChanging){
            return;
        }

        playBackGroundMusic();

    }

    private void playBackGroundMusic()
    {
        isNowChanging = true;

        AudioClip targetAudioClip = playRequestStack.Pop();
        playRequestStack.Clear();
        if(BackGroundSources1.isPlaying) // 현재 1번창을 틀고 있고, 이제 2번으로 바꿔야 할 차례
        {
            StartCoroutine(musicChange(BackGroundSources1 , BackGroundSources2, targetAudioClip));
        }else if(BackGroundSources2.isPlaying)
        {
            StartCoroutine(musicChange(BackGroundSources2 , BackGroundSources1, targetAudioClip));
        }else // 제일 초기 상태
        {
            BackGroundSources1.clip = targetAudioClip;
            BackGroundSources1.volume = currentBackGroundVolume;
            BackGroundSources1.Play();
            isNowChanging = false;
        }    
    }

    private void Update() {
        if(isNowChanging)
        {
            return;
        }

        if(playRequestStack.Count == 0)
        {
            return;
        }

        playBackGroundMusic();
    }

    IEnumerator musicChange(AudioSource beforeAudioSource , AudioSource afterAudioSource, AudioClip targetAudioClip)
    {
        isNowChanging = true;

        afterAudioSource.clip = targetAudioClip;
        afterAudioSource.volume = 0;
        afterAudioSource.Play();

        float timeElaped = 0;
        while(timeElaped < 1f)
        {
            beforeAudioSource.volume = currentBackGroundVolume * Mathf.Cos(Mathf.PI * 0.5f * timeElaped);
            afterAudioSource.volume = currentBackGroundVolume * Mathf.Sin(Mathf.PI * 0.5f * timeElaped);
            yield return new WaitForSecondsRealtime(0.05f);
            timeElaped += 0.05f;
        }

        beforeAudioSource.Stop();

        isNowChanging = false;
    }

    public void PlaySFXMusic()
    {

    }

    public void PlayUIMusic(UIAudioType audioType)
    {
        CurrentUISource.clip = UIAudioClip[(int)audioType];
        CurrentUISource.volume = currentUIVolume;
        CurrentUISource.Play();
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

        playRequestStack = new Stack<AudioClip>();

        AudioSource[] temp = GetComponents<AudioSource>();

        BackGroundSources1 = temp[0];
        BackGroundSources2 = temp[1];
        BackGroundSources1.loop = true;
        BackGroundSources2.loop = true;

        CurrentSfxSource = temp[2];
        CurrentUISource = temp[3];
    }
}
