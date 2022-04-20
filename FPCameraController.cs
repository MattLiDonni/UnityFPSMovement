using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FPCameraController : MonoBehaviour
{
    
    static InputActionMap inputActions;
    [SerializeField] Camera PlayerCamera;

    Vector2 lookVector = new Vector2(0, 0);
    Vector3 currentVelocity = Vector3.zero;
    float camRot;

    //Look sensitivity
    [SerializeField] float sensitivity = 0.8f;

    private void Awake()
    {
        inputActions = gameObject.GetComponent<PlayerInput>().currentActionMap;
    }

    void Start()
    {
        inputActions = gameObject.GetComponent<PlayerInput>().currentActionMap;
        
        //Hides cursor, can be shown again by pressing ESC. 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        updateInputs();

        //Player Y Rotation
        transform.Rotate(0, lookVector.x * sensitivity, 0, 0);

        //Camera X Rotation
        camRot -= lookVector.y * sensitivity;
        camRot = Mathf.Clamp(camRot, -90f, 90f);
        PlayerCamera.transform.localEulerAngles = Vector3.right * camRot;
    }

    void updateInputs()
    {
        //Updates the current movement of the mouse, stores in lookVector.
        lookVector = inputActions.FindAction("Look").ReadValue<Vector2>();

    }

    //Unity Input System requires these two methods to be defined as they are.
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
