using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class RollerPlayer : MonoBehaviour
{
    [SerializeField, Range(0, 100)] float moveForce = 3;
    [SerializeField, Range(0, 50)] float jumpForce = 3;
    [SerializeField] Transform view;

    [Header("Ground Collision")]
    [SerializeField, Range(0, 5)] float rayLength = 1;
    [SerializeField] LayerMask groundLayerMask = Physics.AllLayers;

    Rigidbody rb;
    Vector2 inputMovement;

    InputAction moveAction;
    InputAction jumpAction;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        
        jumpAction.performed += OnJump;
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        
        jumpAction.performed -= OnJump;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(inputMovement.x, 0, inputMovement.y);
        rb.AddForce(movement * moveForce);
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        inputMovement = ctx.ReadValue<Vector2>();
    }
    
    private void OnJump(InputAction.CallbackContext ctx)
    {
        if(OnGround())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayerMask);
    }
}
