using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Vector2 directionToMove;

    #endregion
    #region State Variables
    public HealthHeartBar healthHeartBar;
    private float health, maxHealth;
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
        rb.velocity = new Vector2(movementVector.x, movementVector.y) * speed;
        if (rb.velocity.magnitude < 1)
        {
            rb.velocity = Vector2.zero;
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
            if(health <= 0)
            {
                SceneManager.LoadScene(0);
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

        if(collision.tag == "BDD")
        {
            directionToMove = Vector2.down;
            Debug.Log(Vector2.down);
        }
        if(collision.tag == "BDU")
        {
            directionToMove = Vector2.up;
            Debug.Log(Vector2.up);

        }
        if (collision.tag == "BDR")
        {
            directionToMove = Vector2.right;
            Debug.Log(Vector2.right);

        }
        if (collision.tag == "BDL")
        {
            directionToMove = Vector2.left;
            Debug.Log(Vector2.left);
        }
    }
    #endregion
}
