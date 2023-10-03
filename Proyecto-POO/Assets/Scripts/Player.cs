using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Shooting Mechanic
    [Header("Shooting Mechanic")]
    [SerializeField]
    Transform firePivot;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float timeBetweenShoots;
    [SerializeField]
    private int handGunBullets = 12;
    private Animator an;
    private float nextTimetoShoot;
    
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
        an = GetComponent<Animator>();
    }
    private void Update()
    {
        GetInput();
        /*
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Rotation();
        }*/
        Shoot();
        Reload();
    }
    void FixedUpdate()
    {
        movementVector = Vector2.SmoothDamp(movementVector, inputVector.normalized, ref smoothVelocity, timeToStop);
        Move();
    }
    private void LateUpdate()
    {
        if (isWalking)
        {
            an.SetBool("isWalking", true);
        }
        else
        {
            an.SetBool("isWalking", false);
        }
    }
    #endregion
    void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        if (xAxis < 0 && transform.right.x > 0)
        {
            transform.right = transform.right * -1;
        }
        else if (xAxis > 0 && transform.right.x < 0)
        {
            transform.right = transform.right * -1;
        }
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
        if (Input.GetMouseButtonDown(0) && handGunBullets !=0)
        {
            if (Time.time > nextTimetoShoot)
            {
                nextTimetoShoot = Time.time + timeBetweenShoots;
                an.SetTrigger("Shoot");
                Instantiate(bulletPrefab, firePivot.position, firePivot.rotation);
                handGunBullets--;
            }
        }
    }
    void Reload()
    {
        
        if (Input.GetKeyDown(KeyCode.R) || handGunBullets == 0)
        {
            an.SetTrigger("Reload");
            StartCoroutine(WaitReload());
            handGunBullets = 12;
        }
    }
    IEnumerator WaitReload()
    {
        yield return new WaitForSeconds(1);
    }
}
