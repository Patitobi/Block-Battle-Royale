using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiste_Check : MonoBehaviour
{
    public bool Contact, open;
    
    
    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.tag == "Player" || collision.tag == "Bot"){
            try{
                collision.gameObject.GetComponent<Movement>().lootable = true;
            }catch{
                collision.gameObject.GetComponent<Bot_Behavior>().lootable = true;
            }
            Contact = true;
            try{
                if(collision.gameObject.GetComponent<Movement>().lootbutton == true) open = true;
            }catch{
                if(collision.gameObject.GetComponent<Bot_Behavior>().lootbutton == true) open = true;
            }
        }else Contact = false;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "Player" || collision.tag == "Bot"){
            try{
                collision.gameObject.GetComponent<Movement>().lootable = false;
            }catch{
                collision.gameObject.GetComponent<Bot_Behavior>().lootable = false;
            }
            Contact = false;
            open = false;
        }
    }
}
