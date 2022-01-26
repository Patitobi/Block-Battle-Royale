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
    

    // Start is called before the first frame update
    void Start()
    {
        Weapons = GameObject.Find("Weapons");
        rb = GetComponent<Rigidbody2D>();
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
        animatior = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector2 movementDirection = new Vector2(horizontalInput,verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        
        if(transform.position != lastpos){
            animatior.SetBool("isrunning", true);
        }else{
            animatior.SetBool("isrunning", false);
        }
        lastpos = transform.position;

        moveVector = (Vector3.up * joystick.Vertical - Vector3.left * joystick.Horizontal);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        }
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