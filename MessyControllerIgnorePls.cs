using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MessyControllerIgnorePls : MonoBehaviour
{

    static InputActionMap inputActions;
    Rigidbody rb;
    [SerializeField] Camera cam;
    
    Vector2 moveVector = new Vector2(0,0);
    Vector2 lookVector = new Vector2(0, 0);
    Vector3 currentVelocity = Vector3.zero;
    float camRot;

    [SerializeField] float speed = 2f, sensitivity = 0.8f;

    private void Awake()
    {
        inputActions = gameObject.GetComponent<PlayerInput>().currentActionMap;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Start() {
        inputActions = gameObject.GetComponent<PlayerInput>().currentActionMap;
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        updateInputs();


        //Player Y Rotation
        transform.Rotate(0, lookVector.x * sensitivity, 0, 0);

        //Camera X Rotation
        camRot -= lookVector.y * sensitivity;
        camRot = Mathf.Clamp(camRot, -90f, 90f);
        cam.transform.localEulerAngles = Vector3.right * camRot;
        

        



    }

    private void FixedUpdate()
    {
        //Moving the player
        currentVelocity = rb.velocity;
        Vector3 toMove = new Vector3(moveVector.x * speed + currentVelocity.x, currentVelocity.y, moveVector.y * speed + currentVelocity.z);
        rb.AddForce(toMove.x, toMove.y, toMove.z, ForceMode.Impulse);


    }



    void updateInputs() {
        //Move
        moveVector = inputActions.FindAction("Move").ReadValue<Vector2>();

        //Look
        lookVector = inputActions.FindAction("Look").ReadValue<Vector2>();

    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
