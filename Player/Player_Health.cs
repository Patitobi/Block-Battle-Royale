using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class Player_Health : NetworkBehaviour
{
    public float health;
    public float Maxhealth;
    public Text Lebenstext;
    private GameObject World;
    public GameObject Healthbar;
    // Start is called before the first frame update
    void Start()
    {
        if(IsOwner){
        health = 200f;
        Lebenstext = GameObject.Find("Lebenstext").GetComponent<Text>();
        Maxhealth = 200f;
        World = GameObject.Find("World");
        Healthbar = GameObject.Find("Health_Bar");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOwner){
        Lebenstext.text = health.ToString() + "%";
        Healthbar.GetComponent<Image>().fillAmount = health / Maxhealth;
        if(health <= 20){
            Healthbar.GetComponent<Image>().color = Color.red;
        }else{
            Healthbar.GetComponent<Image>().color = Color.green;
        }

        //Spieler stirbt wenn er 0HP hat
        if(health <= 0){
            Destroy(this.gameObject);
            Time.timeScale = 0;
        }
    }
    }
}
