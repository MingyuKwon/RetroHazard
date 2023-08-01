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
    YangHan_Village = 5,
    Spooky_House = 6,
    Guide_Center = 7,
    Cave = 8,
}

public enum SFXAudioType{
    noramlAttack = 0,  
    Energy1Attack = 1, 
    Energy2Attack = 2, 
    Energy3Attack = 3, 
    Attacked = 4,  
    GetItem = 5,  
    Parry = 6,  
    ParrySuccess = 7,  
    reload = 8,
    HpUp = 9,
    Block = 10,
}

public enum UIAudioType{
    Click = 0,
    Back = 1,
    Move = 2,
    Open = 3,    
}

public class GameAudioManager : MonoBehaviour
{
    public class LanguageManager{
        public static string currentLanguage = "E";
        public static Action<string> languageChangeEvent;
        public static void Invoke_languageChangeEvent(string language)
        {
            currentLanguage = language;
            languageChangeEvent?.Invoke(language);
            PlayerPrefs.SetString("language", language);
        }
    }

    public class LoadManager{
        public static string nextSceneName;
    }

    public class AudioEvent{
        static public void Invoke_updateTotal()
        {
            currentBackGroundVolume = currentBackGroundVolume;
            currentUIVolume = currentUIVolume;
            currentSFXVolume = currentSFXVolume;
            currentEnvironmentVolume = currentEnvironmentVolume;
        }

        static public event Action updateEnemyVolume;
        static public void Invoke_updateEnemyVolume()
        {
            updateEnemyVolume?.Invoke();
        }

        static public event Action updateEnvironmentVolume;
        static public void Invoke_updateEnvironmentVolume()
        {
            updateEnvironmentVolume?.Invoke();
        }
    }

    static public GameAudioManager instance;

    static public float totalVolme{
        get{
            return _totalVolme;
        }

        set{
            _totalVolme = value;
            PlayerPrefs.SetFloat("totalVolme", _totalVolme);
            PlayerPrefs.Save();

            if(isAwaking) return;

            AudioEvent.Invoke_updateTotal();
        }
    }
    static public float currentBackGroundVolume{
        get{
            return _currentBackGroundVolume;
        }

        set{
            _currentBackGroundVolume = value;
            _currentBackGroundVolume = Mathf.Clamp(_currentBackGroundVolume, 0,1);
            PlayerPrefs.SetFloat("currentBackGroundVolume", _currentBackGroundVolume);
            PlayerPrefs.Save();

            if(isAwaking) return;

            if(isNowChanging)
            {       
                if(isNowChangingNum == 1)
                {
                    BackGroundSources1.volume = _currentBackGroundVolume* totalVolme;
                }else if(isNowChangingNum == 2)
                {
                    BackGroundSources2.volume = _currentBackGroundVolume* totalVolme;
                }
            }else
            {
                if(BackGroundSources1.isPlaying)
                {
                    BackGroundSources1.volume = _currentBackGroundVolume* totalVolme;
                }else if(BackGroundSources2.isPlaying)
                {
                    BackGroundSources2.volume = _currentBackGroundVolume* totalVolme;
                }
            }

        }
    }
    static public float currentUIVolume{
        get{
            return _currentUIVolume;
        }

        set{
            _currentUIVolume = value;
            _currentUIVolume = Mathf.Clamp(_currentUIVolume, 0,1);
            PlayerPrefs.SetFloat("currentUIVolume", _currentUIVolume);
            PlayerPrefs.Save();

            if(isAwaking) return;

            CurrentUISource.volume = _currentUIVolume * totalVolme;
        }
    }
    static public float currentSFXVolume{
        get{
            return _currentSFXVolume;
        }

        set{
            _currentSFXVolume = value;
            _currentSFXVolume = Mathf.Clamp(_currentSFXVolume, 0,1);
            PlayerPrefs.SetFloat("currentSFXVolume", _currentSFXVolume);
            PlayerPrefs.Save();

            if(isAwaking) return;
            CurrentSfxSource.volume = _currentSFXVolume * totalVolme;
            AudioEvent.Invoke_updateEnemyVolume();
        }
    } 
    static public float currentEnvironmentVolume{
        get{
            return _currentEnvironmentVolume;
        }

        set{
            _currentEnvironmentVolume = value;
             _currentEnvironmentVolume = Mathf.Clamp(_currentEnvironmentVolume, 0,1);
            PlayerPrefs.SetFloat("currentEnvironmentVolume", _currentEnvironmentVolume);
            PlayerPrefs.Save();

            if(isAwaking) return;

            AudioEvent.Invoke_updateEnvironmentVolume();
        }
    } 

    static float _totalVolme;
    static float _currentBackGroundVolume;
    static float _currentUIVolume;
    static float _currentSFXVolume; 
    static float _currentEnvironmentVolume;

    public static float default_totalVolme = 1f;
    public static float default_currentBackGroundVolume = 0.5f;
    public static float default_currentUIVolume = 0.3f;
    public static float default_currentSFXVolume = 0.5f;
    public static float default_currentEnvironmentVolume = 0.7f;

    public AudioClip[] backGroundAudioClip;
    public AudioClip[] sfxAudioClip;
    public AudioClip[] UIAudioClip;

    static bool isNowChanging = false;
    static AudioSource BackGroundSources1 = null; // 이 2개 옮겨 다니면서 연속적인 배경음악 바꾸기를 할 것이다
    static AudioSource BackGroundSources2 = null;

    static AudioSource CurrentSfxSource;
    static AudioSource CurrentUISource;

    static Stack<AudioClip> playRequestStack;


    static bool isAwaking = true;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
            return;
        }

        if (PlayerPrefs.HasKey("totalVolme"))
        {
            totalVolme = PlayerPrefs.GetFloat("totalVolme");
        }
        else
        {
            totalVolme = default_totalVolme;
            PlayerPrefs.SetFloat("totalVolme", totalVolme);
        }

        if (PlayerPrefs.HasKey("currentBackGroundVolume"))
        {
            currentBackGroundVolume = PlayerPrefs.GetFloat("currentBackGroundVolume");
        }
        else
        {
            currentBackGroundVolume = default_currentBackGroundVolume;
            PlayerPrefs.SetFloat("currentBackGroundVolume", currentBackGroundVolume);
        }

        if (PlayerPrefs.HasKey("currentUIVolume"))
        {
            currentUIVolume = PlayerPrefs.GetFloat("currentUIVolume");
        }
        else
        {
            currentUIVolume = default_currentUIVolume;
            PlayerPrefs.SetFloat("currentUIVolume", currentUIVolume);
        }

        if (PlayerPrefs.HasKey("currentSFXVolume"))
        {
            currentSFXVolume = PlayerPrefs.GetFloat("currentSFXVolume");
        }
        else
        {
            currentSFXVolume = default_currentSFXVolume;
            PlayerPrefs.SetFloat("currentSFXVolume", currentSFXVolume);
        }

        if (PlayerPrefs.HasKey("currentEnvironmentVolume"))
        {
            currentEnvironmentVolume = PlayerPrefs.GetFloat("currentEnvironmentVolume");
        }
        else
        {
            currentEnvironmentVolume = default_currentEnvironmentVolume;
            PlayerPrefs.SetFloat("currentEnvironmentVolume", currentEnvironmentVolume);
        }

        PlayerPrefs.Save();

        isAwaking = false;

        playRequestStack = new Stack<AudioClip>();

        AudioSource[] temp = GetComponents<AudioSource>();

        BackGroundSources1 = temp[0];
        BackGroundSources2 = temp[1];
        BackGroundSources1.loop = true;
        BackGroundSources2.loop = true;

        CurrentSfxSource = temp[2];
        CurrentUISource = temp[3];
    }

    public void PlayBackGroundMusic(BackGroundAudioType audioType)
    {
        AudioClip targetAudioClip = backGroundAudioClip[(int)audioType];
        playRequestStack.Push(targetAudioClip);

        if(isNowChanging){
            return;
        }

        playBackGroundMusic();

    }

    static int isNowChangingNum = 1;
    private void playBackGroundMusic()
    {
        isNowChanging = true;

        AudioClip targetAudioClip = playRequestStack.Pop();
        playRequestStack.Clear();
        if(BackGroundSources1.isPlaying) // 현재 1번창을 틀고 있고, 이제 2번으로 바꿔야 할 차례
        {
            isNowChangingNum = 2;
            StartCoroutine(musicChange(BackGroundSources1 , BackGroundSources2, targetAudioClip));
        }else if(BackGroundSources2.isPlaying)
        {
            isNowChangingNum = 1;
            StartCoroutine(musicChange(BackGroundSources2 , BackGroundSources1, targetAudioClip));
        }else // 제일 초기 상태
        {
            BackGroundSources1.clip = targetAudioClip;
            BackGroundSources1.volume = currentBackGroundVolume * totalVolme;
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
            beforeAudioSource.volume = currentBackGroundVolume * totalVolme * Mathf.Cos(Mathf.PI * 0.5f * timeElaped);
            afterAudioSource.volume = currentBackGroundVolume * totalVolme * Mathf.Sin(Mathf.PI * 0.5f * timeElaped);
            yield return new WaitForSecondsRealtime(0.05f);
            timeElaped += 0.05f;
        }

        beforeAudioSource.Stop();

        isNowChanging = false;
    }

    public void PlaySFXMusic(SFXAudioType audioType)
    {
        CurrentSfxSource.clip = sfxAudioClip[(int)audioType];
        CurrentSfxSource.volume = currentSFXVolume * totalVolme;
        CurrentSfxSource.Play();
    }

    public void PlayUIMusic(UIAudioType audioType)
    {
        CurrentUISource.clip = UIAudioClip[(int)audioType];
        CurrentUISource.volume = currentUIVolume * totalVolme;
        CurrentUISource.Play();
    }


}
