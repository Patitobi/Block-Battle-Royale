using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Map_Manager : MonoBehaviour
{
    public int Playercount;
    private GameObject[] x;
    private int y = 1;
    public GameObject[] EntryPoints;
    // Start is called before the first frame update
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

    

    IEnumerator PlayerCheck(){
        while(1 == 1){
        yield return new WaitForSeconds(3f);
        x =  GameObject.FindGameObjectsWithTag("Bot");
        Playercount = x.Length;
        x = GameObject.FindGameObjectsWithTag("Player");
        Playercount += x.Length;
        GameObject.Find("PlayerCount").GetComponent<Text>().text = Playercount.ToString();
        }
    }
}
