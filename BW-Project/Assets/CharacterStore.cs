using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStore : MonoBehaviour
{
    public GameObject trump;
    public GameObject kim;
    public GameObject prayut;

    public static CharacterStore instance;

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
}
