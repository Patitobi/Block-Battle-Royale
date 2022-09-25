using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    GameObject[] Bots_View_Collider;
    // Start is called before the first frame update
    void Start()
    {
        Bots_View_Collider = GameObject.FindGameObjectsWithTag("Player Check");
        Physics2D.IgnoreLayerCollision(21, 2); //Bullets ignorieren die IgnoreRaycasts Layer
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(this.gameObject);
    }
}