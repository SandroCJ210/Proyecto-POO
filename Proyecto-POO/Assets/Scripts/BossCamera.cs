using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public Vector3 minValues, maxValues;


    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);
        Vector3 boundedPosition = new Vector3(
                                              Mathf.Clamp(newPosition.x, minValues.x, maxValues.x),
                                              Mathf.Clamp(newPosition.y, minValues.y, maxValues.y),
                                              Mathf.Clamp(newPosition.z, minValues.z, maxValues.z));

        transform.position = Vector3.Slerp(transform.position, boundedPosition, speed * Time.deltaTime);
        
    }
}
