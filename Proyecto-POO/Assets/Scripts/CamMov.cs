using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMov : MonoBehaviour
{
    private GameObject otherRoom;
    public Camera camMapa;
    public int arribaAbajo;
    public int izqDer;
    public bool puertaAbierta;
    public Sprite[] sprtPuerta;

    private void Awake()
    {
        camMapa = Camera.main;
    }

    private void Update()
    {
        if (puertaAbierta == true)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprtPuerta[1];
        }
        else this.GetComponent<SprteRenderer>().sprite = sprtPuerta[0];

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            puertaAbierta = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Door")
        {
            otherRoom = other.transform.parent.transform.parent.gameObject;
        }
    
        if(puertaAbierta == true)
        {
            if(other.tag == "Player")
            {
                other.transform.position = new Vector2(this.transform.position.x + 2.5f * izqDer, this.transform.position.y + 2.5f * arribaAbajo);
                camMapa.transform.position = new Vector3(otherRoom.transform.position.x, otherRoom.transform.position.y,-10);
            }
        }
        StartCoroutine("Verificar");
        IEnumerator Verificar()
        {
            vield return new WaitForSeconds(0.01f);
            if(otherRoom == null)
            {
                Destroy(this.gameObject);
            }
        }

    }

    

}
