using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone_Collider_Damage : MonoBehaviour
{
    public GameObject World;
    public bool hi;
    void Start(){
        World = GameObject.Find("World");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" && World.GetComponent<Zone_Manager>().TakingDamage == false){
            World.GetComponent<Zone_Manager>().InZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player"){
            World.GetComponent<Zone_Manager>().InZone = false;
        }
    }
}
