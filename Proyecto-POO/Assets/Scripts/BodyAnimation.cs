using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimation : MonoBehaviour
{
    private Animator an;
    void Start()
    {
        an = GetComponent<Animator>();
    }

    public void ShootAnimation()
    {
        an.SetTrigger("Shoot");
    }
}
