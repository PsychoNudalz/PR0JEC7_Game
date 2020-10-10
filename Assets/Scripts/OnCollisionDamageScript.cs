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
    [Header("Launch On Collision")]
    public bool launchOnCollision = true;
    [SerializeField] Vector3 launchDir;
    public float launchForce = 200f;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (onEnter && tagList.Contains(collision.gameObject.tag) && collision.gameObject.GetComponentInParent<LifeSystemScript>() != null)
        {
            addAttackedTargets(collision.gameObject);
            dealDamage();
            applyLaunch(collision);
        }
        print(onEnter && tagList.Contains(collision.gameObject.tag) && collision.gameObject.GetComponentInParent<LifeSystemScript>() != null);
    }

    void applyLaunch(Collision collision)
    {
        if (launchOnCollision)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                return;
            }
            launchDir = (collision.transform.position-transform.position).normalized;
            rb.AddForce(launchDir * launchForce*rb.mass);
        }
    }



}
