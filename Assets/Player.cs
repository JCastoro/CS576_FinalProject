using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isCrouching;
    public bool isPressingTab;
    public bool isTakingPicture;
    public CharacterController characterController;
    public Transform CameraTransform;

    private float walkingVelocity = 5;
    float velocity;

    private float sensitivity = 400;
    private float xRotation, yRotation;

    // I think in order to keep reference to this i have to declare it from Unity
    public GameObject Scrapbook;


    void Start(){
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
        MoveCamera();
        MovePlayer();

    }

    void MoveCamera(){
        // Camera Position
        if (isCrouching){
            CameraTransform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        } else {
            CameraTransform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }

        // Camera Rotation
        yRotation += Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        xRotation -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        CameraTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    void MovePlayer(){
        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        Vector3 motion = transform.forward * VerticalInput + transform.right * HorizontalInput;
        velocity = isCrouching ? walkingVelocity/2 : walkingVelocity;
        characterController.Move(motion.normalized * velocity * Time.deltaTime);
    }
}
