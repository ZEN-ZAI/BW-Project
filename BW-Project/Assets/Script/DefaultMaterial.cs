using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMaterial : MonoBehaviour {

    private Material aDefaultMaterial;

    void Start()
    {
        aDefaultMaterial = transform.gameObject.GetComponent<Renderer>().sharedMaterial;
    }

    public void SetDefaultMaterial()
    {
        transform.gameObject.GetComponent<Renderer>().sharedMaterial = aDefaultMaterial;
    }

    public void ChangeShader(Material material)
    {
        transform.GetComponent<Renderer>().sharedMaterial = material;
    }
}
