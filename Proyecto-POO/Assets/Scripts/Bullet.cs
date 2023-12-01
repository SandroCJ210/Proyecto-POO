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
    //private Rigidbody2D rbChildren;
    //private bool stop = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rbChildren = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        StartCoroutine(WaitThenDestroy());
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * bulletSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyTear();
    }
    private void DestroyTear()
    {
        rb.velocity = Vector2.zero;
        an.Play("Impact");
        StartCoroutine(WaitTillAnIsOver());
        
    }
    IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        DestroyTear();
    }
    IEnumerator WaitTillAnIsOver()
    {
        yield return new WaitForSeconds(0.536f);
        Destroy(this.gameObject);
    }
}