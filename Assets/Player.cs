using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isCrouching;
    public bool isPressingTab;
    public bool isTakingPicture;
    public CharacterController characterController;
    public Camera PlayerCamera;
    public AudioClip CameraClip;
    private AudioSource Source;

    private float walkingVelocity = 5;
    float velocity;

    private float sensitivity = 400;
    private float xRotation, yRotation;

    public GameObject Scrapbook;
    public GameObject GameManager;


    private string[] animalNames = {"Deer", "Boar", "Bear"};

    void Start(){
        Source = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isCrouching = false;
        Scrapbook.SetActive(false);
    }

    void Update(){
        isPressingTab = Input.GetKey("tab");
        isCrouching = Input.GetKey("left ctrl");
        isTakingPicture= Input.GetKey("mouse 1");
        //tab_pressed = Input.GetKey("tab");
        TakePicture();
        MoveCamera();
        MovePlayer();

    }

    void MoveCamera(){
        // Camera Position
        if (isCrouching){
            PlayerCamera.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        } else {
            PlayerCamera.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }

        // Camera Rotation
        yRotation += Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        xRotation -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        PlayerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    void MovePlayer(){
        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        Vector3 motion = transform.forward * VerticalInput + transform.right * HorizontalInput;
        velocity = isCrouching ? walkingVelocity/2 : walkingVelocity;
        characterController.Move(motion.normalized * velocity * Time.deltaTime);
    }

    string TakePicture(){
       if(isTakingPicture && Input.GetMouseButtonDown(0)){
            Source.PlayOneShot(CameraClip);
            Ray CameraRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit CameraHit;
            
            if (Physics.Raycast(CameraRay, out CameraHit)){
                int AnimalIdx = Array.IndexOf(animalNames, CameraHit.collider.tag);
                if (AnimalIdx != -1){
                    GameManager.GetComponent<GameManager>().CheckIfUpdateHUDandSB(AnimalIdx);
                    // Scrapbook.GetComponent<Scrapbook>().MarkAnimalFound(AnimalIdx);
                    // GameManager.GetComponent<GameManager>().SetHUDbyAnimalIndex(AnimalIdx+1);
                    Debug.Log("TAG: " + CameraHit.collider.tag);
                    return animalNames[AnimalIdx];
                }
            }
       }
       return null;
    }
}
