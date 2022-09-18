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
    public GameObject Healthbar;
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
