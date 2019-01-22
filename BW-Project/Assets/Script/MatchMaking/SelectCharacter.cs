using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectCharacter : MonoBehaviour
{
    public Sprite Trump;
    public Sprite Prayut;
    public Sprite Kim;

    public void SelectedCharacter(string name)
    {
        Debug.Log("Select " + name);
        SoundStore.instance.PlayButtonSound();
        GameData.instance.myCharacterName = name;

        GameObject.Find("Dummy").GetComponent<SetMaterial>().SetNewMaterial(name);
        //GameObject.Find("Dummy Picture").GetComponent<Image>().sprite = GameObject.Find(name).GetComponent<Image>().sprite;

        if (name == "Trump")
        {
            GameObject.Find("Dummy Picture").GetComponent<Image>().sprite = Trump;
        }
        else if (name == "Prayut")
        {
            GameObject.Find("Dummy Picture").GetComponent<Image>().sprite = Prayut;
        }
        else if (name == "Kim")
        {
            GameObject.Find("Dummy Picture").GetComponent<Image>().sprite = Kim;
        }

        PanelControl.instance.MoveLeft();
    }
}
