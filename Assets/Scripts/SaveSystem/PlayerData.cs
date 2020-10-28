using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public float[] playerPos;

    public PlayerData(PlayerScript player)
    {
        playerPos = new float[2];
        playerPos[0] = player.transform.position.x;
        playerPos[1] = player.transform.position.y;
    }
}
