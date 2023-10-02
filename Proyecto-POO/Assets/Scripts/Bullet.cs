using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float timeToDestroy;
    private void Start()
    {
        StartCoroutine(WaitThenDestroy());
    }

    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }

    IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);

    }
}