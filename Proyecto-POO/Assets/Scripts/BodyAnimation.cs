using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimation : MonoBehaviour
{
    [SerializeField]
    private Player player;
    private Animator an;
    private void Start()
    {
        an = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        an.SetFloat("Horizontal", player.InputVector.x);
        an.SetFloat("Vertical", player.InputVector.y);
        an.SetFloat("Speed", player.InputVector.sqrMagnitude);
    }
}
