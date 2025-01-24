using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 

    [SerializeField] float moveSpeed;
    [SerializeField] float strafeSpeed;
    [SerializeField] float jumpPower;

    bool isGrounded;
    float heightCheck = 1.3f;
    bool isJumping;

    Rigidbody rb;      

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, heightCheck);
        if (isGrounded & Input.GetKey(KeyCode.Space)) {
            isJumping = true;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) {
            transform.position += transform.forward * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * strafeSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * strafeSpeed;
        }
        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJumping = false;
        }
    }
}
