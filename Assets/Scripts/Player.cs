using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInput playerInput;
    private InputAction tiltAction;
    public Vector2 tilt;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        tiltAction = playerInput.actions["Tilt"];
        tiltAction.performed += Tilt;
        tiltAction.canceled += Tilt;
    }

    private void OnDisable()
    {
        tiltAction.performed -= Tilt;
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
        rigidbody.angularVelocity = new Vector3(tilt.y, tilt.x, 0);
    }



    private void Tilt(InputAction.CallbackContext context)
    {
        tilt = context.ReadValue<Vector2>();
    }
}
