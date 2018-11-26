using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Character : MonoBehaviour
{
    public int x;
    public int y;

    private int jumpPower = 5000;
    public string group;

    private bool walking;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (walking)
        {
            if (agent.velocity == Vector3.zero)
            {
                GameSystem.instance.moveCharacter = false;
                NetworkSystem.instance.UpdateColumn("state", "playing");
                walking = false;
            }
        }   
    }

    public void WalkToBlock(int target_x, int target_y)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up* jumpPower);
        agent.SetDestination(Map.instance.GetBlockPosition(target_x, target_y));
        walking = true;
        Map.instance.MoveCharacter(x,y, target_x, target_y);
        //transform.GetComponent<PlayerMotor>().MoveToPoint(Map.GetBlockPosition(index_x,index_y));
        //transform.SetPositionAndRotation(Map.instance.GetBlockPosition(target_x, target_y), Quaternion.identity);

        //transform.GetComponent<Rigidbody>().AddForce(Vector3.up* jumpPower);
        //transform.transform.Translate(Map.instance.GetBlockPosition(target_x, target_y));

        this.x = target_x;
        this.y = target_y;
    }

    public void ChangeGroup(string playerName,string skinName)
    {
        this.group = playerName;
        gameObject.GetComponent<SetMaterial>().SetNewMaterial(skinName);
    }

}
