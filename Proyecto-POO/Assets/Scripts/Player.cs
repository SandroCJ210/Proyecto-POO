using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Shooting Mechanic")]
    [SerializeField]
    Transform firePivot;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float timeBetweenShoots;
    [SerializeField]
    BodyAnimation bodyAnimation;
    private float nextTimetoShoot;

    [Header("Movement and Physics")]
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float timeToStop = 0.1f;
    private Rigidbody2D rb;
    private float xAxis;
    private float yAxis;
    private Vector2 movementVector;
    private Vector2 inputVector;
    private Vector2 smoothVelocity;

    #region State Variables
    public bool isWalking;
    public bool isShooting;
    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        GetInput();
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Rotation();
        }
        Shoot();

    }
    void FixedUpdate()
    {
        movementVector = Vector2.SmoothDamp(movementVector, inputVector, ref smoothVelocity, timeToStop);
        Move();
    }
    void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(xAxis, yAxis);
    }
    void Move()
    {
        rb.velocity = new Vector2(movementVector.x, movementVector.y) * speed;
        if (rb.velocity.magnitude < 0.01f)
        {
            rb.velocity = Vector2.zero;
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }
    }
    void Rotation()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 distance = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > nextTimetoShoot)
            {
                isShooting = true;
                nextTimetoShoot = Time.time + timeBetweenShoots;
                bodyAnimation.ShootAnimation();
                Instantiate(bulletPrefab, firePivot.position, firePivot.rotation);
            }
        }
    }
}
