using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Mechanic")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float timeBetweenShoots;
    [SerializeField]
    private Player player;
    private Animator an;
    private float xAxis;
    private float yAxis;
    [HideInInspector]
    public Vector2 shootVector;
    
    private float nextTimetoShoot;
    void Start()
    {
        an = GetComponent<Animator>();
    }

    void Update()
    {
        Shooting();
    }
    private void LateUpdate()
    {
        an.SetFloat("HShoot", xAxis);
        an.SetFloat("VShoot", yAxis);
        an.SetFloat("isShooting", shootVector.sqrMagnitude);
        an.SetFloat("Horizontal", player.inputVector.x);
        an.SetFloat("Vertical",player.inputVector.y);
        an.SetFloat("Sp", player.inputVector.sqrMagnitude);
    }

    void Shooting()
    {
        
        
        if (yAxis == 0)
        {
            xAxis = Input.GetAxisRaw("HorizontalShoot");
        }
        if(xAxis == 0)
        {
            yAxis = Input.GetAxisRaw("VerticalShoot");
        }
        shootVector = new Vector2(xAxis, yAxis);
        if (xAxis != 0 || yAxis != 0)
        {
            if (Time.time > nextTimetoShoot)
            {
                nextTimetoShoot = Time.time + timeBetweenShoots;
                if (shootVector.y == -1) 
                {
                    Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                }
                else
                {
                    Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, shootVector.x*-90)));
                }
            }
        }
        
    }
}
