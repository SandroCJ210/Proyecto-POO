using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    public Vector2 InputVector { get; private set; }
    private Vector2 smoothVelocity;
    #endregion
    #region State Variables
    public HealthHeartBar healthHeartBar;
    private float health, maxHealth;
    public bool isWalking;
    #endregion
    #region Events
    private void Start()
    {
        maxHealth = 6;
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        GetInput();
    }
    void FixedUpdate()
    {
        movementVector = Vector2.SmoothDamp(movementVector, InputVector, ref smoothVelocity, timeToStop);
        Move();
    }
    #endregion
    #region Functions
    private void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        
        InputVector = new Vector2(xAxis, yAxis).normalized;
    }
    private void Move()
    {
        rb.velocity = new Vector2(movementVector.x,movementVector.y) * speed;
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
    public float GetHealth()
    {
        return health;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public void TakeDamage()
    {
        if (health > 0)
        {
            health -= 1;
            if(health < 0)
            {
                //GameOverScreen and reload level
            }
            healthHeartBar.DrawHearts();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            TakeDamage();
            healthHeartBar.DrawHearts();
        }
    }
    #endregion
}
