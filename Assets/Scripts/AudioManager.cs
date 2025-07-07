using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    //'clipNames' and 'clips' must have the same length and matching indices.
    public string[] clipNames;
    public AudioClip[] clips;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    private Dictionary<string, AudioClip> _audioClipMap;

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _audioClipMap = new Dictionary<string, AudioClip>();

        for (int i = 0; i < clipNames.Length; i++)
        {
            if (!_audioClipMap.ContainsKey(clipNames[i]))
            {
                _audioClipMap.Add(clipNames[i], clips[i]);
            }
        }
    }

    public void PlaySFX(string name, float volume = 1f)
    {
        if (_audioClipMap.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    public void PlayMusic(string name, float volume = 0.5f)
    {
        if (_audioClipMap.TryGetValue(name, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}