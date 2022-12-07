using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null) Debug.LogError("AudioManager is null");

            return _instance;
        }
    }

    public Sound[] sounds;
    public Sound[] quacks;

    private void Awake()
    {
        _instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume; 
            s.source.pitch = s.pitch;
            s.source.loop = s.loop; 

        }
        foreach (Sound s in quacks)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }

        DontDestroyOnLoad(gameObject);
        Play("MenuThemeMixed");
        Play("Quack1");

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void RandomQuack()
    {
        int index = UnityEngine.Random.Range(0, quacks.Length);
        Debug.Log($"Playing quack sound: {quacks[index].name}");
        Play(quacks[index].name);
    }
}
