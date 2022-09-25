using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bot_Health : MonoBehaviour
{
    public float health;
    public float Maxhealth;
    public GameObject World;
    // Start is called before the first frame update
    void Start()
    {
        health = 200f;
        Maxhealth = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        //Bot stirbt wenn er 0HP hat
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Bullet"){
            health -= 20;
        }
    }
}
