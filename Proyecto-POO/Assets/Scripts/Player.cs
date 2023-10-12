using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    #region CameraMovement
    public AnimationCurve curve;
    public Camera gameCamera;
    public float lerpDuration;
    private Vector3 gameCameraVector;
    #endregion
    #region State Variables
    public bool isWalking;
    #endregion
    #region Events
    void Start()
    {
        gameCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        float gameCameraHeight = 2 * gameCamera.orthographicSize;
        float gameCameraWidth = gameCameraHeight * gameCamera.aspect;
        gameCameraVector = new Vector3(gameCameraWidth, gameCameraHeight, gameCamera.transform.position.z);
        lerpDuration = 0.5f;
    }
    private void Update()
    {
        GetInput();
        Shoot();
        //DetectDoor();
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

    /*void DetectDoor()
    {
        float rayLength = 0.8f;
        Ray2D ray = new Ray2D(transform.position, inputVector);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.green);

        RaycastHit2D hit;

        if(hit = Physics2D.Raycast(transform.position, inputVector, rayLength))
        {
            if (hit.collider.tag == "Door" && inputVector.magnitude != Mathf.Sqrt(2))
            {
                MovePlayerToRoom();
                Vector3 actualCamPosition = gameCamera.transform.position;
                Vector3 targetCamPosition = actualCamPosition + Vector3.Scale(inputVector, gameCameraVector);
                StartCoroutine(MoveCamera(actualCamPosition, targetCamPosition, lerpDuration));

            }
        }

    }

    void MovePlayerToRoom()
    {
        Vector3 actualPlayerPosition = transform.position;
        Vector3 advance = new Vector3(3, 3, actualPlayerPosition.z);
        Vector3 targetPosition = actualPlayerPosition + Vector3.Scale(inputVector, advance);
        transform.position = targetPosition;
    }

    IEnumerator MoveCamera(Vector3 start, Vector3 target, float lerpDuration)
    {
        float elapsedTime = 0;

        while (elapsedTime < lerpDuration)
        {
            gameCamera.transform.position = Vector3.Lerp(start, target, curve.Evaluate(elapsedTime / lerpDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameCamera.transform.position = target;

    }*/
    #endregion


}
