using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTrigger : MonoBehaviour
{
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            cam.GetComponent<BossCamera>().enabled = true;
        }
    }
}
