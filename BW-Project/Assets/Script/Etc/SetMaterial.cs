using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterial : MonoBehaviour {

    public Material defaultMaterial;

    void Start()
    {
        if (gameObject.layer == LayerMask.GetMask("Tile"))
        {
            defaultMaterial = transform.gameObject.GetComponent<Renderer>().sharedMaterial;
        }
        else if(gameObject.layer == LayerMask.GetMask("Character"))
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
            FindObjectOfType<Renderer>().sharedMaterial = defaultMaterial;
        }
    }

    public void SetNewMaterial(string material)
    {
        if (material == "Prayut")
        {
            defaultMaterial = CharacterStore.instance.prayut.GetComponent<Renderer>().sharedMaterial;
            transform.GetComponent<Renderer>().sharedMaterial = CharacterStore.instance.prayut.GetComponent<Renderer>().sharedMaterial;
        }
        else if (material == "Trump")
        {
            defaultMaterial = CharacterStore.instance.trump.GetComponent<Renderer>().sharedMaterial;
            transform.GetComponent<Renderer>().sharedMaterial = CharacterStore.instance.trump.GetComponent<Renderer>().sharedMaterial;
        }
        else if (material == "Kim")
        {
            defaultMaterial = CharacterStore.instance.kim.GetComponent<Renderer>().sharedMaterial;
            transform.GetComponent<Renderer>().sharedMaterial = CharacterStore.instance.kim.GetComponent<Renderer>().sharedMaterial;
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
            FindObjectOfType<Renderer>().sharedMaterial = material;
        }
    }
}
