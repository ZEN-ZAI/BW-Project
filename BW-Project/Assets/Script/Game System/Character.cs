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

    public Animator animator;

    private void Start()
    {
        transform.LookAt(new Vector3(350,0,-10));
    }

    private void Update()
    {

        if (walking)
        {

            if (transform.position == target)
            {
                animator.SetTrigger("down");
                animator.SetBool("idle", true);
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
        animator.SetTrigger("jump");
        animator.SetBool("idle", false);
        target = Map.instance.GetBlockPosition(target_x, target_y);
        Transform temp = Map.instance.GetBlockPositionTranform(target_x, target_y);
        transform.LookAt(temp);
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
