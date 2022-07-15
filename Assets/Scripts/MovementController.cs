using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D _rb;
    private Animator _animator;

    private Vector2 movement;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        _animator.SetFloat("Horizontal", movement.x);
        _animator.SetFloat("Vertical", movement.y);
        _animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movement.normalized * (moveSpeed * Time.fixedDeltaTime));
    }
}
