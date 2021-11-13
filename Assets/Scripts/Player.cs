using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInput playerInput;
    private InputAction tiltAction;
    private InputAction rollAction;
    private InputAction reverseAction;
    private Rigidbody rb;
    public Vector2 tilt;
    public float roll;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        tiltAction = playerInput.actions["Tilt"];
        tiltAction.performed += Tilt;
        tiltAction.canceled += Tilt;

        rollAction = playerInput.actions["Roll"];
        rollAction.performed += Roll;
        rollAction.canceled += Roll;

        reverseAction = playerInput.actions["Reverse-Camera"];
        reverseAction.started += Reverse;
        reverseAction.canceled += Forward;
    }

    private void OnDisable()
    {
        tiltAction.performed -= Tilt;
        tiltAction.canceled -= Tilt;

        rollAction.performed -= Roll;
        rollAction.canceled -= Roll;

        reverseAction.started -= Reverse;
        reverseAction.canceled -= Forward;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.AddRelativeTorque(new Vector3(tilt.y, tilt.x, -roll));
    }



    private void Tilt(InputAction.CallbackContext context)
    {
        tilt = context.ReadValue<Vector2>();
    }

    private void Reverse(InputAction.CallbackContext context)
    {
        Camera.main.transform.localPosition = new Vector3(0, 2, 5);
        Camera.main.transform.Rotate(new Vector3(20, 180, 0));
    }

    private void Forward(InputAction.CallbackContext context)
    {
        Camera.main.transform.localPosition = new Vector3(0, 2, -5);
        Camera.main.transform.Rotate(new Vector3(20, 180, 0));
    }

    private void Roll(InputAction.CallbackContext context)
    {
        roll = context.ReadValue<float>();
    }
}
