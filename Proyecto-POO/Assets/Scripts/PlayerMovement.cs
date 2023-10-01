using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement and Physics")]
    private Rigidbody2D rb;
    private float xAxis;
    private float yAxis;
    private Vector2 movementVector;
    private Vector2 inputVector;
    private Vector2 smoothVelocity;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float timeToStop = 0.1f;
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
        }else
        {
            Debug.Log("Debug");
        }
        
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
        }
    }
    void Rotation()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 distance = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
    //Comentario de Albert
}

