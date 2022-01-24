using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    GameObject Player;
    public GameObject Bullet;
    public GameObject Feuerpunkt; 
    public bool isshooting;
    public bool shootbttn = false;
    public bool reloadbttn = false;
    public bool inreload;
    public int small_ammo;
    public int mid_ammo;
    public int big_ammo;
    public int mag_small_ammo;
    public int mag_mid_ammo;
    public int mag_big_ammo;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Feuerpunkt = GameObject.Find("Feuerpunkt");
    }

    // Update is called once per frame
    void Update()
    {
        if(shootbttn == true){
            StartCoroutine(shoot());
        }

        if(reloadbttn == true){
            StartCoroutine(reload());
        }
    }

     IEnumerator shoot(){
         //Glock-18
        if(GameObject.Find("Player").GetComponent<Inventory_Handler>().Glock_18_Selected == true && isshooting == false && mag_small_ammo > 0){
            isshooting = true;
            Instantiate(Bullet, Feuerpunkt.transform.position, Feuerpunkt.transform.rotation); //WICHTIG Glock reloaded zu schnell!
            mag_small_ammo--;
            yield return new WaitForSeconds(1f); 
            isshooting = false;
        }
        //Reload Glock-18
        if(mag_small_ammo == 0 && inreload == false && small_ammo > 0 && GameObject.Find("Player").GetComponent<Inventory_Handler>().Glock_18_Selected == true && isshooting == false){
            inreload = true;
            small_ammo -= 12;
            if(small_ammo < 0){
                small_ammo += 12;
                mag_small_ammo = small_ammo;
                small_ammo = 0;
                yield return new WaitForSeconds (2.5f);
                goto reloadend;
            }
            yield return new WaitForSeconds (2.5f);
            mag_small_ammo += 12;
            reloadend:
            inreload = false;
        }
        
        //Für Automatische Gewähre. (M4)
        if(GameObject.Find("Player").GetComponent<Inventory_Handler>().M4_Selected == true && isshooting == false && mag_mid_ammo > 0){
            isshooting = true;
            for( ; mag_mid_ammo != 0 ; mag_mid_ammo--){
                if(shootbttn == false) goto ende;
                Instantiate(Bullet, Feuerpunkt.transform.position, Feuerpunkt.transform.rotation);
                yield return new WaitForSecondsRealtime(0.3f);
            }
            ende:
            isshooting = false;
        }
        //Reload M4
        if(mag_mid_ammo == 0 && inreload == false && mid_ammo > 0 && GameObject.Find("Player").GetComponent<Inventory_Handler>().M4_Selected == true && isshooting == false){
            inreload = true;
            mid_ammo -= 25;
            if(mid_ammo < 0){
                mid_ammo += 25;
                mag_mid_ammo = mid_ammo;
                mid_ammo = 0;
                yield return new WaitForSeconds (3.5f);
                goto reloadend;
            }
            yield return new WaitForSeconds (3.5f);
            mag_mid_ammo += 25;
            reloadend:
            inreload = false;
        }

        //Für Automatische Gewähre. (Ak47)
        if(GameObject.Find("Player").GetComponent<Inventory_Handler>().Ak47_Selected == true && isshooting == false && mag_mid_ammo > 0){
            isshooting = true;
            for( ; mag_mid_ammo != 0 ; mag_mid_ammo--){
                if(shootbttn == false) goto ende;
                Instantiate(Bullet, Feuerpunkt.transform.position, Feuerpunkt.transform.rotation);
                yield return new WaitForSecondsRealtime(0.3f);
            }
            ende:
            isshooting = false;
        }
        //Reload Ak47
        if(mag_mid_ammo == 0 && inreload == false && mid_ammo > 0 && GameObject.Find("Player").GetComponent<Inventory_Handler>().Ak47_Selected == true && isshooting == false){
            inreload = true;
            mid_ammo -= 25;
            if(mid_ammo < 0){
                mid_ammo += 25;
                mag_mid_ammo = mid_ammo;
                mid_ammo = 0;
                yield return new WaitForSeconds (3.5f);
                goto reloadend;
            }
            yield return new WaitForSeconds (3.5f);
            mag_mid_ammo += 25;
            reloadend:
            inreload = false;
        }

        //Sniper
        if(GameObject.Find("Player").GetComponent<Inventory_Handler>().Sniper_Selected == true && isshooting == false && mag_big_ammo > 0){
            isshooting = true;
            for( ; mag_big_ammo != 0 ; mag_big_ammo--){
                if(shootbttn == false) goto ende;
                Instantiate(Bullet, Feuerpunkt.transform.position, Feuerpunkt.transform.rotation);
                yield return new WaitForSecondsRealtime(2.5f);
            }
            ende:
            isshooting = false;
        }

        //Reload Sniper
        if(mag_big_ammo == 0 && inreload == false && big_ammo > 0 && GameObject.Find("Player").GetComponent<Inventory_Handler>().Sniper_Selected == true && isshooting == false){
            inreload = true;
            big_ammo -= 5;
            if(big_ammo < 0){
                big_ammo += 5;
                mag_big_ammo = big_ammo;
                big_ammo = 0;
                yield return new WaitForSeconds (6f);
                goto reloadend;
            }
            yield return new WaitForSeconds (6f);
            mag_big_ammo += 5;
            reloadend:
            inreload = false;
        }
    }
//Shootbutton variablen
    public void shootbutton(){
        shootbttn = true;
    }
    public void shootbuttonrelease(){
        shootbttn = false;
    }
//Reloadbutton variablen
    public void reloadbutton(){
        reloadbttn = true;
    }
    public void reloadbuttonrelease(){
        reloadbttn = false;
    }

    public IEnumerator reload(){
        //Reload Glock-18
        if(inreload == false && GameObject.Find("Player").GetComponent<Inventory_Handler>().Glock_18_Selected == true && isshooting == false && mag_small_ammo != 12){
            inreload = true;
            yield return new WaitForSeconds(2f);
            small_ammo += mag_small_ammo;
            if(small_ammo >= 12){
                mag_small_ammo = 12;
                small_ammo -= 12;
            }else if(small_ammo < 12){
                mag_small_ammo = small_ammo;
                small_ammo = 0;
            }
            inreload = false;
        }

        //Reload M4
        if(inreload == false && GameObject.Find("Player").GetComponent<Inventory_Handler>().M4_Selected == true && isshooting == false && mag_mid_ammo != 25){
            inreload = true;
            yield return new WaitForSeconds(4f);
            mid_ammo += mag_mid_ammo;
            if(mid_ammo >= 25){
                mag_mid_ammo = 25;
                mid_ammo -= 25;
            }else if(mid_ammo < 25){
                mag_mid_ammo = mid_ammo;
                mid_ammo = 0;
            }
            inreload = false;
        }

        //Reload M4
        if(inreload == false && GameObject.Find("Player").GetComponent<Inventory_Handler>().Ak47_Selected == true && isshooting == false && mag_mid_ammo != 25){
            inreload = true;
            yield return new WaitForSeconds(4f);
            mid_ammo += mag_mid_ammo;
            if(mid_ammo >= 25){
                mag_mid_ammo = 25;
                mid_ammo -= 25;
            }else if(mid_ammo < 25){
                mag_mid_ammo = mid_ammo;
                mid_ammo = 0;
            }
            inreload = false;
        }

        //Reload Sniper
        if(inreload == false && GameObject.Find("Player").GetComponent<Inventory_Handler>().Sniper_Selected == true && isshooting == false && mag_big_ammo != 5){
            inreload = true;
            yield return new WaitForSeconds(5f);
            big_ammo += mag_big_ammo;
            if(big_ammo >= 5){
                mag_big_ammo = 5;
                big_ammo -= 5;
            }else if(big_ammo < 5){
                mag_big_ammo = big_ammo;
                big_ammo = 0;
            }
            inreload = false;
        }
    }
}
