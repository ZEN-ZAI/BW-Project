using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterial : MonoBehaviour {

    public Material defaultMaterial;

    void Start()
    {
        defaultMaterial = transform.gameObject.GetComponent<Renderer>().sharedMaterial;
    }

    public void SetDefaultMaterial()
    {
        transform.gameObject.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
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
        transform.GetComponent<Renderer>().sharedMaterial = material;
    }
}
