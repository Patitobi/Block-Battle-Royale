using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public class Map_Manager : MonoBehaviour
{
    public int Playercount;
    private GameObject[] x;
    private int y = 1;
    public GameObject[] EntryPoints;
    public float Timespeed;
    public GameObject GlobalLight;
    public GameObject[] Laternen;

    void Awake(){
        //Registriere alle Laternen in eine Array
        Laternen = GameObject.FindGameObjectsWithTag("Laternen");
        //Mache alle Lichter aus
        TurnOffLights();
        //Rotiert durch Tag und nacht durch
        StartCoroutine(WorldTime());
    }
    void Start()
    {
        StartCoroutine(PlayerCheck());

        EntryPoints = GameObject.FindGameObjectsWithTag("Entry");
        foreach(GameObject i in EntryPoints){
            i.GetComponent<Entry>().id = y;
            y++;
        }
        //reset "y" für Update()
        y = 0;
    }

    private IEnumerator WorldTime(){
        while(true){
            //Es wird Nacht
            while(GlobalLight.GetComponent<Light2D>().intensity > .15f){
                GlobalLight.GetComponent<Light2D>().intensity -= .0075f;
                //Mach Lichter AN wenn Abend (.5f) erreicht wird
                if(GlobalLight.GetComponent<Light2D>().intensity < .5f) TurnOnLights();
                yield return new WaitForSeconds(Timespeed);
            }
            //Es wird Tag
            while(GlobalLight.GetComponent<Light2D>().intensity < .9f){
                GlobalLight.GetComponent<Light2D>().intensity += .015f;
                //Mach Lichter AUS wenn Abend (.5f) erreicht wird
                if(GlobalLight.GetComponent<Light2D>().intensity > .5f) TurnOffLights();
                yield return new WaitForSeconds(Timespeed);
            }
        }
    }

    private void TurnOnLights(){
        foreach(GameObject i in Laternen){
            i.GetComponentInChildren<Light2D>().enabled = true;
        }
    }

    private void TurnOffLights(){
        foreach(GameObject i in Laternen){
            i.GetComponentInChildren<Light2D>().enabled = false;
        }
    }

    IEnumerator PlayerCheck(){
        while(true){
        x =  GameObject.FindGameObjectsWithTag("Bot");
        Playercount = x.Length;
        x = GameObject.FindGameObjectsWithTag("Player");
        Playercount += x.Length;
        GameObject.Find("PlayerCount").GetComponent<Text>().text = Playercount.ToString();
        yield return new WaitForSeconds(5f);
        }
    }
}
