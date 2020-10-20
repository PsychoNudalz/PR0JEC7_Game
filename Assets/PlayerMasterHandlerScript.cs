using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
