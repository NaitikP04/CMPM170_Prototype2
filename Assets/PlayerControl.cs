using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public enum PlatformType { Normal, Bouncy, jumpPad }
    public PlatformType equippedPlatformType = PlatformType.Normal;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
        ChangePlatformType();
    }

    void Move()
    {
        float moveHorizontal = -(Input.GetAxis("Horizontal"));
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ChangePlatformType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equippedPlatformType = PlatformType.Normal;
            Debug.Log("Equipped Normal Platform");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equippedPlatformType = PlatformType.Bouncy;
            Debug.Log("Equipped Bouncy Platform");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equippedPlatformType = PlatformType.jumpPad;
            Debug.Log("Equipped Sticky Platform");
        }
    }
}
