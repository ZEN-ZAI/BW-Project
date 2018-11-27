using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStore : MonoBehaviour
{
    public GameObject npc;
    public GameObject trump;
    public GameObject kim;
    public GameObject prayut;

    public GameObject trumpGuard;
    public GameObject kimGuard;
    public GameObject prayutGuard;

    public Sprite yourTeam;
    public Sprite enermyTeam;
    public Sprite npcTeam;

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
        DontDestroyOnLoad(gameObject);
    }
}
