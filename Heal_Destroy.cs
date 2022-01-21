using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Destroy : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnCollisionEnter2D(Collision2D collision) {
     if(collision.gameObject.tag == "Player" && GameObject.Find("Player").GetComponent<Inventory_Handler>().Player_Heal < 5){
         Destroy(this.gameObject);
     }
    }
}
