using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Animator trapAnimator;
    Collider2D trapCollider;

    private void Start()
    {
        trapAnimator = GetComponent<Animator>();
        trapCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        CheckTrap();
    }

    private void CheckTrap()
    {
        if (trapCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("trap hit");
            trapAnimator.SetTrigger("Stepped");
        }
    }
}
