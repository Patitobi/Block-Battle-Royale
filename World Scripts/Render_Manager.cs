using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Render_Manager : MonoBehaviour
{
    public float Renderdistance;
    public GameObject Player;
    public Renderer rend;
    public Light2D lightcomponent;
    void Awake(){
        Player = GameObject.Find("Player");
        if(GetComponent<Renderer>() != null) rend = GetComponent<Renderer>(); //Sprite Renderer
        //if(GetComponent<Light2D>() != null) lightcomponent = GetComponent<Light2D>(); //Licht
        Renderdistance = 75f;
        //Wege und gebäude früher einblenden 
        if(this.gameObject.tag == "Gebäude"){
            Renderdistance = Renderdistance * 2;
        }else if(this.gameObject.tag == "Wege"){
            Renderdistance = Renderdistance * 8f;
        }else if(this.gameObject.tag == "Highrender"){
            Renderdistance = Renderdistance * 8f;
        }
        if(rend != null) StartCoroutine(Rendercheck());
        //else if(lightcomponent != null) StartCoroutine(lightcheck());
    }

    private IEnumerator Rendercheck(){
        while(true){
            if(Time.timeScale == 1){ //Verhindert das Minimap Optimiert Gerendert wird. Sonst würde nur die umgebung des spielers auf der map angezeigtr werden
                if(Vector2.Distance(Player.transform.position, transform.position) > Renderdistance){
                    rend.enabled = false;
                }else{
                    rend.enabled = true;
                }
            }
            yield return new WaitForSeconds(4f);
        }
    }

    /*private IEnumerator lightcheck(){
        while(true){
            if(Time.timeScale == 1){ //Verhindert das Minimap Optimiert Gerendert wird. Sonst würde nur die umgebung des spielers auf der map angezeigtr werden
                if(Vector2.Distance(Player.transform.position, transform.position) > Renderdistance){
                    lightcomponent.enabled = false;
                }else{
                    lightcomponent.enabled = true;
                }
            }
            yield return new WaitForSeconds(4f);
        }
    }*/
}
