using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnsonTestManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            testPlayerAttack();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            testHealingPlayer();
        }
        //testOnCollisionDamage();
    }

    void testDamagingPlayer()
    {
        FindObjectOfType<PlayerLifeSystemScript>().takeDamage(Random.Range(5,10));

    }

    void testHealingPlayer()
    {
        FindObjectOfType<PlayerLifeSystemScript>().healHealth(10);
    }
    void testPlayerAttack()
    {
        FindObjectOfType<BoxCastDamageScript>().dealDamage();
    }

    void testOnCollisionDamage()
    {
        FindObjectOfType<OnCollisionDamageScript>().transform.position += new Vector3(1,0,0)* Mathf.Sin(Time.time);
    }
}
