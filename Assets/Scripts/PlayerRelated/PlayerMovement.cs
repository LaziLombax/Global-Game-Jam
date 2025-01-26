using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 

    [SerializeField] float moveSpeed;
    [SerializeField] float strafeSpeed;
    [SerializeField] float jumpPower;

    private float defaultSpeed = 0f;
    private float defaultStrafe = 0f;
    
    bool isGrounded;
    private bool isMoving;

    public float groundDrag = 20f;
    private float airMulti = 0.005f;
    float heightCheck = 1.2f;
    bool isJumping;

    //slope detection
    private float maxSlopeAngle = 60f;
    private RaycastHit slopeHit; //detects whether slope has been interacted with
    private bool slopExit;

    private Vector3 moveDirection = Vector3.zero;
    
    Rigidbody rb;

    private string footprintsClip = "playerFoot";
    private AudioSource asfp;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultSpeed = moveSpeed;
        defaultStrafe = strafeSpeed;

        asfp = GameManager.gm.audioLib.AddNewAudioSourceFromStandard("Player", gameObject, footprintsClip);
    }

    private void Update()
    {
        bool ishitting = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, heightCheck);
        playerIsGrounded();
        if (isGrounded & Input.GetKey(KeyCode.Space)) {
            isJumping = true;
            slopExit = false;
        }
        else if(!isGrounded)
        {
            
            moveSpeed = airMulti;
            strafeSpeed = airMulti;
        }
        
        // if (hit.collider.gameObject.layer == 3 && !isMoving)
        // {
        //     rb.linearVelocity = Vector3.zero;
        //     rb.angularVelocity = Vector3.zero;
        // }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.W))
        {

            moveDirection = transform.forward;
            // rb.AddForce(moveDirection * moveSpeed * 10, ForceMode.Impulse);
            isMoving = true;
        }
        
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -transform.right; 
            
            //rb.AddForce(moveDirection * strafeSpeed * 10, ForceMode.Impulse);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection = -transform.forward;
            
            //rb.AddForce(moveDirection * moveSpeed * 10, ForceMode.Impulse);
            isMoving = true;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = transform.right;
            
            //rb.AddForce(moveDirection * strafeSpeed * 10, ForceMode.Impulse);
            isMoving = true;
        }
        else moveDirection = Vector3.zero;
        
        if (Onslope() && !slopExit)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 30f, ForceMode.Force);

            if (rb.linearVelocity.y > 0) rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            
            Debug.LogWarning($"we on slope");

        }
        
        rb.AddForce(moveDirection * moveSpeed * 10, ForceMode.Impulse);
        
        
        
        if (isJumping)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJumping = false;
        }
    }
    
    void playerIsGrounded()
    {
        //checking to see if player is grounded
        // isGrounded = Physics.Raycast(transform.position + new Vector3(0f, 1.3f, 0f), Vector3.down, playerHei * 0.5f + 0.2f, PlayerData._grounded);
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, heightCheck);
        
        // //Debug.Log(playerGrounded);
        // if (hit.collider.gameObject.layer == 3 && !isMoving)
        // {
        //     rb.linearVelocity = Vector3.zero;
        // }
        //manipulates drag depending on if grounded or not
        if (isGrounded)
        {
            moveSpeed = defaultSpeed;
            strafeSpeed = defaultStrafe;
            //jumpReady();
            rb.linearDamping = groundDrag;
        }
        else rb.linearDamping = 0;
    }
    
    bool Onslope() //returns either true or false
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, heightCheck))
        {
            //calculation of the angle 
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        //if there is no slope
        return false;
    }

    Vector3 GetSlopeMoveDirection(Vector3 movementDirection)
    {
        return Vector3.ProjectOnPlane(movementDirection, slopeHit.normal).normalized;
    }

}
