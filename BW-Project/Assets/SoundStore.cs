using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStore : MonoBehaviour
{

    private AudioSource audioSource;
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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(button);
    }

    public void PlayTH()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(TH);
    }

    public void PlayNK()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(NK);
    }

    public void PlayUSA()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(USA);
    }
}
