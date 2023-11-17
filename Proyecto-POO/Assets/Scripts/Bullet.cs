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
    private float fallingSpeed;
    private Animator an;
    private Rigidbody2D rb;
    private Rigidbody2D rbChildren;
    private bool stop = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbChildren = GetComponentInChildren<Rigidbody2D>();
        an = GetComponentInChildren<Animator>();
        StartCoroutine(WaitThenDestroy());
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * bulletSpeed;
    }
    IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        if(rb.velocity.x < 0.1)
        {
            rbChildren.velocity = new Vector2(rb.velocity.x * 1 / 6, -0.5f);
        }
        else
        {
            rbChildren.velocity = new Vector2(0, 0.1f);
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}



