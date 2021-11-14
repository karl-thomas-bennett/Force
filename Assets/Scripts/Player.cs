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
    private InputAction mousePositionAction;
    private InputAction pullAction;
    private InputAction pushAction;
    private Rigidbody rb;

    public float speed;

    public Vector2 tilt;
    public float roll;
    public Vector2 mousePosition;
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

        mousePositionAction = playerInput.actions["Mouse"];
        mousePositionAction.performed += UpdateMousePosition;

        pullAction = playerInput.actions["Pull"];
        pullAction.started += Pull;

        pushAction = playerInput.actions["Push"];
        pushAction.started += Push;
    }

    private void OnDisable()
    {
        tiltAction.performed -= Tilt;
        tiltAction.canceled -= Tilt;

        rollAction.performed -= Roll;
        rollAction.canceled -= Roll;

        reverseAction.started -= Reverse;
        reverseAction.canceled -= Forward;

        pullAction.started -= Pull;
        pushAction.started -= Push;
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

    private void UpdateMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    private void Pull(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 toHit = hit.transform.position - transform.position;
            if(toHit.magnitude > 3)
            {
                float currentSpeed = rb.velocity.magnitude;
                float totalSpeed = speed + currentSpeed * 50;
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(toHit.normalized * totalSpeed);
                Rigidbody hitRB = hit.transform.GetComponent<Rigidbody>();
                hitRB.velocity = new Vector3(0, 0, 0);
                hitRB.AddForce(-toHit.normalized * totalSpeed);
            }
            
        }
    }

    private void Push(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 toHit = transform.position - hit.transform.position;
            float currentSpeed = rb.velocity.magnitude;
            float totalSpeed = speed + currentSpeed * 50;
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(toHit.normalized * totalSpeed);
            Rigidbody hitRB = hit.transform.GetComponent<Rigidbody>();
            hitRB.velocity = new Vector3(0, 0, 0);
            hitRB.AddForce(-toHit.normalized * totalSpeed);
        }
    }
}
