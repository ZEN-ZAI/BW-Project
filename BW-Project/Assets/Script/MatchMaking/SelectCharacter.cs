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

        GameObject.Find("Dummy Picture").GetComponent<Image>().sprite = GameObject.Find(name).GetComponent<Image>().sprite;
        GameObject.Find("Dummy").GetComponent<SetMaterial>().SetNewMaterial(name);
        GameData.instance.myCharacterName = name;
        
        PanelControl.instance.MoveLeft();
    }
}
