using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class no_Camera_rotation : NetworkBehaviour
{
    public GameObject player;
    public static Vector3 trans = new Vector3(0,0,0);

    void Update(){
        
        if(player != null) transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        
    }

}

