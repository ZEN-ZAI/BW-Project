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

    public void WalkToBlock(int newIndex_x, int newIndex_y)
    {

        Map.instance.MoveCharacter(x,y,newIndex_x, newIndex_y);
        //transform.GetComponent<PlayerMotor>().MoveToPoint(Map.GetBlockPosition(index_x,index_y));
        transform.SetPositionAndRotation(Map.instance.GetBlockPosition(newIndex_x, newIndex_y), Quaternion.identity);

        this.x = newIndex_x;
        this.y = newIndex_y;
    }

    public void ChangeGroup(Player player)
    {
        this.group = player.playerName;
        gameObject.GetComponent<SetMaterial>().SetNewMaterial(player.characterPlayerName);
    }

}
