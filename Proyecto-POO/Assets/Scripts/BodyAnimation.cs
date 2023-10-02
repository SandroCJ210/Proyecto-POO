using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimation : MonoBehaviour
{
    [SerializeField]
    private Player player;
    private Animator an;
    void Start()
    {
        an = GetComponent<Animator>();
    }
    private void Update()
    {
        if (player.isWalking)
        {
            an.SetBool("isWalking", true);
        }
        else
        {
            an.SetBool("isWalking", false);
        }
    }
    public void ShootAnimation()
    {
        an.SetTrigger("Shoot");
    }
    public void ReloadAnimation()
    {
        an.SetTrigger("Reload");
    }
}
