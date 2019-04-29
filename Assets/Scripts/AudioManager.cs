using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer;
    [Header("Pause Music Parameters")]
    [Header("Music Cutoff")]
    public float playCutoffFreq;
    public float pauseCutoffFreq;
    [Header("Music Volume")]
    public float playVolume;
    public float pauseVolume;
    public float fadeDuration;

    [Header("New Game music crossfade")]
    public float crossFadeDuration;

    [Header("Sound List")]
    public Sound[] sounds;

    [HideInInspector] public bool appearingAnimationIsFinished;
    [HideInInspector] public bool disapearingLevelAnimationIsFinished;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //for using actual sfx with correct names
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.multipleClips = new AudioClip[s.clipsNumber];
            if (!s.randomSounds && !s.followingSounds)
            {
                s.clip = Resources.Load<AudioClip>("Audio/" + s.name);
            }
            else if (s.randomSounds || s.followingSounds)
            {
                for (int i = 0; i < s.multipleClips.Length; i++)
                {
                    s.multipleClips[i] = Resources.Load<AudioClip>("Audio/" + s.name + " " + i);
                    if (s.multipleClips[i] == null)
                    {
                        Debug.LogWarning("Sound: " + s.name + " " + i + " not found");
                        return;
                    }
                }
            }
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

        if (playCutoffFreq <= pauseCutoffFreq)
        {
            playCutoffFreq = 22000;
            pauseCutoffFreq = 550;
        }
        if (playVolume <= pauseVolume)
        {
            playVolume = 0;
            pauseVolume = -5;
        }

        Play("menu music");
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        if (!s.randomSounds && !s.followingSounds)
        {
            s.source.Play();
        }
        else if (s.randomSounds)
        {
            s.rndIndex = UnityEngine.Random.Range(0, s.multipleClips.Length);
            s.source.clip = s.clip = s.multipleClips[s.rndIndex];
            if (s.source.clip == null)
            {
                Debug.LogWarning("A sound must be missing in " + s.name);
                s.source.clip = s.clip = s.multipleClips[0];
            }
            s.source.Play();
        }
        else if (s.followingSounds)
        {
            s.source.clip = s.clip = s.multipleClips[s.currentIndex];
            if (s.source.clip == null)
            {
                Debug.LogWarning("A sound must be missing in " + s.name);
                s.source.clip = s.clip = s.multipleClips[0];
            }
            if (s.currentIndex == s.multipleClips.Length - 1)
            {
                Debug.Log(s.currentIndex);
                s.currentIndex = 0;
            }
            else
                s.currentIndex++;
            s.source.Play();
        }
    }

    public void PlayUnique(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        if (!s.randomSounds)
        {
            if (!s.source.isPlaying)
                s.source.Play();
        }
        else
        {
            s.rndIndex = UnityEngine.Random.Range(0, s.multipleClips.Length);
            s.source.clip = s.clip = s.multipleClips[s.rndIndex];
            if (s.source.clip == null)
            {
                Debug.LogWarning("A sound must be missing in " + s.name);
                s.source.clip = s.clip = s.multipleClips[0];
            }
            if (!s.source.isPlaying)
                s.source.Play();
        }
    }


    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }


    public void LoadArenaFromMenu()
    {
        StartCoroutine(MusicCrossFade(audioMixer, "MusicMenuVolume", "MusicArenaVolume", crossFadeDuration, "menu music", "ig music arena"));
    }
    public void LoadShopFromArena()
    {
        StartCoroutine(MusicCrossFade(audioMixer, "MusicArenaVolume", "MusicShopVolume", crossFadeDuration, "ig music arena", "ig music merchant"));
        StartCoroutine(MusicCrossFade(audioMixer, "AmbianceArenaVolume", "AmbianceShopVolume", crossFadeDuration, "ig crowd ambiance", "ig merchant ambiance"));
        Play("ig merchant say hello");
    }
    public void QuitShop()
    {
        Play("ig merchant say goodbye");
    }
    public void LoadArenaFromShop()
    {
        StartCoroutine(MusicCrossFade(audioMixer, "MusicShopVolume", "MusicArenaVolume", crossFadeDuration, "ig music merchant", "ig music arena"));
        StartCoroutine(MusicCrossFade(audioMixer, "AmbianceShopVolume", "AmbianceArenaVolume", crossFadeDuration, "ig merchant ambiance", "ig crowd ambiance"));
    }




    public IEnumerator MusicCrossFade(AudioMixer audioMixer, string fadeOutParamName, string fadeInParamName, float crossFadeDuration, string fadedOutMusicName, string fadeInMusicName)
    {
        float currentTime = 0f;
        float normalizedValue;
        Play(fadeInMusicName);
        while (currentTime <= crossFadeDuration)
        {
            currentTime += Time.unscaledDeltaTime;
            normalizedValue = currentTime / crossFadeDuration;
            audioMixer.SetFloat(fadeOutParamName, Mathf.Lerp(0f, -80f, normalizedValue));
            audioMixer.SetFloat(fadeInParamName, Mathf.Lerp(-80f, 0f, normalizedValue));
            yield return null;
        }
        audioMixer.SetFloat(fadeOutParamName, -80f);
        audioMixer.SetFloat(fadeInParamName, 0f);
        Stop(fadedOutMusicName);
        yield return null;
    }
}
