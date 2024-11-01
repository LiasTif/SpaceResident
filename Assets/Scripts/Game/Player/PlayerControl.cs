using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float _movSpeed;
    [SerializeField]
    private float _inertiaStrength;
    [SerializeField]
    private BrakeIcon _brakeIcon;

    private Rigidbody2D _rb;
    private Vector2 _inputDirection;
    private bool _isBraking;

    private PlayerInputActions _inputActions;

    public float CurrentSpeed => _rb.velocity.magnitude;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inputActions = new();

        _inputActions.Player.Move.performed += ctx => _inputDirection = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += ctx => _inputDirection = Vector2.zero;
        _inputActions.Player.Brake.performed += ctx => ToggleBraking();
    }

    private void ToggleBraking()
    {
        _isBraking = !_isBraking;
        _brakeIcon.ChangeStatus(_isBraking);

        Debug.Log($"Brake: {_isBraking}");
    }

    private void FixedUpdate()
    {
        if (_inputDirection.magnitude > 0)
        {
            Move();
            return;
        }
        else if (_isBraking && _inputDirection.magnitude == 0)
        {
            Brake();
            return;
        }

        MoveByInertia();
    }

    private void Move() => _rb.velocity += _movSpeed * Time.fixedDeltaTime * _inputDirection;
    private void Brake() => _rb.velocity = Vector2.MoveTowards(_rb.velocity, Vector2.zero, _movSpeed * Time.fixedDeltaTime);
    private void MoveByInertia() => _rb.velocity *= _inertiaStrength;

    private void OnEnable() => _inputActions.Player.Enable();
    private void OnDisable() => _inputActions.Player.Disable();
}