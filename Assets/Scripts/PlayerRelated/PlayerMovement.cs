using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 

    [SerializeField] float moveSpeed;
    [SerializeField] float strafeSpeed;
    [SerializeField] float jumpPower;

    private float defaultSpeed = 0f;
    private float defaultStrafe = 0f;
    
    public bool isGrounded;
    private bool isMoving;
    private Transform playerDirection;

    public float groundDrag = 20f;
    private float airMulti = 0.005f;
    float heightCheck = 1f;
    bool isJumping;
    public bool sloping;

    //slope detection
    private float maxSlopeAngle = 80f;
    private RaycastHit slopeHit; //detects whether slope has been interacted with
    private bool slopExit;

    
    Rigidbody rb;


    public bool exitingSlope = false;
    public float maxYSpeed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultSpeed = moveSpeed;
        defaultStrafe = strafeSpeed;
        playerDirection = Camera.main.transform;
        inputHandler = InputHandler.Instance;
    }

    private void Update()
    {
        Drawings();
        sloping = Onslope();
        bool ishitting = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, heightCheck);
        MyInput();
        playerIsGrounded();

        // Handles drag based on player state
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        } else
        {
            rb.linearDamping = 0;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        
        SpeedControl();
    }
    private Vector3 moveInput;
    private InputHandler inputHandler;
    private Vector2 playerMovement;
    private Vector3 moveDirection;

    private void MovePlayer()
    {
        Vector3 force = Vector3.zero;
        moveDirection = playerDirection.forward * moveInput.z + playerDirection.right * moveInput.x;
        moveDirection.y = 0f;
        // Move the player using Rigidbody
        if (Onslope() && !exitingSlope)
        {
            force = GetSlopeMoveDireciton() * moveSpeed *10f;
            rb.AddForce(force, ForceMode.Force);
            //if (rb.linearVelocity.y > 0)
            //{
            //    force = Vector3.down * 80f;
            //    rb.AddForce(force, ForceMode.Force);
            //}
        }
        else if (isGrounded)
        {
            force = moveDirection.normalized * moveSpeed * 13f;
            rb.AddForce(force, ForceMode.Force);
        }
        else if (!isGrounded)
        {
            force = moveDirection.normalized * moveSpeed;
            rb.AddForce(force, ForceMode.Force);
        }
        rb.useGravity = !Onslope();

    }
    private void MyInput()
    {
        // Player Input
        #region Player Input and Jumping
        playerMovement = inputHandler.GetPlayerMovement();
        moveInput = new Vector3(playerMovement.x, 0f, playerMovement.y);

        if (inputHandler.PlayerJumped() && canJump)
        {
            Jump(jumpPower);
        }
        if (isGrounded && !canJump)
        {
            Invoke(nameof(ResetJump), 0.5f);
        }
        #endregion
    }

    // Applies a vertical force to the player when called.
    public void Jump(float liftForce)
    {
        if (Onslope())
        {
            exitingSlope = true;
        }
        canJump = false;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * liftForce, ForceMode.Impulse);
    }
    public bool canJump;
    // Resets the jump bools.
    private void ResetJump()
    {
        exitingSlope = false;
        canJump = true;
    }

    // Controls the movement speed
    private void SpeedControl()
    {
        if (Onslope() && !slopExit)
        {
            if (rb.linearVelocity.magnitude > moveSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
            }
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }

        if (maxYSpeed != 0 && rb.linearVelocity.y > maxYSpeed)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, maxYSpeed, rb.linearVelocity.z);
    }

    [SerializeField] private float sphereRadius = 0.5f;
    [SerializeField] private Vector3 sphereOffset = Vector3.zero;
    [SerializeField] private LayerMask groundLayer;

    void playerIsGrounded()
    {
        Vector3 spherePosition = transform.position + sphereOffset;
        isGrounded = Physics.CheckSphere(spherePosition, sphereRadius, groundLayer);
    }
    private bool Onslope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1f + 0.5f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }
    void Drawings()
    {
        // Start and end points
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.down * 5f; // Adjust the length as needed

        // Draw the line
        Debug.DrawLine(start, end, Color.blue);
    }

    private Vector3 GetSlopeMoveDireciton()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

}
