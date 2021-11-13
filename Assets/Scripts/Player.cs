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
    public Vector2 tilt;
    public float roll;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        tiltAction = playerInput.actions["Tilt"];
        tiltAction.performed += Tilt;
        tiltAction.canceled += Tilt;
        rollAction = playerInput.actions["Roll"];
        rollAction.performed += Roll;
        rollAction.canceled += Roll;
    }

    private void OnDisable()
    {
        tiltAction.performed -= Tilt;
        tiltAction.canceled -= Tilt;
        rollAction.performed -= Roll;
        rollAction.canceled -= Roll;
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
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddRelativeTorque(new Vector3(tilt.y, tilt.x, -roll));
    }



    private void Tilt(InputAction.CallbackContext context)
    {
        tilt = context.ReadValue<Vector2>();
    }

    private void Roll(InputAction.CallbackContext context)
    {
        roll = context.ReadValue<float>();
    }
}
