using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : Character
{

    private Rigidbody _rigidBody;

    [SerializeField]
    private float movingForce = 20f;

    [SerializeField]
    private float jumpForce = 80f;

    [SerializeField]
    private float maxSlope = 30f;

    [SerializeField]
    private float damping = 0.3f;

    [SerializeField]
    private float maxSpeed = 50.0f;

    private bool isOnGround = false;

    void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            Debug.Log("");
            isOnGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isOnGround = CheckIsOnGround(collision);
    }

    void Update()
    {
        UpdateTimer();

        LookAtTarget();

        Shoot();
    }

    private Vector3 GetShootDirection(Vector3 shootDirection, Vector3 gunPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetDirection = hit.point - gunPosition;
            if (Vector3.Angle(shootDirection, targetDirection) < 45)
            {
                shootDirection = targetDirection;
            }
        }

        return shootDirection;
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var shootDirection = GetShootDirection(transform.forward, gun.position);
            ShootBullet(shootDirection);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            ShootRocket();
        }
    }

    void FixedUpdate()
    {
        if (isOnGround)
        {
            ApplyMovingForce();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidBody.AddForce(Vector3.up * jumpForce);
            }
            else
            {
                _rigidBody.velocity = Vector3.ClampMagnitude(
                    _rigidBody.velocity, maxSpeed);
            }
        }
    }

    private bool CheckIsOnGround(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) 
        {
            return true;
        }

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.point.y < transform.position.y)
            {
                return Vector3.Angle(contact.normal, Vector3.up) < maxSlope;
            }
        }

        return false;
    }

    private void ApplyMovingForce()
    {
        Vector3 xAxisForce = transform.right * Input.GetAxis("Horizontal");
        Vector3 zAxisForce = transform.forward * Input.GetAxis("Vertical");

        Vector3 resultXZForce = xAxisForce + zAxisForce;
        if (resultXZForce.magnitude > 0)
        {
            resultXZForce.Normalize();
            _rigidBody.AddForce(resultXZForce * movingForce);
        }
        else
        {
            Vector3 dampedVel = _rigidBody.velocity * damping;
            dampedVel.y = _rigidBody.velocity.y;
            _rigidBody.velocity = dampedVel;
        }
    }

    private void LookAtTarget()
    {
        var plane = new Plane(Vector3.up, transform.position);

        float distance;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out distance))
        {
            Vector3 position = ray.GetPoint(distance);
            transform.LookAt(position);
        }
    }

    private void Destroyed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
