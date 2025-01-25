using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 

    [SerializeField] float moveSpeed;
    [SerializeField] float strafeSpeed;
    [SerializeField] float jumpPower;

    private float defaultSpeed = 0f;
    private float defaultStrafe = 0f;
    
    bool isGrounded;

    public float groundDrag = 20f;
    private float airMulti = 0.005f;
    float heightCheck = 1.2f;
    bool isJumping;

    Rigidbody rb;      

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultSpeed = moveSpeed;
        defaultStrafe = strafeSpeed;
    }

    private void Update()
    {
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, heightCheck);
        playerIsGrounded();
        if (isGrounded & Input.GetKey(KeyCode.Space)) {
            isJumping = true;
        }
        else if(!isGrounded)
        {
            moveSpeed = airMulti;
            strafeSpeed = airMulti;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(transform.forward * moveSpeed * 10, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * strafeSpeed * 10, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * moveSpeed * 10, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * strafeSpeed * 10, ForceMode.Impulse);
        }
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
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, heightCheck);
        
        //Debug.Log(playerGrounded);

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

}
