using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int x;
    public int y;
    public string group;

    public int speed;
    public int jumpPower;
    
    private bool walking;
    public Vector3 target;
    

    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
        }

        if (walking)
        {

            if (transform.position == target)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GameSystem.instance.moveCharacter = false;
                NetworkSystem.instance.UpdateColumn("state", "playing");
                walking = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }   
    }

    public void WalkToBlock(int target_x, int target_y)
    {
        target = Map.instance.GetBlockPosition(target_x, target_y);
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
        walking = true;

        this.x = target_x;
        this.y = target_y;
    }

    public void ChangeGroup(string playerName,string skinName)
    {
        this.group = playerName;
        gameObject.GetComponent<SetMaterial>().SetNewMaterial(skinName);
    }

}
