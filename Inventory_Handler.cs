using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Handler : MonoBehaviour
{
    //Bools von denen aber nur eine gleichzeitig aktiv seien kann da ja nur eine Waffe in der hand gehalten werden kann.
    public bool Glock_18_Selected;
    public bool M4_Selected;
    public bool Ak47_Selected;
    public bool Sniper_Selected;
    //Strings die besagen Welchen Item sich exact im Slot befindet.
    public string Slot1_Item;
    public string Slot2_Item;
    public string Slot3_Item;
    //Sind Slot Belegt bools.
    public bool Slot1 = false;
    public bool Slot2 = false;
    public bool Slot3 = false;
    //Sprites zum austauschen fürs Inventar UI.
    public Sprite Glock_18;
    public Sprite M4;
    public Sprite Ak47;
    public Sprite Sniper;
    public Text Healtxt;
    //Ammonition
    public int small_ammo;
    public int mid_ammo;
    public int big_ammo;
    public int mag_small_ammo;
    public int mag_mid_ammo;
    public int mag_big_ammo;
    //Andere Objekte
    public GameObject Weapons;
    public int lootcount;
    public int lootcount2;
    public int Player_Heal;
    public int Player_Heal2;
    public GameObject Player;
    //Aktuelles Magazin und Reserve
    public int CurrentMag;
    public int CurrentMaxMag;

    // Start is called before the first frame update
    void Start()
    {
        Healtxt = GameObject.Find("Heal_Count").GetComponent<Text>();
        Weapons = GameObject.Find("Weapons");
        Player = GameObject.Find("Player");
        Player_Heal = 0;
    }

    // Update is called once per frame
    void Update()
    {  
        //Aktualisierung des Heal-Counters im Inv.
        Healtxt.text = Player_Heal.ToString();

        //Heal Count auf Rot stellen falls Kein Heal mehr übrig ist.
        if(Player_Heal == 0){
            Healtxt.color = Color.red;
        }else{
            Healtxt.color = Color.black;
        }

        if(Player_Heal == 6) Player_Heal -= 1;
        if(Player_Heal2 == 6) Player_Heal -= 1;

        //Munition im UI anzeigen
        GameObject.Find("Small Ammo Reserve").GetComponent<Text>().text = "Small Ammo: " + small_ammo.ToString();
        GameObject.Find("Mid Ammo Reserve").GetComponent<Text>().text = "Mid Ammo: " + mid_ammo.ToString();
        GameObject.Find("Big Ammo Reserve").GetComponent<Text>().text = "Big Ammo: " + big_ammo.ToString();

        //Aktuelles MAG UI Aktuallisieren
        MagUI(); 

        //MagUI Funktion darstellen.
        GameObject.Find("Ammo_Reserve").GetComponent<Text>().text = CurrentMaxMag.ToString();
        GameObject.Find("Ammo_Mag").GetComponent<Text>().text = CurrentMag.ToString();
    }

    void LateUpdate() {
        lootcount = lootcount2; //Ist dazu da den Lootcount ein wenig später zu aktualisieren damit langsame handys keine Probleme beim Lootcount haben
        Player_Heal = Player_Heal2; 
 
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(Slot1 == false){
        if(collision.gameObject.tag == "Glock_18" && lootcount <= 3){
            Slot1_Item = "Glock_18";
            Slot1 = true;
            GameObject.Find("Icon1").GetComponent<Image>().sprite = Glock_18;
        } else if(collision.gameObject.tag == "M4" && lootcount <= 3){
            Slot1_Item = "M4";
            Slot1 = true;
            GameObject.Find("Icon1").GetComponent<Image>().sprite = M4;
        }else if(collision.gameObject.tag == "AK_47" && lootcount <= 3){
            Slot1_Item = "Ak47";
            Slot1 = true;
            GameObject.Find("Icon1").GetComponent<Image>().sprite = Ak47;
        }else if(collision.gameObject.tag == "Sniper" && lootcount <= 3){
            Slot1_Item = "Sniper";
            Slot1 = true;
            GameObject.Find("Icon1").GetComponent<Image>().sprite = Sniper;
        }
        }

        else if(Slot2 == false && Slot1 == true){
        if(collision.gameObject.tag == "Glock_18" && lootcount <= 3){
            Slot2_Item = "Glock_18";
            Slot2 = true;
            GameObject.Find("Icon2").GetComponent<Image>().sprite = Glock_18;
        } else if(collision.gameObject.tag == "M4" && lootcount <= 3){
            Slot2_Item = "M4";
            Slot2 = true;
            GameObject.Find("Icon2").GetComponent<Image>().sprite = M4;
        }else if(collision.gameObject.tag == "AK_47" && lootcount <= 3){
            Slot2_Item = "Ak47";
            Slot2 = true;
            GameObject.Find("Icon2").GetComponent<Image>().sprite = Ak47;
        }else if(collision.gameObject.tag == "Sniper" && lootcount <= 3){
            Slot2_Item = "Sniper";
            Slot2 = true;
            GameObject.Find("Icon2").GetComponent<Image>().sprite = Sniper;
        }
        }

        else if(Slot3 == false && Slot2 == true && Slot1 == true){
        if(collision.gameObject.tag == "Glock_18" && lootcount <= 3){
            Slot3_Item = "Glock_18";
            Slot3 = true;
            GameObject.Find("Icon3").GetComponent<Image>().sprite = Glock_18;
        } else if(collision.gameObject.tag == "M4" && lootcount <= 3){
            Slot3_Item = "M4";
            Slot3 = true;
            GameObject.Find("Icon3").GetComponent<Image>().sprite = M4;
        }else if(collision.gameObject.tag == "AK_47" && lootcount <= 3){
            Slot3_Item = "Ak47";
            Slot3 = true;
            GameObject.Find("Icon3").GetComponent<Image>().sprite = Ak47;
        }else if(collision.gameObject.tag == "Sniper" && lootcount <= 3){
            Slot3_Item = "Sniper";
            Slot3 = true;
            GameObject.Find("Icon3").GetComponent<Image>().sprite = Sniper;
        }
        }
        if(collision.gameObject.tag == "Glock_18" && lootcount < 3){
            lootcount2 += 1;
        }
        if(collision.gameObject.tag == "M4" && lootcount < 3){
            lootcount2 += 1;
        }
        if(collision.gameObject.tag == "AK_47" && lootcount < 3){
            lootcount2 += 1;
        }
        if(collision.gameObject.tag == "Sniper" && lootcount < 3){
            lootcount2 += 1;
        }
        if(collision.gameObject.tag == "Heal" && Player_Heal <= 4){
            Player_Heal2 += 1;
        }
    }


    public void Slot1_function(){
        if(Slot1_Item == "Glock_18"){
            //Aktive Waffe
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(true);
            Glock_18_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot1_Item == "M4"){
            //Aktive Waffe
            Weapons.transform.Find("M4_Top").gameObject.SetActive(true);
            M4_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot1_Item == "Ak47"){
            //Aktive Waffe
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(true);
            Ak47_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot1_Item == "Sniper"){
            //Aktive Waffe
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(true);
            Sniper_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(false);
            Glock_18_Selected = false;
        }
    }
    public void Slot2_function(){
        if(Slot2_Item == "Glock_18"){
            //Aktive Waffe
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(true);
            Glock_18_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot2_Item == "M4"){
            //Aktive Waffe
            Weapons.transform.Find("M4_Top").gameObject.SetActive(true);
            M4_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot2_Item == "Ak47"){
            //Aktive Waffe
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(true);
            Ak47_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot2_Item == "Sniper"){
            //Aktive Waffe
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(true);
            Sniper_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(false);
            Glock_18_Selected = false;
        }
    }
    public void Slot3_function(){
        if(Slot3_Item == "Glock_18"){
            //Aktive Waffe
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(true);
            Glock_18_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot3_Item == "M4"){
            //Aktive Waffe
            Weapons.transform.Find("M4_Top").gameObject.SetActive(true);
            M4_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot3_Item == "Ak47"){
            //Aktive Waffe
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(true);
            Ak47_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot3_Item == "Sniper"){
            //Aktive Waffe
            Weapons.transform.Find("Sniper_Top").gameObject.SetActive(true);
            Sniper_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Glock_18_Top").gameObject.SetActive(false);
            Glock_18_Selected = false;
        }
    }

    public void Slot4_function(){
        //Heal Funktion
        if(Player_Heal > 0 && GameObject.Find("Player").GetComponent<Player_Health>().health < 200){
            Player_Heal2 -= 1;
            GameObject.Find("Player").GetComponent<Player_Health>().health += 20;
        }
    }
    
    void MagUI(){
        if(gameObject.GetComponent<Inventory_Handler>().Glock_18_Selected == true){
            CurrentMag = mag_small_ammo;
            CurrentMaxMag = small_ammo;
        }
        if(gameObject.GetComponent<Inventory_Handler>().M4_Selected == true){
            CurrentMag = mag_mid_ammo;
            CurrentMaxMag = mid_ammo;
        }
        if(gameObject.GetComponent<Inventory_Handler>().Ak47_Selected == true){
            CurrentMag = mag_mid_ammo;
            CurrentMaxMag = mid_ammo;
        }
        if(gameObject.GetComponent<Inventory_Handler>().Sniper_Selected == true){
            CurrentMag = mag_big_ammo;
            CurrentMaxMag = big_ammo;
        }
    }
}