using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_PlayerContact : MonoBehaviour
{
    public GameObject Bot;
    Quaternion norotation;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject == this.gameObject){
            Bot.GetComponent<Bot_Behavior>().EnemyContact = false;
        }
        
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bot"){
            Bot.GetComponent<Bot_Behavior>().EnemyContact = true;
        }

        if(collision.gameObject == this.gameObject){
            Bot.GetComponent<Bot_Behavior>().EnemyContact = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bot"){
            Bot.GetComponent<Bot_Behavior>().EnemyContact = false;
        }
    }
    void Update(){
        //Damit sich die Hitbox für den Enemy detect nicht dumm bewegt.
        transform.rotation = Quaternion.Euler(0f,0f,Bot.transform.rotation.z - transform.rotation.z);
    }
}
