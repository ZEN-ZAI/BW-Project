using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectCharacter : MonoBehaviour
{
    public void SelectedCharacter(string name)
    {
        Debug.Log("Select " + name);
        SoundStore.instance.PlayButtonSound();
        GameData.instance.myCharacterName = name;

        GameObject.Find("Dummy").GetComponent<SetMaterial>().SetNewMaterial(name);
        GameObject.Find("Dummy Picture").GetComponent<Image>().sprite = GameObject.Find(name).GetComponent<Image>().sprite;
        
        PanelControl.instance.MoveLeft();
    }
}
