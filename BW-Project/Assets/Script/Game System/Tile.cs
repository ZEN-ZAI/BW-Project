using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int row;
    public int col;

    public int pathLevel;
    
    public bool visited;

    public GameObject character;

    void Update()
    {
        //path finder
        if (visited)
        {
            tag = "Path";
        }
        else
        {
            tag = "Tile";
        }


        if (HaveCharacter())
        {

        }
    }

    public bool HaveCharacter()
    {
        if (character == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void LinkCharacter(GameObject character)
    {
        this.character = character;
    }

    public void UnLinkCharacter()
    {
        this.character = null;
    }

    public void DestroyCharacter()
    {
        Destroy(character);
        character = null;
    }
    
    void OnMouseOver()
    {
        GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
    }

    void OnMouseExit()
    {
        if (!Player.instance.selectCharecter)
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
        }
    }
}
