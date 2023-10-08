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
    private float toFall;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, toFall*-1);
        StartCoroutine(WaitThenDestroy());
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletSpeed);
    }
    IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}