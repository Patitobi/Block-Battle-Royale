using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    Rigidbody2D rb;
    public FixedJoystick joystick;
    public Animator animatior;
    Vector3 lastpos;
    public float rotationSpeed;
    private Vector3 prevPosition = Vector3.zero;
    public GameObject Weapons;
    public Vector3 moveVector;
    public GameObject World;
    public Vector2 movement;    

    // Start is called before the first frame update
    void Start()
    {
        World = GameObject.Find("World");
        rb = GetComponent<Rigidbody2D>();
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
        animatior = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {   
        float X = joystick.Horizontal;
        float Y = joystick.Vertical;

        Vector2 movementDir = new Vector2(X,Y);

        transform.Translate(movementDir * speed * Time.fixedDeltaTime, Space.World);

        
        if(transform.position != lastpos){
            animatior.SetBool("isrunning", true);
        }else{
            animatior.SetBool("isrunning", false);
        }
        lastpos = transform.position;

        if(X != 0 || Y != 0){
        transform.up = new Vector2(X,Y);
        }
    }

    void Update(){
        Weapon_Follow_Player();
    }

    public void EnterCrouch(){
        speed = speed / 2;
    }

    public void ExitCrouch(){
        speed = speed * 2;
    }

    public void Weapon_Follow_Player(){
        Weapons.transform.position = transform.position;
        Weapons.transform.rotation = transform.rotation;
    }
}