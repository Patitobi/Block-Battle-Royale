using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Inventory_Handler Inv;
    GameObject Player;
    public GameObject Bullet;
    public GameObject Feuerpunkt; 
    public bool isshooting;
    public bool shootbttn = false;
    public bool reloadbttn = false;
    public bool inreload;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Feuerpunkt = GameObject.Find("Feuerpunkt");
        Inv = Player.GetComponent<Inventory_Handler>();
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
        if(GameObject.Find("Player").GetComponent<Inventory_Handler>().Glock_18_Selected == true && isshooting == false && Inv.mag_small_ammo > 0){
            isshooting = true;
            Instantiate(Bullet, Feuerpunkt.transform.position, Feuerpunkt.transform.rotation); //WICHTIG Glock reloaded zu schnell!
            Inv.mag_small_ammo--;
            yield return new WaitForSeconds(1f); 
            isshooting = false;
        }
        //Reload Glock-18
        if(Inv.mag_small_ammo == 0 && inreload == false && Inv.small_ammo > 0 && GameObject.Find("Player").GetComponent<Inventory_Handler>().Glock_18_Selected == true && isshooting == false){
            inreload = true;
            Inv.small_ammo -= 12;
            if(Inv.small_ammo < 0){
                Inv.small_ammo += 12;
                Inv.mag_small_ammo = Inv.small_ammo;
                Inv.small_ammo = 0;
                yield return new WaitForSeconds (2.5f);
                goto reloadend;
            }
            yield return new WaitForSeconds (2.5f);
            Inv.mag_small_ammo += 12;
            reloadend:
            inreload = false;
        }
        
        //Für Automatische Gewähre. (M4)
        if(GameObject.Find("Player").GetComponent<Inventory_Handler>().M4_Selected == true && isshooting == false && Inv.mag_mid_ammo > 0){
            isshooting = true;
            for( ; Inv.mag_mid_ammo != 0 ; Inv.mag_mid_ammo--){
                if(shootbttn == false) goto ende;
                Instantiate(Bullet, Feuerpunkt.transform.position, Feuerpunkt.transform.rotation);
                yield return new WaitForSecondsRealtime(0.3f);
            }
            ende:
            isshooting = false;
        }
        //Reload M4
        if(Inv.mag_mid_ammo == 0 && inreload == false && Inv.mid_ammo > 0 && GameObject.Find("Player").GetComponent<Inventory_Handler>().M4_Selected == true && isshooting == false){
            inreload = true;
            Inv.mid_ammo -= 25;
            if(Inv.mid_ammo < 0){
                Inv.mid_ammo += 25;
                Inv.mag_mid_ammo = Inv.mid_ammo;
                Inv.mid_ammo = 0;
                yield return new WaitForSeconds (3.5f);
                goto reloadend;
            }
            yield return new WaitForSeconds (3.5f);
            Inv.mag_mid_ammo += 25;
            reloadend:
            inreload = false;
        }

        //Für Automatische Gewähre. (Ak47)
        if(GameObject.Find("Player").GetComponent<Inventory_Handler>().Ak47_Selected == true && isshooting == false && Inv.mag_mid_ammo > 0){
            isshooting = true;
            for( ; Inv.mag_mid_ammo != 0 ; Inv.mag_mid_ammo--){
                if(shootbttn == false) goto ende;
                Instantiate(Bullet, Feuerpunkt.transform.position, Feuerpunkt.transform.rotation);
                yield return new WaitForSecondsRealtime(0.3f);
            }
            ende:
            isshooting = false;
        }
        //Reload Ak47
        if(Inv.mag_mid_ammo == 0 && inreload == false && Inv.mid_ammo > 0 && GameObject.Find("Player").GetComponent<Inventory_Handler>().Ak47_Selected == true && isshooting == false){
            inreload = true;
            Inv.mid_ammo -= 25;
            if(Inv.mid_ammo < 0){
                Inv.mid_ammo += 25;
                Inv.mag_mid_ammo = Inv.mid_ammo;
                Inv.mid_ammo = 0;
                yield return new WaitForSeconds (3.5f);
                goto reloadend;
            }
            yield return new WaitForSeconds (3.5f);
            Inv.mag_mid_ammo += 25;
            reloadend:
            inreload = false;
        }

        //Sniper
        if(GameObject.Find("Player").GetComponent<Inventory_Handler>().Sniper_Selected == true && isshooting == false && Inv.mag_big_ammo > 0){
            isshooting = true;
            for( ; Inv.mag_big_ammo != 0 ; Inv.mag_big_ammo--){
                if(shootbttn == false) goto ende;
                Instantiate(Bullet, Feuerpunkt.transform.position, Feuerpunkt.transform.rotation);
                yield return new WaitForSecondsRealtime(2.5f);
            }
            ende:
            isshooting = false;
        }

        //Reload Sniper
        if(Inv.mag_big_ammo == 0 && inreload == false && Inv.big_ammo > 0 && GameObject.Find("Player").GetComponent<Inventory_Handler>().Sniper_Selected == true && isshooting == false){
            inreload = true;
            Inv.big_ammo -= 5;
            if(Inv.big_ammo < 0){
                Inv.big_ammo += 5;
                Inv.mag_big_ammo = Inv.big_ammo;
                Inv.big_ammo = 0;
                yield return new WaitForSeconds (6f);
                goto reloadend;
            }
            yield return new WaitForSeconds (6f);
            Inv.mag_big_ammo += 5;
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
        if(inreload == false && GameObject.Find("Player").GetComponent<Inventory_Handler>().Glock_18_Selected == true && isshooting == false && Inv.mag_small_ammo != 12){
            inreload = true;
            yield return new WaitForSeconds(2f);
            Inv.small_ammo += Inv.mag_small_ammo;
            if(Inv.small_ammo >= 12){
                Inv.mag_small_ammo = 12;
                Inv.small_ammo -= 12;
            }else if(Inv.small_ammo < 12){
                Inv.mag_small_ammo = Inv.small_ammo;
                Inv.small_ammo = 0;
            }
            inreload = false;
        }

        //Reload M4
        if(inreload == false && GameObject.Find("Player").GetComponent<Inventory_Handler>().M4_Selected == true && isshooting == false && Inv.mag_mid_ammo != 25){
            inreload = true;
            yield return new WaitForSeconds(4f);
            Inv.mid_ammo += Inv.mag_mid_ammo;
            if(Inv.mid_ammo >= 25){
                Inv.mag_mid_ammo = 25;
                Inv.mid_ammo -= 25;
            }else if(Inv.mid_ammo < 25){
                Inv.mag_mid_ammo = Inv.mid_ammo;
                Inv.mid_ammo = 0;
            }
            inreload = false;
        }

        //Reload M4
        if(inreload == false && GameObject.Find("Player").GetComponent<Inventory_Handler>().Ak47_Selected == true && isshooting == false && Inv.mag_mid_ammo != 25){
            inreload = true;
            yield return new WaitForSeconds(4f);
            Inv.mid_ammo += Inv.mag_mid_ammo;
            if(Inv.mid_ammo >= 25){
                Inv.mag_mid_ammo = 25;
                Inv.mid_ammo -= 25;
            }else if(Inv.mid_ammo < 25){
                Inv.mag_mid_ammo = Inv.mid_ammo;
                Inv.mid_ammo = 0;
            }
            inreload = false;
        }

        //Reload Sniper
        if(inreload == false && GameObject.Find("Player").GetComponent<Inventory_Handler>().Sniper_Selected == true && isshooting == false && Inv.mag_big_ammo != 5){
            inreload = true;
            yield return new WaitForSeconds(5f);
            Inv.big_ammo += Inv.mag_big_ammo;
            if(Inv.big_ammo >= 5){
                Inv.mag_big_ammo = 5;
                Inv.big_ammo -= 5;
            }else if(Inv.big_ammo < 5){
                Inv.mag_big_ammo = Inv.big_ammo;
                Inv.big_ammo = 0;
            }
            inreload = false;
        }
    }
}
