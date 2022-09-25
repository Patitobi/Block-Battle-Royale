using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bot_Manager : MonoBehaviour
{
    float mapsizeX = 479; 
    float mapsizeY = 479;
    public GameObject Bot;
    public int Bots;
    private string[] Bot_Names = {"Sergej", "Hannes", "Wolfgang", "Joan", "Steven", "Peter", "Jakob", "Connor", "Dean", "Noah", "Oliver", "James", "Henry", "William", "Lucas", "Daniel", "Alexander", "Chris", "Timothy", "Vlad", "David", "Isaac", "Grayson", "Aaron", "Ryon", "Jason", "Joseppe", "Connor", "Austin", "Jordan"};
    private GameObject[] All_Bots;
    private void Awake() {
       Bots = Menu_Handler.Menu_Bots;
    }
    void Start()
    {
        StartCoroutine(Bot_Spawn());
    }

    public IEnumerator Bot_Spawn(){
        for(int i = 0; i != Bots; i++){
            Instantiate(Bot, new Vector3(Random.Range(-mapsizeX, mapsizeX), Random.Range(-mapsizeY, mapsizeY), 100f /*100 = Bot default height*/), Quaternion.identity);
            yield return new WaitForSeconds(.2f);
        } //1 Bot wird pro durchlauf zwischen x: 0-479 und y: 0-479 gespawned (Random)

        //Jedem Bot wird ein Name gegeben
        //Vorgang: Es wird ein Random Name aus der Array genommen und dieser wird aus der array gestrichen dann beim nächsten durchlauf wird gecheckt ob der neu gewählte name nicht null ist so das wir kein NRE kassieren
        All_Bots = GameObject.FindGameObjectsWithTag("Bot");
        foreach(GameObject i in All_Bots){
            Start:
            int Randint = Random.Range(0,20); //Random Int zwischen 0 und 20.
            string nextname = Bot_Names[Randint]; //Die zufällige zahl wird nun als index in der Array benutzt.
            Bot_Names[Randint] = null; //Der nun benutzte name wird nun gelöscht um zu vermeiden das 2 Bots den selben Namen erhalten.
            if(nextname == null){
                goto Start; //Wenn der Ausgewählte name bereits auf null (benutzt worden) ist suche einen neuen Namen
            }
            i.GetComponentInChildren<TextMeshPro>().text = nextname;
            //Reset alles für den nächsten run
            Randint = 0;
            nextname = null;
        }
    }
}
