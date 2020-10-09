using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base Life system super class
/// </summary>

public class LifeSystemScript : MonoBehaviour
{
    [Header("States")]
    [SerializeField] int health_Current;
    [SerializeField] int health_Max = 10;
    [SerializeField] bool isDead = false;

    [Header("On Death")]
    public GameObject deathGameObject;
    public bool disableOnDeath = true;
    public bool destroyOnDeath;

    [Header("Components")]
    public DamagePopScript damagePopScript;

    public int Health_Current { get => health_Current; }
    public int Health_Max { get => health_Max; }
    public bool IsDead { get => isDead; }

    private void Awake()
    {
        health_Current = health_Max;
    }

    /// <summary>
    /// deal damage to the gameobject
    /// damage rounded to the closest integer
    /// triggers death event if health reaches 0
    /// </summary>
    /// <param name="dmg"></param>
    /// <returns> health remaining </returns>
    public virtual int takeDamage(float dmg)
    {

        if (!isDead)
        {
            health_Current -= Mathf.RoundToInt(dmg);
            print(name + " take damage: " + dmg);
            displayDamage(dmg);
        }

        checkDead();
        return health_Current;

    }
    /// <summary>
    /// heal gameobject
    /// amount rounded to the closest integer
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns> health remaining</returns>
    public virtual int healHealth(float amount)
    {
        if (!isDead)
        {
            health_Current += Mathf.RoundToInt(amount);
            print(name + " heal damage: " + amount);
        }
        return health_Current;
    }


    /// <summary>
    /// check if the gameobject is dead
    /// plays death event when health reaches 0
    /// </summary>
    /// <returns></returns>
    public bool checkDead()
    {
        if (health_Current <= 0)
        {
            isDead = true;
            if(deathGameObject != null)
            {
                deathGameObject.transform.SetParent(null);
                deathGameObject.SetActive(true);

            }

            if (disableOnDeath)
            {
                gameObject.SetActive(false);
            } else if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }
        return isDead;
    }

    void displayDamage(float dmg)
    {
        damagePopScript.displayDamage(dmg);
    }
}
