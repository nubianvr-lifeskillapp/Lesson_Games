using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;



[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    
    [Range(0f, 1f)] public float volume = .75f;
    public bool playOnAwake = false;
    public bool loop = false;
    [HideInInspector] 
    public AudioSource source;
}
public class SoundManager : MonoBehaviour
{
    
    public static SoundManager soundManager;
    public Sound[] genericSounds;
    public Sound[] SFX;
    public Sound[] reactionSounds;

    private AudioSource[] audioSource;
    void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        SetAudioSourceParameters();
    }

    private void SetAudioSourceParameters()
    {
        foreach (Sound s in genericSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.playOnAwake = s.playOnAwake;
        }
        foreach (Sound s in SFX)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.playOnAwake = s.playOnAwake;
        }
        foreach (Sound s in reactionSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    private void Start()
    {
        
    }
  
    public void PlayGenericSound(string sound)
    {
        Sound s = Array.Find(genericSounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void PlaySFX(string sound)
    {
        Sound s = Array.Find(SFX, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void PlayReactionSound()
    {
        float r = UnityEngine.Random.Range(0, reactionSounds.Length);
        Debug.Log("Random Audio Index: " +  r);
        Sound s = reactionSounds[Mathf.FloorToInt(r)];

        s.source.Play();
    }

    public void PlayDescriptiveSound(ImageObject_S imageObject)
    {
        if(imageObject.descriptionAudio.source == null)
        {
            imageObject.descriptionAudio.source = gameObject.AddComponent<AudioSource>();
            imageObject.descriptionAudio.source.clip = imageObject.descriptionAudio.clip;
            imageObject.descriptionAudio.source.loop = imageObject.descriptionAudio.loop;
            imageObject.descriptionAudio.source.volume = imageObject.descriptionAudio.volume;
        }
        imageObject.descriptionAudio.source.Play();
    }

    public void StopGenericSound(string sound)
    {
        Sound s;
        s = Array.Find(genericSounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void StopSFX(string sound)
    {
        Sound s;
        s = Array.Find(SFX, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void StopReactionSound(string sound)
    {
        Sound s;
        s = Array.Find(reactionSounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void PauseGenericSound(string sound)
    {
        Sound s = Array.Find(genericSounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Pause();
    }

    public void PauseSFX(string sound)
    {
        Sound s = Array.Find(SFX, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Pause();
    }

    public void PauseReactionSound(string sound)
    {
        Sound s = Array.Find(reactionSounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Pause();
    }

    public void StopAllGenericSounds()
    {
        foreach (var s in genericSounds)
        {
            s.source.Stop();
        }
    }

    public void StopAllSFX()
    {
        foreach (var s in SFX)
        {
            s.source.Stop();
        }
    }

    public void StopAllReactionSounds()
    {
        foreach (var s in reactionSounds)
        {
            s.source.Stop();
        }
    }
}
