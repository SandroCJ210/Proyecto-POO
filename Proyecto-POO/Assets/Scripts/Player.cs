using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Shooting Mechanic
    [Header("Shooting Mechanic")]
    [SerializeField]
    private Transform firePivot;
    [SerializeField]
    private GameObject bulletPrefab;
    
    
    #endregion
    #region Movement and Physics
    [Header("Movement and Physics")]
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float timeToStop = 0.1f;
    private Rigidbody2D rb;
    private float xAxis;
    private float yAxis;
    private Vector2 movementVector;
    public Vector2 inputVector;
    private Vector2 smoothVelocity;
    #endregion
    #region State Variables
    public bool isWalking;
    #endregion
    #region Events
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        GetInput();
        Shoot();
    }
    void FixedUpdate()
    {
        movementVector = Vector2.SmoothDamp(movementVector, inputVector.normalized, ref smoothVelocity, timeToStop);
        Move();
    }
    #endregion
    #region Functions
    void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        
        inputVector = new Vector2(xAxis, yAxis);
    }
    void Move()
    {
        rb.velocity = new Vector2(movementVector.x, movementVector.y) * speed;
        if (rb.velocity.magnitude < 1)
        {
            isWalking = false;
            rb.velocity = Vector2.zero;
        }
        else
        {
            isWalking = true;
        }
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
                Instantiate(bulletPrefab, firePivot.position, firePivot.rotation);
        }
    }
    #endregion
}
