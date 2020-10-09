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
            testDamagingPlayer();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            testHealingPlayer();
        }
    }

    void testDamagingPlayer()
    {
        FindObjectOfType<PlayerLifeSystemScript>().takeDamage(Random.Range(5,10));

    }

    void testHealingPlayer()
    {
        FindObjectOfType<PlayerLifeSystemScript>().healHealth(10);
    }
}
