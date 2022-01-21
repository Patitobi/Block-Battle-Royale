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
    void Update()
    {
    
    }

    private void OnCollisionEnter2D(Collision2D collision) { //Diese Funktion muss in die Inventory_Handler Datei umgeschrieben werden.
     if(collision.gameObject.tag == "Player" && GameObject.Find("Player").GetComponent<Inventory_Handler>().lootcount < 3 ){ //Lootcount wird beim handy früher Aktuallisiert deswegen haben wir beim handy probleme
         Destroy(this.gameObject);
     }
    }
}
