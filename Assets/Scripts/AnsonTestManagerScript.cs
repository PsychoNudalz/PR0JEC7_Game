using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnsonTestManagerScript : MonoBehaviour
{
    Mouse mouse;
    Keyboard keyboard;
    private void Awake()
    {
        mouse = InputSystem.GetDevice<Mouse>();
        keyboard = InputSystem.GetDevice<Keyboard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.leftButton.isPressed)
        {
            testPlayerAttack();
        }
        if (mouse.rightButton.isPressed)
        {
            testHealingPlayer();
        }
        //testOnCollisionDamage();
    }

    void testDamagingPlayer()
    {
        FindObjectOfType<PlayerLifeSystemScript>().takeDamage(Random.Range(5, 10));

    }

    void testHealingPlayer()
    {
        FindObjectOfType<PlayerLifeSystemScript>().healHealth(10);
    }
    void testPlayerAttack()
    {
        if (FindObjectOfType<BoxCastDamageScript>().canDamage())
        {
            FindObjectOfType<BoxCastDamageScript>().dealDamage();
            FindObjectOfType<WeaponAttackAnimateScript>().swingWeapon();

        }
    }

    void testOnCollisionDamage()
    {
        FindObjectOfType<OnCollisionDamageScript>().transform.position += new Vector3(1, 0, 0) * Mathf.Sin(Time.time);
    }
}
