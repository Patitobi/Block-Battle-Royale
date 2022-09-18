using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_PlayerContact : MonoBehaviour
{
    public GameObject Bot;
    public Rigidbody2D rb;
    Quaternion norotation;
    public float Raydistance;
    [SerializeField] private GameObject Wall;
    [SerializeField] private LayerMask layerMask;
    private RaycastHit2D[] contactPoints = new RaycastHit2D[100];
    private Vector2[] endpos = new Vector2[100];
    private string[] Itemtags = new string[] {"Glock_18", "M4", "AK_47", "Sniper"};
    private string[] Ammotags = new string[] {"Big_Ammo", "Mid_Ammo", "Small_Ammo", "Heal", "Kiste_Oben", "Kiste_Unten", "Kiste_Rechts", "Kiste_Links"}; //Ammo und Heal
    public string[] Kistentags = new string[] {"Kiste_Oben", "Kiste_Unten", "Kiste_Rechts", "Kiste_Links"}; //Kisten
    [SerializeField] public List<int> Entryids = new List<int>();
    public bool Entry2 = false;

    private void Awake() {
          
    }
    void Start(){
        norotation = Quaternion.Euler(0, 0, 0);
        
    }
    
    void EnemyContact(float Raydistance) {
        float x = 0;
        //Setze alle sum variablen wieder auf null
        Bot.GetComponent<Bot_Behavior>().rightsum = 0;
        Bot.GetComponent<Bot_Behavior>().leftsum = 0;
        Bot.GetComponent<Bot_Behavior>().topsum = 0;
        Bot.GetComponent<Bot_Behavior>().bottomsum = 0;

        //Create 4 Rays. Up, Down, Left, Right.
        //1 Oben
        Vector2 endpos0 = Bot.transform.position + Vector3.up * Raydistance;
        contactPoints[0] = Physics2D.Linecast(Bot.transform.position, endpos0, layerMask);
        //Make the rays visible in the Scene
        if(contactPoints[0].collider == null) {
            Debug.DrawLine(Bot.transform.position, endpos0, Color.green);
        }else{
            Debug.DrawLine(Bot.transform.position, contactPoints[0].point, Color.red);
        }
        if(contactPoints[0].collider == null){
            Bot.GetComponent<Bot_Behavior>().topsum += 10;
        }else Bot.GetComponent<Bot_Behavior>().topsum += contactPoints[0].distance;
        
        //2 Unten
        Vector2 endpos1 = Bot.transform.position + Vector3.down * Raydistance;
        contactPoints[1] = Physics2D.Linecast(transform.position, endpos1, layerMask);
        if(contactPoints[1].collider == null) {
            Debug.DrawLine(Bot.transform.position, endpos1, Color.green);
        }else{
            Debug.DrawLine(Bot.transform.position, contactPoints[1].point, Color.red);
        }
        if(contactPoints[1].collider == null){
            Bot.GetComponent<Bot_Behavior>().bottomsum += 10;
        }else Bot.GetComponent<Bot_Behavior>().bottomsum += contactPoints[1].distance;
        
        //3 Rechts
        Vector2 endpos2 = Bot.transform.position + Vector3.right * Raydistance * 2f;
        contactPoints[2] = Physics2D.Linecast(transform.position, endpos2, layerMask);
        if(contactPoints[2].collider == null) {
            Debug.DrawLine(Bot.transform.position, endpos2, Color.green);
        }else{
            Debug.DrawLine(Bot.transform.position, contactPoints[2].point, Color.red);
        }
        if(contactPoints[2].collider == null){
            Bot.GetComponent<Bot_Behavior>().rightsum += 20;
        }else Bot.GetComponent<Bot_Behavior>().rightsum += contactPoints[2].distance;
        
        //4 Links
        Vector2 endpos3 = Bot.transform.position + Vector3.left * Raydistance * 2f;
        contactPoints[3] = Physics2D.Linecast(transform.position, endpos3, layerMask);
        if(contactPoints[3].collider == null) {
            Debug.DrawLine(Bot.transform.position, endpos3, Color.green);
        }else{
            Debug.DrawLine(Bot.transform.position, contactPoints[3].point, Color.red);
        }
        if(contactPoints[3].collider == null){
            Bot.GetComponent<Bot_Behavior>().leftsum += 20;
        }else Bot.GetComponent<Bot_Behavior>().leftsum += contactPoints[3].distance;
        
        //Oben Rechte ecke
        for(int i = 4; i < 20 ;i++){
            x += .125f;
            endpos[i] = Bot.transform.position + Vector3.up * Raydistance + Vector3.right * Raydistance * x;
            contactPoints[i] = Physics2D.Linecast(transform.position, endpos[i], layerMask);
            if(contactPoints[i].collider == null) {
                Debug.DrawLine(Bot.transform.position, endpos[i], Color.green);
            }else{
                Debug.DrawLine(Bot.transform.position, contactPoints[i].point, Color.red);
            }
            if(contactPoints[i].collider == null){
            Bot.GetComponent<Bot_Behavior>().topsum += 10;
            }else Bot.GetComponent<Bot_Behavior>().topsum += contactPoints[i].distance;
        }
        //Rechte Seite
        for(int i = 4; i < 20 ;i++){
            x -= .125f;
            endpos[i] = Bot.transform.position + Vector3.up * Raydistance + Vector3.right * Raydistance * 1.5f + Vector3.right * Raydistance * 0.5f + Vector3.down * Raydistance * x;
            contactPoints[i] = Physics2D.Linecast(transform.position, endpos[i], layerMask);
            if(contactPoints[i].collider == null) {
                Debug.DrawLine(Bot.transform.position, endpos[i], Color.green);
            }else{
                Debug.DrawLine(Bot.transform.position, contactPoints[i].point, Color.red);
            }
            //Addiere alles zur rightsum variable
            if(contactPoints[i].collider == null){
            Bot.GetComponent<Bot_Behavior>().rightsum += 20;
            }else Bot.GetComponent<Bot_Behavior>().rightsum += contactPoints[i].distance;
        }
        x = 0;
        //Oben Linke ecke
        for(int i = 21; i < 37 ;i++){
            x += .125f;
            endpos[i] = Bot.transform.position + Vector3.up * Raydistance + Vector3.left * Raydistance * x;
            contactPoints[i] = Physics2D.Linecast(transform.position, endpos[i], layerMask);
            if(contactPoints[i].collider == null) {
                Debug.DrawLine(Bot.transform.position, endpos[i], Color.green);
            }else{
                Debug.DrawLine(Bot.transform.position, contactPoints[i].point, Color.red);
            }
            if(contactPoints[i].collider == null){
            Bot.GetComponent<Bot_Behavior>().topsum += 10;
            }else Bot.GetComponent<Bot_Behavior>().topsum += contactPoints[i].distance;
        }
        //Linke Seite
        for(int i = 38; i < 54 ;i++){
            x -= .125f;
            endpos[i] = Bot.transform.position + Vector3.up * Raydistance + Vector3.left * Raydistance * 1.5f + Vector3.left * Raydistance * 0.5f + Vector3.down * Raydistance * x;
            contactPoints[i] = Physics2D.Linecast(transform.position, endpos[i], layerMask);
            if(contactPoints[i].collider == null) {
                Debug.DrawLine(Bot.transform.position, endpos[i], Color.green);
            }else{
                Debug.DrawLine(Bot.transform.position, contactPoints[i].point, Color.red);
            }
            if(contactPoints[i].collider == null){
            Bot.GetComponent<Bot_Behavior>().leftsum += 20;
            }else Bot.GetComponent<Bot_Behavior>().leftsum += contactPoints[i].distance;
        }
        x = 0;
        //Unten Linke ecke
        for(int i = 55; i < 71 ;i++){
            x += .125f;
            endpos[i] = Bot.transform.position + Vector3.down * Raydistance + Vector3.left * Raydistance * x;
            contactPoints[i] = Physics2D.Linecast(transform.position, endpos[i], layerMask);
            if(contactPoints[i].collider == null) {
                Debug.DrawLine(Bot.transform.position, endpos[i], Color.green);
            }else{
                Debug.DrawLine(Bot.transform.position, contactPoints[i].point, Color.red);
            }
            if(contactPoints[i].collider == null){
            Bot.GetComponent<Bot_Behavior>().bottomsum += 10;
            }else Bot.GetComponent<Bot_Behavior>().bottomsum += contactPoints[i].distance;
        }
        x = 0;
        //Unten Rechte ecke
        for(int i = 72; i < 88 ;i++){
            x += .125f;
            endpos[i] = Bot.transform.position + Vector3.down * Raydistance + Vector3.right * Raydistance * x;
            contactPoints[i] = Physics2D.Linecast(transform.position, endpos[i], layerMask);
            if(contactPoints[i].collider == null) {
                Debug.DrawLine(Bot.transform.position, endpos[i], Color.green);
            }else{
                Debug.DrawLine(Bot.transform.position, contactPoints[i].point, Color.red);
            }
            if(contactPoints[i].collider == null){
            Bot.GetComponent<Bot_Behavior>().bottomsum += 10;
            }else Bot.GetComponent<Bot_Behavior>().bottomsum += contactPoints[i].distance;
        }
        //Setze alle Summen auf ihren Protzentsatz
        Bot.GetComponent<Bot_Behavior>().bottomsum = Bot.GetComponent<Bot_Behavior>().bottomsum / 330;
        Bot.GetComponent<Bot_Behavior>().topsum = Bot.GetComponent<Bot_Behavior>().topsum / 330;
        Bot.GetComponent<Bot_Behavior>().rightsum = Bot.GetComponent<Bot_Behavior>().rightsum / 340;
    	Bot.GetComponent<Bot_Behavior>().leftsum = Bot.GetComponent<Bot_Behavior>().leftsum / 340;

        //Setzt den "Contact" bool auf true oder false je nachdem ob ein Objekt getroffen wurde.
        //Scanne jeden Ray nach Kontakt zu Items.
        //Scanne jeden Ray nach Blockaden für den Bot und Setze die Blockade dann als Gameobject ein.
        foreach(RaycastHit2D contact in contactPoints){
            //Gegner Registrieren
            if(contact.collider != null){
                if(contact.collider.gameObject.tag == "Player" || contact.collider.gameObject.tag == "Bot"){
                    //Setzt das Collidierte Gameobject zum Aktuellen Gegner.
                    if(Bot.GetComponent<Bot_Behavior>().EnemyContact == false) Bot.GetComponent<Bot_Behavior>().Enemy = contact.collider.gameObject;
                    Bot.GetComponent<Bot_Behavior>().EnemyContact = true;
                    break; //Um die Varialble beim nächsten durchlauf nicht wieder auf false zu setzen.
                }

                //Loot Registrieren
                //Waffen
                if(Bot.GetComponent<Bot_Behavior>().EnemyContact == false){
                foreach(string i in Itemtags){
                    if(contact.collider.gameObject.tag == i && Bot.GetComponent<Bot_Behavior>().Currloot == null){
                        if(Bot.GetComponent<Bot_Behavior>().Currloot == null && Bot.GetComponent<Bot_Inventory>().lootcount < 3){
                            Bot.GetComponent<Bot_Behavior>().Currloot = contact.collider.gameObject;
                            Bot.GetComponent<Bot_Behavior>().looting = true;
                        }
                        break;
                    }
                }
                //Ammo
                foreach(string i in Ammotags){
                    if(contact.collider.gameObject.tag == i && Bot.GetComponent<Bot_Behavior>().Currloot == null){
                        Bot.GetComponent<Bot_Behavior>().Currloot = contact.collider.gameObject;
                        Bot.GetComponent<Bot_Behavior>().looting = true;
                        break;
                    }
                }
                //Kisten
                foreach(string i in Kistentags){
                    if(contact.collider.gameObject.tag == i){
                        try{
                        if(contact.collider.gameObject.GetComponent<Kiste>().isopen == false){
                            Bot.GetComponent<Bot_Behavior>().Currloot = contact.collider.gameObject;
                            Bot.GetComponent<Bot_Behavior>().looting = true;
                            break;
                        }else{
                            Bot.GetComponent<Bot_Behavior>().Currloot = null;
                            Bot.GetComponent<Bot_Behavior>().looting = false;
                        }
                        }catch{}
                    }
                }
                //Häuser eingänge Registrieren 
                if(contact.collider.gameObject.tag == "Entry"){
                    if(!Bot.GetComponent<Bot_Behavior>().EnemyContact && !Bot.GetComponent<Bot_Behavior>().looting && 
                        Bot.GetComponent<Bot_Behavior>().Currloot == null && Bot.GetComponent<Bot_Behavior>().inhouse == false){
                        foreach(int i in Entryids){
                            if(contact.collider.gameObject.GetComponent<Entry>().id != i && Entry2 == false){
                                Bot.GetComponent<Bot_Behavior>().EntryPoint = contact.collider.gameObject; //Hier muss geändert werden dass wen der Bot das Haus wieder verlässt nicht diereckt wieder reingeht.
                            }else{
                                if(!Entry2){
                                    Bot.GetComponent<Bot_Behavior>().EntryPoint = null;
                                }
                            }
                        }
                    }
                }
            }else{
                //Setzt auf null.
                Bot.GetComponent<Bot_Behavior>().Enemy = null;
                Bot.GetComponent<Bot_Behavior>().EnemyContact = false;
                Bot.GetComponent<Bot_Behavior>().looting = false;
                Bot.GetComponent<Bot_Behavior>().Currloot = null;
            }
            }
        }
    }

    void Update(){
        //Damit sich die Hitbox für den Enemy detect nicht dumm bewegt.
        transform.rotation = Quaternion.Euler(0f,0f,Bot.transform.rotation.z - transform.rotation.z);
        EnemyContact(Raydistance);
    }
}