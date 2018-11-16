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

    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        }else{
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
         }
    }

    public void interactWithTreadmill(){
        interaction = false;
        hideTextAssist();

        player.GetComponent<Controls>().enabled = false;

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


    public void hideTextAssist(){
        newTextAssist.color = new Color(newTextAssist.color.r,
                                        newTextAssist.color.g,
                                        newTextAssist.color.b,
                                        0.0f);
    }

    public void displayTextAssist()
    {
        newTextAssist.color = new Color(newTextAssist.color.r,
                                        newTextAssist.color.g,
                                        newTextAssist.color.b,
                                        1.0f);
    }

    public void dismissDialog(){
        interaction = true;
        displayTextAssist();
        player.GetComponent<Controls>().enabled = true;

        Transform canvas = GameObject.Find("Canvas").GetComponent<Canvas>().transform;
        canvas.Find("TreadmillDialog").gameObject.SetActive(!canvas.Find("TreadmillDialog").gameObject.activeSelf);

        GameObject.Find("Treadmill/Camera").GetComponent<Camera>().enabled = false;
        GameObject.FindWithTag("3rdPersonCamera").GetComponent<Camera>().enabled = true;

        transform.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<Rigidbody>().useGravity = true;

        player.transform.position = savedPosition;
        player.transform.rotation = savedRotation;
    }

}