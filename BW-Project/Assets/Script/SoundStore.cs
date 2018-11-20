using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStore : MonoBehaviour
{
    public AudioClip button;
    public AudioClip TH;
    public AudioClip NK;
    public AudioClip USA;

    public static SoundStore instance;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    public void StopSound()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void PlayButtonSound()
    {
        GetComponent<AudioSource>().PlayOneShot(button);
    }

    public void PlayTH()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(TH);
    }

    public void PlayNK()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(NK);
    }

    public void PlayUSA()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(USA);
    }
}
