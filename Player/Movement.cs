using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Rendering.Universal;
using Unity.Netcode;

public class Movement : NetworkBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public FixedJoystick joystick1;
    public FixedJoystick joystick2;
    public Animator animatior;
    Vector3 lastpos;
    public float rotationSpeed;
    private Vector3 prevPosition = Vector3.zero;
    public GameObject Weapons;
    public Vector3 moveVector;
    public GameObject World;
    public Vector2 movement; 
    public bool tap;
    public Touch touch;
    public Shoot Shoot;
    public GameObject Player;
    public bool crouch;
    public float RotateX, RotateY;
    private bool steping;
    public GameObject Footsteps;
    public GameObject Footsteps2;
    public int schleifenx;
    public bool lootable;
    public bool lootbutton;
    public string Player_Name_Ingame;
    public GameObject Name_Text;
    //public GameObject PostProcessingVolume, Wald;

    void Awake(){
        //Performance Settings from Menu
        /*if(Menu_Handler.performancemode == false){
            //-Low Performance settings-
            //Disable Post Processing
            PostProcessingVolume.SetActive(false);
        }else{
            PostProcessingVolume.SetActive(true);
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        if(IsOwner){
        GameObject.Find("Main Camera").GetComponent<no_Camera_rotation>().player = this.gameObject; //Camera fängt an zu folgen
        Player_Name_Ingame = Menu_Handler.Player_Name;
        Name_Text.GetComponent<TextMeshPro>().text = Player_Name_Ingame;
        World = GameObject.Find("World");
        rb = GetComponent<Rigidbody2D>();
        animatior = gameObject.GetComponent<Animator>();
        Shoot = Player.GetComponent<Shoot>();
        StartCoroutine(FootstepGen());
        joystick1 = GameObject.Find("Fixed Joystick Laufen").GetComponent<FixedJoystick>();
        joystick2 = GameObject.Find("Fixed Joystick Umschauen").GetComponent<FixedJoystick>();
        }
    }

    // Update is called once per frame
    void Update(){
        //Namen überm Kopf anzeigen
        //Name_Text.GetComponent<Rigidbody2D>().rotation = 0;
        //Name_Text.GetComponent<RectTransform>().position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.5f);
    }

    void FixedUpdate()
    {   
        if(IsOwner){
        float X = joystick1.Horizontal;
        float Y = joystick1.Vertical;

        Vector2 movementDir = new Vector2(X,Y);

        transform.Translate(movementDir * speed * Time.fixedDeltaTime, Space.World);

        
        if(transform.position != lastpos){
            animatior.SetBool("isrunning", true);
            steping = true;
        }else{
            animatior.SetBool("isrunning", false);
            steping = false;
        }
        lastpos = transform.position;

        //Rotation für rotationsstick setzen
        if(joystick2.Horizontal != 0f || joystick2.Vertical != 0f){
            RotateY = joystick2.Vertical;
            RotateX = joystick2.Horizontal;
        }else{
            RotateX = 0f;
            RotateY = 0f;
        }

        //in die richtung drehen in die man läuft falls roatations stick nicht bewegt wird
        if(X != 0 || Y != 0 && new Vector2(RotateX,RotateY) == Vector2.zero){
            transform.up = new Vector2(X,Y);
        }

        if(new Vector2(RotateX,RotateY) != Vector2.zero){
            transform.up = new Vector2(RotateX,RotateY);
        }
        }
    }

    private IEnumerator FootstepGen(){
        begin:
        for( ;steping; ){
            Instantiate(Footsteps,new Vector3(transform.position.x, transform.position.y, 121f), transform.rotation);
            yield return new WaitForSeconds(.3f);
            if(!steping) break;
            Instantiate(Footsteps2, new Vector3(transform.position.x, transform.position.y, 121f), transform.rotation);
            yield return new WaitForSeconds(.3f);
        }
        if(!steping){
            yield return new WaitForSeconds(.3f);
            goto begin;
        }
    }

    public void enterlooting(){
        lootbutton = true;
    }

    public void exitlooting(){
        lootbutton = false;
    }
}