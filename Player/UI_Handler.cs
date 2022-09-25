using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class UI_Handler : MonoBehaviour
{
    [SerializeField] GameObject BigMap;
    [SerializeField] GameObject X_Button;
    [SerializeField] GameObject MinimapCam;
    [SerializeField] GameObject Home_Button;
    private GameObject[] AllrenderableGameObjects;

    public void Start(){
       AllrenderableGameObjects = FindObjectsOfType<Render_Manager>().Select(rm => rm.gameObject).ToArray();
    }

    public void Mapbutton(){
        //Mache Alles auf der Karte sichtbar
        foreach(GameObject i in AllrenderableGameObjects){
            if(i != null){
                i.GetComponent<Renderer>().enabled = true;
            }
        }
        MinimapCam.SetActive(true);
        BigMap.SetActive(true);
        X_Button.SetActive(true);
        Home_Button.SetActive(true);

        Time.timeScale = 0; //Pausiere das Spiel
    }

    public void ResumeGame(){
        //Map + X Button Entfernen
        BigMap.SetActive(false);
        X_Button.SetActive(false);
        Home_Button.SetActive(false);
        //Cam Aus
        MinimapCam.SetActive(false);
        //Spiel geht weiter
        Time.timeScale = 1; 
    }

    public IEnumerator Wait(){
        yield return new WaitForSeconds(.2f);
    }

    public void BacktoHome(){
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName:"Title_Menu");
    }
}
