using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class no_Camera_rotation : MonoBehaviour
{
    public GameObject player;
    public static Vector3 trans = new Vector3(0,0,0);

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate(){
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

}

