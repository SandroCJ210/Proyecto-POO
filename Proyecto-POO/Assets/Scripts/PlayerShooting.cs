using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Animator an;
    [SerializeField]
    private Player player;
    private float xAxis;
    private float yAxis;
    public Vector2 shootVector;
    [SerializeField]
    private float timeBetweenShoots;
    [SerializeField]
    private float nextTimetoShoot;
    void Start()
    {
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }
    private void LateUpdate()
    {
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
        }/*
        if(xAxis != 0 || yAxis != 0)
        {
            if (Time.time > nextTimetoShoot)
            {
                nextTimetoShoot = Time.time + timeBetweenShoots;
            }
        }*/
        shootVector = new Vector2(xAxis, yAxis);
        an.SetFloat("HShoot", xAxis);
        an.SetFloat("VShoot", yAxis);
        an.SetFloat("isShooting", shootVector.sqrMagnitude);
        //Usar una nueva configuración de Input para conseguir un vector y de esa manera usar blend trees
    }
}
