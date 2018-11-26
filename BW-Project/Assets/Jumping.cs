using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jumping : MonoBehaviour
{
    public static Jumping instance;

     OffMeshLink offMeshLink;

    private void Awake()
    {
        instance = this;
    }

    public void SetJump(Transform start, Transform end)
    {
        GetComponent<OffMeshLink>().startTransform = start;
        GetComponent<OffMeshLink>().endTransform = end;
        GetComponent<OffMeshLink>().UpdatePositions();
        GetComponent<OffMeshLink>().activated = true;
    }
}
