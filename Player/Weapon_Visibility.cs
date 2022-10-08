using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Weapon_Visibility : NetworkBehaviour
{
    public GameObject Weapons;

    // Start is called before the first frame update
    void Start()
    {
        if(IsOwner){
        Weapons = GameObject.Find("Weapons_Sprites");
        //Alle Waffen am anfang des Games verstecken.
        Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
        Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
        Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
        Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
        }
    }

    public void Checkvisibility(){
        if(IsOwner){
        Glock_18visibility();
        M4visibility();
        Ak47visibility();
        Snipervisibility();
        }
    }

    public void Glock_18visibility(){
        if(gameObject.GetComponent<Inventory_Handler>().Glock_18_Selected == true){
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(true);
        }else{
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
        }
    }

    public void M4visibility(){
        if(gameObject.GetComponent<Inventory_Handler>().M4_Selected == true){
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(true);
        }else{
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
        }
    }

    public void Ak47visibility(){
        if(gameObject.GetComponent<Inventory_Handler>().Ak47_Selected == true){
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(true);
        }else{
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
        }
    }

    public void Snipervisibility(){
        if(gameObject.GetComponent<Inventory_Handler>().Sniper_Selected == true){
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(true);
        }else{
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
        }
    }
}