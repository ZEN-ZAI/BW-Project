using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterial : MonoBehaviour
{

    public Material defaultMaterial;

    void Start()
    {
        if (gameObject.layer == LayerMask.GetMask("Tile"))
        {
            defaultMaterial = transform.gameObject.GetComponent<Renderer>().sharedMaterial;
        }
        else if (gameObject.layer == LayerMask.GetMask("Character"))
        {
            defaultMaterial = FindObjectOfType<Renderer>().sharedMaterial;
        }
    }

    public void SetDefaultMaterial()
    {

        if (gameObject.layer == LayerMask.GetMask("Tile"))
        {
            transform.gameObject.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
        }
        else if (gameObject.layer == LayerMask.GetMask("Character"))
        {
            foreach (Material mat in FindObjectOfType<Renderer>().sharedMaterials)
            {
                mat.mainTexture = defaultMaterial.mainTexture;
            }
        }
    }

    public void SetNewMaterial(string material)
    {
        if (material == "Prayut")
        {
            defaultMaterial = CharacterStore.instance.prayut.GetComponentInChildren<Renderer>().sharedMaterial;

            foreach (Material mat in FindObjectOfType<Renderer>().sharedMaterials)
            {
                mat.mainTexture = CharacterStore.instance.prayut.GetComponentInChildren<Renderer>().sharedMaterial.mainTexture;
            }

        }
        else if (material == "Trump")
        {
            defaultMaterial = CharacterStore.instance.trump.GetComponentInChildren<Renderer>().sharedMaterial;

            foreach (Material mat in FindObjectOfType<Renderer>().sharedMaterials)
            {
                mat.mainTexture = CharacterStore.instance.trump.GetComponentInChildren<Renderer>().sharedMaterial.mainTexture;
            }
        }
        else if (material == "Kim")
        {
            defaultMaterial = CharacterStore.instance.kim.GetComponentInChildren<Renderer>().sharedMaterial;

            foreach (Material mat in FindObjectOfType<Renderer>().sharedMaterials)
            {
                mat.mainTexture = CharacterStore.instance.kim.GetComponentInChildren<Renderer>().sharedMaterial.mainTexture;
            }
        }
    }

    public void SetNewMaterial(Material material)
    {

        if (gameObject.layer == LayerMask.GetMask("Tile"))
        {
            transform.GetComponent<Renderer>().sharedMaterial = material;
        }
        else if (gameObject.layer == LayerMask.GetMask("Character"))
        {
            foreach (Material mat in FindObjectOfType<Renderer>().sharedMaterials)
            {
                mat.mainTexture = material.mainTexture;
            }
        }
    }

    public void Highlight()
    {
        if (gameObject.GetComponent<Character>() != null)
        {
            foreach (Material mat in FindObjectOfType<Renderer>().sharedMaterials)
            {
                mat.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
            }
        }
        else
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
        }


    }


    public void HighlightOutline()
    {
        if (gameObject.GetComponent<Character>() != null)
        {
            foreach (Material mat in FindObjectOfType<Renderer>().sharedMaterials)
            {
                mat.shader = Shader.Find("Outlined/UltimateOutline");
            }
        }
        else
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Outlined/UltimateOutline");
        }


    }


    public void UnHighlight()
    {
        if (gameObject.GetComponent<Character>() != null)
        {
            foreach (Material mat in FindObjectOfType<Renderer>().sharedMaterials)
            {
                mat.shader = Shader.Find("Diffuse");
            }
        }
        else
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
        }


    }

}
