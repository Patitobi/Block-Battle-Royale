using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render_Manager : MonoBehaviour
{
    public float Renderdistance;
    public GameObject Player;
    public Renderer rend;
    void Awake(){
        Player = GameObject.Find("Player");
        rend = GetComponent<Renderer>();
        Renderdistance = 75f;
        //Wege und gebäude früher einblenden 
        if(this.gameObject.tag == "Gebäude"){
            Renderdistance = Renderdistance * 2;
        }else if(this.gameObject.tag == "Wege"){
            Renderdistance = Renderdistance * 8f;
        }else if(this.gameObject.tag == "Highrender"){
            Renderdistance = Renderdistance * 10f;
        }
        StartCoroutine(Rendercheck());
    }

    private IEnumerator Rendercheck(){
        while(true){
            yield return new WaitForSeconds(0.1f);
            if(Vector2.Distance(Player.transform.position, transform.position) > Renderdistance){
                rend.enabled = false;
            }else{
                rend.enabled = true;
            }
        }
    }
}
