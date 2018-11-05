using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DebugConsole))]
public class DebugControl : MonoBehaviour
{

    private DebugConsole debugConsole;
    //public GameObject debugPanel;

    // Use this for initialization
    void Start()
    {
        debugConsole = GetComponent<DebugConsole>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8) && debugConsole.enabled)
        {
            debugConsole.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.F8) && !debugConsole.enabled)
        {
            debugConsole.enabled = true;
        }
    }
}
