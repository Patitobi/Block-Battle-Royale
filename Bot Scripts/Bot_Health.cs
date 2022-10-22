using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bot_Health : MonoBehaviour
{
    public float health;
    public float Maxhealth;
    public GameObject World, Healthbar;
    public float prcent = 7.97f, healthpercent, displayhealth; //1 Prozent der healthbar in units
    // Start is called before the first frame update
    void Start()
    {
        health = 200f;
        Maxhealth = 200f;
    }

    public void Damage(int Dmg){
        health -= Dmg;
        //Check ob tod
        if(health <= 0){
            Destroy(this.gameObject);
        }
        //Healthbar aktualisieren
        healthpercent = health / 2; // weil wir 200 health haben und wenn wir durch 200 teilen haben wir sofort die prozent
        displayhealth = healthpercent * prcent; //aktuelle leben in % * 1%
        Healthbar.GetComponent<Transform>().localScale = new Vector3(displayhealth, Healthbar.GetComponent<Transform>().localScale.y, Healthbar.GetComponent<Transform>().localScale.z); //x achse weil die bar um 90 grad gedreht ist
    }
}
