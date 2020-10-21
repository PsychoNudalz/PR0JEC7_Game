using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Main Player Handler, only get this component from outside of player
/// </summary>
public class PlayerMasterHandlerScript : MonoBehaviour
{
    [Header("Components")]
    public PlayerLifeSystemScript playerLifeSystem;
    public PlayerController_AnsonRigidBody playerController;
    public PlayerInteractionScript playerInteraction;
    [Header("Attack")]
    public PlayerAttackScript playerAttack;


    public void Attack()
    {

        if (playerAttack.Attack())
        {
            //playerController.RotateWithCamera_Force();
        }
    }
}
