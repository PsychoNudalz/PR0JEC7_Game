using System.Collections;
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
    /// <summary>
    /// Attacking the player and playing the animation
    /// </summary>
    public void Attack()
    {

        if (playerAttack.Attack())
        {
            //playerController.RotateWithCamera_Force();
        }
    }
    /// <summary>
    /// Called by player input to move the character
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {
        if (!playerLifeSystem.IsDead)
        {
            playerController.Move(context);

        }
    }
    /// <summary>
    /// Called by player input have the character jump
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {
        if (!playerLifeSystem.IsDead)
        {
            playerController.Jump(context);
        }
    }

    /// <summary>
    /// get the fraction of the player's health
    /// </summary>
    /// <returns> fraction of the player's health </returns>
    public float getHealthFraction()
    {
        if (playerLifeSystem != null)
        {
            return (float)playerLifeSystem.Health_Current / (float)playerLifeSystem.Health_Max;
        }
        return 0f;

    }
}
