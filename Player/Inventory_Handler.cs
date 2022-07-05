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
    //Aktuell Ausgewählter Slot
    public bool Slot1_Selected = false;
    public bool Slot2_Selected = false;
    public bool Slot3_Selected = false;
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
    public int slot1_mag_ammo;
    public int slot2_mag_ammo;
    public int slot3_mag_ammo;
    //Andere Objekte
    public GameObject Weapons;
    public int lootcount;
    public int lootcount2;
    public int Player_Heal;
    public int Player_Heal2;
    public GameObject Player;
    //Aktuelles Magazin und Reserve
    public int CurrentMag;
    public int CurrentMaxAmmo;
    public Camera MainCamera;
    public int schleifenx = 0;
    private float i;
    private string[] weapon_names = new string [4]{"Glock_18", "M4", "Ak47", "Sniper"};
    [SerializeField] private Sprite[] weapon_icons = new Sprite[4];

    // Start is called before the first frame update
    void Start()
    {
        Healtxt = GameObject.Find("Heal_Count").GetComponent<Text>();
        Player = GameObject.Find("Player");
        Player_Heal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CameraZoomOut());

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
        GameObject.Find("Small Ammo Reserve").GetComponent<Text>().text = small_ammo.ToString();
        GameObject.Find("Mid Ammo Reserve").GetComponent<Text>().text = mid_ammo.ToString();
        GameObject.Find("Big Ammo Reserve").GetComponent<Text>().text = big_ammo.ToString(); 

        //MagUI Funktion darstellen.
        GameObject.Find("Ammo_Reserve").GetComponent<Text>().text = CurrentMaxAmmo.ToString();
        GameObject.Find("Ammo_Mag").GetComponent<Text>().text = CurrentMag.ToString();

        //Mag wird aufs Aktuelle gewechselt. Mann Könnte bei den Conditionen bei Problemen andere Waffen auf False setzen.
        if(Glock_18_Selected == true){
            CurrentMaxAmmo = small_ammo;
        }else if(M4_Selected == true){
            CurrentMaxAmmo = mid_ammo;
        }else if(Ak47_Selected == true){
            CurrentMaxAmmo = mid_ammo;
        }else if(Sniper_Selected == true){
            CurrentMaxAmmo = big_ammo;
        }else{
            CurrentMaxAmmo = 0;
        }

        //Stellt das Aktuelle Slot mag auf den Current mag um dann mit dem CurrentMag zu reloden und anzuzeigen.
        if(Slot1_Selected == true){
            CurrentMag = slot1_mag_ammo;
        }else if(Slot2_Selected == true){
            CurrentMag = slot2_mag_ammo;
        }else if(Slot3_Selected == true){
            CurrentMag = slot3_mag_ammo;
        }else{
            CurrentMag = 0;
        }
    }

    void LateUpdate() {
        lootcount = lootcount2; //Ist dazu da den Lootcount ein wenig später zu aktualisieren damit langsame handys keine Probleme beim Lootcount haben
        Player_Heal = Player_Heal2; 
    }

    IEnumerator CameraZoomOut(){
        //Camera passt sich zur Waffe an
        if(Sniper_Selected ||  M4_Selected || Ak47_Selected || Glock_18_Selected){
        if(schleifenx == 0)schleifenx++;
        if(schleifenx == 1){
            if(Sniper_Selected){
                for(i = MainCamera.GetComponent<Camera>().orthographicSize; i <= 20f; ){
                    if(i <= 20f){
                        i++;
                    }else{
                        i--;
                    }
                    MainCamera.GetComponent<Camera>().orthographicSize = i; //Sniper
                    yield return new WaitForSeconds(0.025f);
                }
            }else if(M4_Selected){
                for(i = MainCamera.GetComponent<Camera>().orthographicSize; i != 12f; ){
                    if(i > 12f){
                        i--;
                    }else{
                        i++;
                    }
                    MainCamera.GetComponent<Camera>().orthographicSize = i; //M4
                    yield return new WaitForSeconds(0.01f);
                }
            }else if(Ak47_Selected){
                for(i = MainCamera.GetComponent<Camera>().orthographicSize;i != 12f; ){
                    if(i > 12f){
                        i--;
                    }else{
                        i++;
                    }
                    MainCamera.GetComponent<Camera>().orthographicSize = i; //Ak
                    yield return new WaitForSeconds(0.01f);
                }
            }else{
                for(i = MainCamera.GetComponent<Camera>().orthographicSize;i != 10f; ){
                if(i > 10f){
                    i--;
                }else{
                    i++;
                }
                MainCamera.GetComponent<Camera>().orthographicSize = i; //Default
                yield return new WaitForSeconds(0.01f);
            }
            }
        }
        }else{
            schleifenx = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        //Slot1
        if(Slot1 == false){
        foreach(string i in weapon_names){
            if(collision.gameObject.tag == i && lootcount <= 3){
                Slot1_Item = i;
                Slot1 = true;
                foreach(Sprite img in weapon_icons){
                    if(collision.gameObject.tag + "_Inventory_View" == img.name){
                        GameObject.Find("Icon1").GetComponent<Image>().sprite = img;
                    }
                }
                slot1_mag_ammo = collision.gameObject.GetComponent<Weapon_Info>().Currentammo;
            }
        }
        }
        //Slot2
        else if(Slot2 == false && Slot1 == true){
        foreach(string i in weapon_names){
            if(collision.gameObject.tag == i && lootcount <= 3){
                Slot2_Item = i;
                Slot2 = true;
                foreach(Sprite img in weapon_icons){
                    if(collision.gameObject.tag + "_Inventory_View" == img.name){
                        GameObject.Find("Icon2").GetComponent<Image>().sprite = img;
                    }
                }
                slot2_mag_ammo = collision.gameObject.GetComponent<Weapon_Info>().Currentammo;
            }
        }
        }
        //Slot3
        else if(Slot3 == false && Slot2 == true && Slot1 == true){
        foreach(string i in weapon_names){
            if(collision.gameObject.tag == i && lootcount <= 3){
                Slot3_Item = i;
                Slot3 = true;
                foreach(Sprite img in weapon_icons){
                    if(collision.gameObject.tag + "_Inventory_View" == img.name){
                        GameObject.Find("Icon3").GetComponent<Image>().sprite = img;
                    }
                }
                slot3_mag_ammo = collision.gameObject.GetComponent<Weapon_Info>().Currentammo;
            }
        }
        }
        
        //Lootcount
        if(collision.gameObject.tag == "Glock_18" && lootcount < 3){
            lootcount2 += 1;
        }
        else if(collision.gameObject.tag == "M4" && lootcount < 3){
            lootcount2 += 1;
        }
        else if(collision.gameObject.tag == "AK_47" && lootcount < 3){
            lootcount2 += 1;
        }
        else if(collision.gameObject.tag == "Sniper" && lootcount < 3){
            lootcount2 += 1;
        }
        else if(collision.gameObject.tag == "Heal" && Player_Heal <= 4){
            Player_Heal2 += 1;
        }

        //Munition Aufheben

        if(collision.gameObject.tag == "Small_Ammo"){
            small_ammo += collision.gameObject.GetComponent<Ammo_Info>().Ammo;
        }
        else if(collision.gameObject.tag == "Mid_Ammo"){
            mid_ammo += collision.gameObject.GetComponent<Ammo_Info>().Ammo;
        }
        else if(collision.gameObject.tag == "Big_Ammo"){
            big_ammo += collision.gameObject.GetComponent<Ammo_Info>().Ammo;
        }
    }


    public void Slot1_function(){
        if(Slot1_Item == "Glock_18"){
            //Aktive Waffe
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(true);
            Glock_18_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot1_Item == "M4"){
            //Aktive Waffe
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(true);
            M4_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot1_Item == "Ak47"){
            //Aktive Waffe
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(true);
            Ak47_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot1_Item == "Sniper"){
            //Aktive Waffe
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(true);
            Sniper_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
        }else if(Slot1_Item == ""){
            //Alle anderen Deaktivieren
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
        }
        Slot1_Selected = true;
        Slot2_Selected = false;
        Slot3_Selected = false;
    }
    public void Slot2_function(){
        if(Slot2_Item == "Glock_18"){
            //Aktive Waffe
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(true);
            Glock_18_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot2_Item == "M4"){
            //Aktive Waffe
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(true);
            M4_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot2_Item == "Ak47"){
            //Aktive Waffe
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(true);
            Ak47_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot2_Item == "Sniper"){
            //Aktive Waffe
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(true);
            Sniper_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
        }else if(Slot2_Item == ""){
            //Alle anderen Deaktivieren
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
        }
        Slot1_Selected = false;
        Slot2_Selected = true;
        Slot3_Selected = false;
    }
    public void Slot3_function(){
        if(Slot3_Item == "Glock_18"){
            //Aktive Waffe
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(true);
            Glock_18_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot3_Item == "M4"){
            //Aktive Waffe
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(true);
            M4_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot3_Item == "Ak47"){
            //Aktive Waffe
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(true);
            Ak47_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
        }else if(Slot3_Item == "Sniper"){
            //Aktive Waffe
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(true);
            Sniper_Selected = true;
            //Alle anderen Deaktivieren
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
        }else if(Slot3_Item == ""){
            //Alle anderen Deaktivieren
            Weapons.transform.Find("Sniper_Top_Sprite").gameObject.SetActive(false);
            Sniper_Selected = false;
            Weapons.transform.Find("M4_Top_Sprite").gameObject.SetActive(false);
            M4_Selected = false;
            Weapons.transform.Find("Ak47_Top_Sprite").gameObject.SetActive(false);
            Ak47_Selected = false;
            Weapons.transform.Find("Glock_18_Top_Sprite").gameObject.SetActive(false);
            Glock_18_Selected = false;
        }
        Slot1_Selected = false;
        Slot2_Selected = false;
        Slot3_Selected = true;
    }

    public void Slot4_function(){
        //Heal Funktion
        if(Player_Heal > 0 && GameObject.Find("Player").GetComponent<Player_Health>().health < 200){
            Player_Heal2 -= 1;
            GameObject.Find("Player").GetComponent<Player_Health>().health += 20;
        }
    }
}