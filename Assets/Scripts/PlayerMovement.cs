 using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private InputHandler _inputHandler;
    private CharacterController _cc;
    private Transform _cam;
    
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float turnSmoothTime = 0.1f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundLayer;

    bool _isDashing = false;
    bool _canDash = true;
    float _dashTimer = 0f;

    private Vector2 _moveInput;
    private float _verticalVelocity;
    private float _turnSmoothVelocity;
    
    void Start()
    {
        _cam = Camera.main.transform;
        _cc = GetComponent<CharacterController>();
        _inputHandler = GetComponent<InputHandler>();
        _inputHandler.OnJumpEvent += OnJump;
        _inputHandler.OnDashEvent += OnDash;
        _inputHandler.OnMoveEvent += (moveInput) => _moveInput = moveInput;
    }

    void Update()
    {
        Vector3 horizontalVelocity = Vector3.zero;

        if (IsGrounded() && _verticalVelocity < 0f) _verticalVelocity = -2f; // small push keeps the controller grounded

        _verticalVelocity += playerStats.gravity * Time.deltaTime;

        if(_isDashing)
        {
            Vector3 dashDirection = GetHorizontalVelocity().magnitude > 0 ? GetHorizontalVelocity().normalized : transform.forward;
            horizontalVelocity = dashDirection * playerStats.DashAmount;
            _verticalVelocity = 0f; // prevent vertical movement during dash
            if (Time.time - _dashTimer >= playerStats.DashDuration)
            {
                _isDashing = false;
            }
        }
        else
        {
            horizontalVelocity = GetHorizontalVelocity();               
        }   
        _cc.Move((horizontalVelocity + Vector3.up * _verticalVelocity) * Time.deltaTime);   
       
    }
    void OnDash()
    {
        _canDash = Time.time - _dashTimer >= playerStats.DashCooldown && !_isDashing;
        if (!_canDash) return;
        _isDashing = true;
        _dashTimer = Time.time;
    }
    Vector3 GetHorizontalVelocity()
    {
        Vector3 input = Vector3.ClampMagnitude(new Vector3(_moveInput.x, 0f, _moveInput.y), 1f);
        if (input.sqrMagnitude < 0.0001f) return Vector3.zero;

        float targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 dir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        
        return dir * (playerStats.Speed * input.magnitude);
    }
    void OnJump()
    {
        if (IsGrounded()) _verticalVelocity = Mathf.Sqrt(playerStats.JumpHeight * -2f * playerStats.gravity);
    }
    bool IsGrounded() => Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, groundLayer);
    void OnDisable()
    {
        _inputHandler.OnJumpEvent -= OnJump;
        _inputHandler.OnDashEvent -= OnDash;
    }
    void OnDrawGizmosSelected()
    {
        if (groundCheckPosition == null) return;
        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
    } 
}