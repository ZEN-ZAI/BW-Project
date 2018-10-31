using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterial : MonoBehaviour {

    public Material defaultMaterial;

    void Start()
    {
        defaultMaterial = transform.gameObject.GetComponent<Renderer>().sharedMaterial;
        /*
        prayutMaterial = Spawner.instance.CharacterPrayut.GetComponent<Renderer>().sharedMaterial;
        trumpMaterial = Spawner.instance.CharacterTrump.GetComponent<Renderer>().sharedMaterial;
        kimMaterial = Spawner.instance.CharacterKim.GetComponent<Renderer>().sharedMaterial;
        */
    }

    public void SetDefaultMaterial()
    {
        transform.gameObject.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
    }

    public void SetNewMaterial(string material)
    {
        if (material == "Prayut")
        {
            defaultMaterial = Spawner.instance.CharacterPrayut.GetComponent<Renderer>().sharedMaterial; ;
            transform.GetComponent<Renderer>().sharedMaterial = Spawner.instance.CharacterPrayut.GetComponent<Renderer>().sharedMaterial;
        }
        else if (material == "Trump")
        {
            defaultMaterial = Spawner.instance.CharacterTrump.GetComponent<Renderer>().sharedMaterial;
            transform.GetComponent<Renderer>().sharedMaterial = Spawner.instance.CharacterTrump.GetComponent<Renderer>().sharedMaterial;
        }
        else if (material == "Kim")
        {
            defaultMaterial = Spawner.instance.CharacterKim.GetComponent<Renderer>().sharedMaterial;
            transform.GetComponent<Renderer>().sharedMaterial = Spawner.instance.CharacterKim.GetComponent<Renderer>().sharedMaterial;
        }

    }

    public void SetNewMaterial(Material material)
    {
        transform.GetComponent<Renderer>().sharedMaterial = material;
    }
}
