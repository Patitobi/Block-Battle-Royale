using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Destroy : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); 
    }

    // Update is called once per frame
    void Update(){
    {

    }
    }
    private void OnTriggerEnter2D(Collider2D collision) { 
     if(collision.gameObject.tag == "Player" && GameObject.Find("Player").GetComponent<Inventory_Handler>().lootcount < 3 ){ 
         Destroy(this.gameObject);
     }
    }


}
