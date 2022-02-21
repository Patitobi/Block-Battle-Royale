using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public float health;
    public float Maxhealth;
    public Text Lebenstext;
    private GameObject World;
    // Start is called before the first frame update
    void Start()
    {
        health = 200f;
        Lebenstext = GameObject.Find("Lebenstext").GetComponent<Text>();
        Maxhealth = 200f;
        World = GameObject.Find("World");
    }

    // Update is called once per frame
    void Update()
    {
        Lebenstext.text = health.ToString() + "%";
        GameObject.Find("Health_Bar").GetComponent<Image>().fillAmount = health / Maxhealth;
        if(health <= 20){
            GameObject.Find("Health_Bar").GetComponent<Image>().color = Color.red;
        }else{
            GameObject.Find("Health_Bar").GetComponent<Image>().color = Color.green;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Pistol-Bullet"){
            health -= 20;
        }
    }
}
