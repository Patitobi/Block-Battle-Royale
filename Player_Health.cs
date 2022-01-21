using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 200;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Pistol-Bullet"){
            health -= 20;
        }
    }
}
