using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour
{
    public float distance = 1.0f;
    public TextMesh textAssist;
    private TextMesh newTextAssist;
    private GameObject player;
    private bool assistTextDisplayed = false;
    private bool interaction = false;
    private Vector3 savedPosition;
    private Quaternion savedRotation;
    private Animator anim;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = player.GetComponent<Animator>();

    }

    void Update()
    {

        if(newTextAssist != null){
            newTextAssist.transform.rotation = GameObject.FindWithTag("3rdPersonCamera").GetComponent<Camera>().transform.rotation;
        }


        if (Vector3.Distance(player.transform.position, transform.position) <= distance){
            if(newTextAssist == null){
                newTextAssist = Instantiate(textAssist, transform.position, Quaternion.identity);
                newTextAssist.text = "Press E to interact with the " + transform.name;
                interaction = true;
            }
        }
        else{
            if (newTextAssist != null){
                interaction = false;
                Destroy(GameObject.Find("TextAssist(Clone)"));
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && interaction == true){
            toggleInteraction();
        }

    }

    public void toggleInteraction(){
        switch (transform.name){
            case "Treadmill":
                interactWithTreadmill();
                break;
            case "Exercise Bike":
                interactWithBike();
                break;
        }
    }

    public void interactWithTreadmill(){
        interaction = false;
        hideTextAssist();

        player.GetComponent<Controls>().enabled = false;

        anim.SetBool("isWalking", false);

        Transform canvas = GameObject.Find("Canvas").GetComponent<Canvas>().transform;
        canvas.Find("TreadmillDialog").gameObject.SetActive(!canvas.Find("TreadmillDialog").gameObject.activeSelf);

        GameObject.FindWithTag("3rdPersonCamera").GetComponent<Camera>().enabled = false;
        GameObject.Find("Treadmill/Camera").GetComponent<Camera>().enabled = true;

        transform.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;

        savedPosition = player.transform.position;
        savedRotation = player.transform.rotation;

        player.transform.position = new Vector3(-0.86f, 0.03f, 0.67f);
        player.transform.rotation = Quaternion.Euler(0, 149.25f, 0);

    }

    public void interactWithBike()
    {
        interaction = false;
        hideTextAssist();

        player.GetComponent<Controls>().enabled = false;

        anim.SetBool("isWalking", false);
        anim.SetBool("onBike", true);

        Transform canvas = GameObject.Find("Canvas").GetComponent<Canvas>().transform;
        canvas.Find("BikeDialog").gameObject.SetActive(!canvas.Find("BikeDialog").gameObject.activeSelf);

        GameObject.FindWithTag("3rdPersonCamera").GetComponent<Camera>().enabled = false;
        GameObject.Find("Exercise Bike/Camera").GetComponent<Camera>().enabled = true;

        transform.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;

        savedPosition = player.transform.position;
        savedRotation = player.transform.rotation;

        player.transform.position = new Vector3(-0.799f, 0.22f, -0.996f);
        player.transform.rotation = Quaternion.Euler(0, 151.82f, 0);

    }

    public void triggerWalkOnTreadmill()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", true);
    }

    public void triggerJogOnTreadmill(){
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", true);
    }

    public void hideTextAssist(){
        if(newTextAssist != null){
            newTextAssist.color = new Color(newTextAssist.color.r,
                                        newTextAssist.color.g,
                                        newTextAssist.color.b,
                                        0.0f);
        }

    }

    public void displayTextAssist()
    {
        if(newTextAssist!=null){
            newTextAssist.color = new Color(newTextAssist.color.r,
                                        newTextAssist.color.g,
                                        newTextAssist.color.b,
                                        1.0f);
        }

    }

    public void dismissDialog(){
        interaction = true;
        displayTextAssist();
        player.GetComponent<Controls>().enabled = true;

        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("onBike", false);

        Transform canvas = GameObject.Find("Canvas").GetComponent<Canvas>().transform;
        canvas.Find("TreadmillDialog").gameObject.SetActive(false);
        canvas.Find("BikeDialog").gameObject.SetActive(false);

        GameObject.Find("Treadmill/Camera").GetComponent<Camera>().enabled = false;
        GameObject.Find("Exercise Bike/Camera").GetComponent<Camera>().enabled = false;
        GameObject.FindWithTag("3rdPersonCamera").GetComponent<Camera>().enabled = true;

        transform.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<Rigidbody>().useGravity = true;

        player.transform.position = savedPosition;
        player.transform.rotation = savedRotation;
    }


}