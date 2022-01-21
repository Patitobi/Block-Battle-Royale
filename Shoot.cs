using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    GameObject Player;
    public GameObject Bullet;
    public GameObject Feuerpunkt; 
    public bool isshooting;
    public float shootTimer;
    int small_ammo;
    int mid_ammo;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        small_ammo = Player.GetComponent<Inventory_Handler>().small_ammo;
        Feuerpunkt = GameObject.Find("Feuerpunkt");
    }

    // Update is called once per frame
    void Update()
    {

    }

     IEnumerator shoot(){
        if(GameObject.Find("Player").GetComponent<Inventory_Handler>().Glock_18_Selected == true && isshooting == false){
            isshooting = true;
            Instantiate(Bullet, Feuerpunkt.transform.position, Feuerpunkt.transform.rotation);
            yield return new WaitForSeconds(shootTimer);
            isshooting = false;        
        }
        
        
        
        
        
        
        /*Für Automatische Gewähre.
        if(GameObject.Find("Player").GetComponent<Inventory_Handler>().M4_Selected == true && isshooting == false){
            isshooting = true;
            for(int i = mid_ammo; i != 0 ; i--){
                Instantiate(Bullet, Feuerpunkt.transform.position, Feuerpunkt.transform.rotation);
            }
            yield return new WaitForSecondsRealtime(shootTimer);
            isshooting = false;
        }*/
    }
}
