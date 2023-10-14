using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    
    
    [System.Serializable]
    public class Sounds
    {
        public string name;
        public AudioClip clip;
        public bool isPlaying;

        AudioSource source;
        
        public void SetSource(AudioSource _source)
        {
            source = _source;
            source.clip = clip;
        }

        public void Play()
        {
            source.Play();
            isPlaying = true;
        }
        public void Stop()
        {
            source.Stop();
            isPlaying = false;
        }
        public void PlayOneShot()
        {
            source.PlayOneShot(source.clip);
        }
        public bool CurrentlyPlaying()
        {
            return source.isPlaying;
        }
        public void PlayLooped()
        {
            source.loop = true;
            isPlaying = true;
            source.Play();
        }


    }

    [SerializeField]
    Sounds[] sound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        for(int i = 0; i<sound.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + sound[i].name);
            _go.transform.SetParent(this.transform);
            sound[i].SetSource(_go.AddComponent<AudioSource>());
        }
        PlaySound("OST");
    }

    public void PlaySound(string Name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].name == Name)
            {
                sound[i].Play();
                return;
            }
        }
    }
    public void PlayOneShot(string Name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].name == Name)
            {
                sound[i].PlayOneShot();
                return;
            }
        }
    }
    public bool IsPlaying(string Name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].name == Name)
            {

                return sound[i].CurrentlyPlaying();
            }
        }
        return false;
    }
    public void StopSound(string Name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].name == Name)
            {
                sound[i].Stop();
                return;
            }
        }
    }
    public void PlayLooped(string Name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].name == Name)
            {
                sound[i].PlayLooped();
                return;
            }
        }
    }
}
