using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Behavior : MonoBehaviour
{

    public float speed;
    public Animator animatior;
    Vector3 lastpos;
    private Vector3 prevPosition = Vector3.zero;
    public GameObject Weapons;
    public GameObject World;
    public float X;
    public float Y;
    public float BewegungsängerungsZeit;
    public int Zyklus;
    private bool waiting;
    Transform PlayerTransform;
    Quaternion rot;
    Vector2 movement; 
    Rigidbody2D rb;
    public GameObject Playercontact;
    public int Z;
    public bool EnemyContact;

    void Awake() {
        BewegungsängerungsZeit = 30f;
        StartCoroutine(Bot_Random_Move());
    }
    void Start()
    {
        World = GameObject.Find("World");
        animatior = gameObject.GetComponent<Animator>();
        PlayerTransform = GameObject.Find("Player").transform;
        
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        //Simples Bot Movement

        //Animatior
        if(transform.position != lastpos){
            animatior.SetBool("isrunning", true);
        }else{
            animatior.SetBool("isrunning", false);
        }
        lastpos = transform.position;

        Weapon_Follow_Player();
        
    }

    void FixedUpdate() 
    {
        //Movement in calculierte richtung.
        Vector2 movementdir =  new Vector2(X,Y);

        transform.Translate(movementdir * speed * Time.fixedDeltaTime, Space.World);

        //Bot schaut Player an mit 1 Sec delay
        StartCoroutine(EnemyContactfunc());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Zone"){
            //Drehe um Wenn Zone berührt wird
            X *= -1;
            Y*= -1;
        }
    }

    public void Weapon_Follow_Player(){
        Weapons.transform.position = transform.position;
        Weapons.transform.rotation = transform.rotation;
    }

    public IEnumerator Bot_Random_Move(){
        for(Zyklus = 1; ; Zyklus++){
            X =  Random.Range(-1f,1f);
            Y = Random.Range(-1f,1f);
            if(Zyklus == 10) BewegungsängerungsZeit = 15f;
            yield return new WaitForSeconds(BewegungsängerungsZeit);
        }
    }

    public IEnumerator EnemyContactfunc(){
        //Bei Spieler sichutung Augen Kontakt halten
        if(EnemyContact == true){
            if(Z == 1 && waiting == false){
                waiting = true;
                yield return new WaitForSeconds(1.2f);
                Z++;
                waiting = false;
            }
            Vector2 lookDir = new Vector2(PlayerTransform.position.x, PlayerTransform.position.y) - rb.position; 
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 270f; 
            rb.rotation = angle;
        }else{
            //Sonst immer in die lauf richtung schauen.
            if(X != 0 || Y != 0){
            transform.up = new Vector2(X,Y);
            }
        }
    }
}

