using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    Transform firePivot;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float timeBetweenShoots;
    private float nextTimetoShoot;

    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time > nextTimetoShoot)
            {
                nextTimetoShoot = Time.time + timeBetweenShoots;
                GameObject bullet = Instantiate(bulletPrefab, firePivot.position, firePivot.rotation);
            }
        }
    }
}
