using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //camera follow player 
    public GameObject Player;
    float timeOff = 3f;
    public float Offset;
    Vector2 posOffset;
    //

    //camera map boundary
    public Transform bounds;//representativos dos cantos da sala
    public float height, zoom;//limites da camera


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");//reference to player object
        
    }
    void Update()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = Player.transform.position;//camera smoothing parameters

        if (Player.GetComponent<PlayerScript>().horMove > 0)
        {
            posOffset.x = Offset;
        }
        else if (Player.GetComponent<PlayerScript>().horMove < 0)
        {
            posOffset.x = -Offset;
        }
        else posOffset.x = 0;//camera pans towards direction of movement

        endPos.x += posOffset.x;
        endPos.y += height;
        endPos.z = -10;
        
        

        transform.position = Vector3.Lerp(startPos, endPos, timeOff * Time.deltaTime);//actually does the moving part
    }

    
}
