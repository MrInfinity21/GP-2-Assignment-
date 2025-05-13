using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;

    [Header("Movement")] 
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private Transform cameraTransform;
    
    private Vector3 velocity;
    private bool isGrounded;
    
    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;
    
    [Header("Rotation")]
    [SerializeField] private float turnSmoothTime = 0.1f;
    privte float turnSmoothVelocity;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        HandleJump();
    }

    void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance,groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(cameraTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            cameraTransform.rotation = Quaterion.Euler(0f, targetAngle, 0f);
            *Vector3.forward;
            Vector3 targetVelocity = moveDir.normalized * movementSpeed;

            velocity.x = targetVelocity.x;
            velocity.z = targetVelocity.z;
        }
        else
        {
            velocity.x = 0;
            velocity.z = 0;
        }

        velocity.y += gravity * Time.deltaTime;
        _controller.Move(velocity * TimedeltaTime);
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
