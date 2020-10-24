﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Main Player Handler, only get this component from outside of player
/// </summary>
public class PlayerMasterHandlerScript : MonoBehaviour
{
    [Header("Player Components")]
    public PlayerLifeSystemScript playerLifeSystem;
    public PlayerController playerController;
    public PlayerInteractionScript playerInteraction;
    [Header("Attack")]
    public PlayerAttackScript playerAttack;
    [Header("Outside Components")]
    public HealthBarController healthBarController;


    private void Awake()
    {
        if (healthBarController == null)
        {
            healthBarController = FindObjectOfType<HealthBarController>();

        }
        playerLifeSystem.healthBarController = healthBarController;
    }

    public void Attack()
    {

        if (playerAttack.Attack())
        {
            //playerController.RotateWithCamera_Force();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!playerLifeSystem.IsDead)
        {
            playerController.Move(context);

        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!playerLifeSystem.IsDead)
        {
            playerController.Jump(context);
        }
    }
}
