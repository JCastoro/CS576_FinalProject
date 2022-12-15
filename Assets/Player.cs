using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isCrouching;
    public bool isTakingPicture;
    public float walking_velocity;
    public Vector3 movement_direction;
    public Transform orientation;
    public CharacterController characterController;
    GameObject cameraPos;
    float velocity;
    float horizontalInput;
    float verticalInput;
    private GameObject Scrapbook;


    // Start is called before the first frame update
    void Start(){
        cameraPos = GameObject.Find("/Player/CameraPos");
        isCrouching = false;
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        GameObject Scrapbook = GameObject.FindWithTag("Scrapbook");
        Scrapbook.SetActive(false);
        Debug.Log(Scrapbook);
    }

    // Update is called once per frame
    void Update(){

        //bools so we can use for animation stuff
        MovePlayer();
        DisplayScrapbook();

    }

    void MovePlayer(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        movement_direction = orientation.forward * verticalInput + orientation.right * horizontalInput;

        bool ctrl_pressed = Input.GetKey("left ctrl");
        if (ctrl_pressed){
            isCrouching = true;
            velocity = walking_velocity / 2;
            cameraPos.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
        else{
            isCrouching = false;
            velocity = walking_velocity;
            cameraPos.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
        characterController.Move(movement_direction.normalized * velocity * Time.deltaTime);
     
    }

    void DisplayScrapbook(){
        bool tab_pressed = Input.GetKey("tab");
        if (tab_pressed){    
            Debug.Log(Scrapbook);
            //Scrapbook.SetActive(true);
        }
        else{
            //Scrapbook.SetActive(false);
        }

    }
}
