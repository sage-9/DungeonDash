using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float turnSmoothTime = 0.1f;


    private PlayerInput _playerControls;
    private CharacterController _characterController;
    
    private float speed;
    private float turnSmoothVelocity;
    
    private Vector2 _playerinput;

    void Awake()
    {
        _playerControls = new PlayerInput();
        _playerControls.Player.Enable();
        _playerControls.Player.Movement.performed += ctx => _playerinput = ctx.ReadValue<Vector2>();
        _playerControls.Player.Movement.canceled += ctx => _playerinput = Vector2.zero;
   
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        speed = playerStats.Speed;
    }

    void Update()
    {
        MovePlayer(_playerinput);
    }

    
    void MovePlayer(Vector2 input)
    {
        float horizontalInput = input.x;
        float verticalInput = input.y;

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;


        if (movement != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDirection.normalized * speed * Time.deltaTime);

        }
    }
}
