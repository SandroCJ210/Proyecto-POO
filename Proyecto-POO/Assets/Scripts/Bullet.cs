using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float timeToDestroy;
    [SerializeField]
    private GameObject impactEffect;
    [SerializeField]
    private PlayerShooting pS;
    private Vector2 direction;
    private Rigidbody2D rb;
 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pS = FindObjectOfType<PlayerShooting>();
        StartCoroutine(WaitThenDestroy());
        direction = pS.ShootVector;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Player"))
        {
            if (collision.transform.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
            }
            rb.velocity = Vector2.zero;
            DestroyTear();
        }
    }

    IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        DestroyTear();
    }
    void DestroyTear()
    {
        GameObject effect = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        Destroy(this.gameObject);
    }
    
}