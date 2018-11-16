using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public Rigidbody rb;
    public float verticalSpeed;
    public float horizontalSpeed;

    public float rot;
    public float speed;


    Animator anim;

    private void Awake() {
        this.enabled = true;
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update(){
        float mouseInputX = horizontalSpeed * Input.GetAxis("Mouse X");
        float mouseInputY = -verticalSpeed * Input.GetAxis("Mouse Y");

        Vector3 lookhere = new Vector3(mouseInputY, mouseInputX, 0);
        transform.Rotate(lookhere);


        //Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)
        //Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
           Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            anim.SetBool("isWalking", true);
        }else{
            anim.SetBool("isWalking", false);
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * rot * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
        transform.Translate(movement);

        Vector3 rotation = new Vector3(0.0f, moveHorizontal, 0.0f);
        transform.Rotate(rotation);
    }

}
