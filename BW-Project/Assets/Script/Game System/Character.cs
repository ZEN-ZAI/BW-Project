using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int x;
    public int y;

    public string group;

    public void WalkAnimation()
    {

    }

    public void WalkToBlock(int target_x, int target_y)
    {

        Map.instance.MoveCharacter(x,y, target_x, target_y);
        //transform.GetComponent<PlayerMotor>().MoveToPoint(Map.GetBlockPosition(index_x,index_y));
        transform.SetPositionAndRotation(Map.instance.GetBlockPosition(target_x, target_y), Quaternion.identity);

        this.x = target_x;
        this.y = target_y;
    }

    public void ChangeGroup(string playerName,string skinName)
    {
        this.group = playerName;
        gameObject.GetComponent<SetMaterial>().SetNewMaterial(skinName);
    }

}
