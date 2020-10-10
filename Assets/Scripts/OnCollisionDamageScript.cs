using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDamageScript : DamageScript
{
    [Header("Collision Behaviour")]
    public bool onEnter;
    public bool onStay;
    public bool onExit;
    //public bool addToTargetsOnEnter = true;
    //public bool removeFromTargetsOnExit = true;


    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (onEnter && tagList.Contains(collision.gameObject.tag) && collision.gameObject.GetComponentInParent<LifeSystemScript>() != null)
        {
            addAttackedTargets(collision.gameObject);
            dealDamage();
            applyLaunch(collision.gameObject );
        }
        print(onEnter && tagList.Contains(collision.gameObject.tag) && collision.gameObject.GetComponentInParent<LifeSystemScript>() != null);
    }





}
