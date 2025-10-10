using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : Character
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _moveSpeed;
    [Header("Ray settings")]
    public float footOffset = -0.1f;          // khoảng từ tâm player xuống chân
    public float rayLength = 1.5f;           // độ dài tia về phía trước
    public float sphereRadius = 0.1f;        // dùng cho SphereCast (ổn định hơn)
    public LayerMask hitLayers;              // chọn layer cần kiểm tra (VD: Obstacle)

    [Header("Gizmo / Debug")]
    public bool drawDebug = true;


    public bool isGrounded;

    CapsuleCollider _caps;
    void Awake()
    {
        _caps = GetComponent<CapsuleCollider>();
        isGrounded = false;
    }

    void Update()
    {
        Vector3 origin = new Vector3(
       transform.position.x,
       _caps.bounds.min.y + 0.05f,
       transform.position.z
    );

        Vector3 dir = transform.forward;
        isGrounded = Physics.Raycast(origin, dir, out RaycastHit hit, rayLength, hitLayers);

        if (drawDebug)
        {
            Debug.DrawRay(origin, dir * rayLength, isGrounded ? Color.green : Color.red);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isIdle", false);
        }
        else
        {
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isIdle", true);
        }
    }
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Wall"))
    //     {
    //         if (!isGrounded)
    //         {
    //             other.gameObject.SetActive(false);
    //         }
    //     }
    // }
}

