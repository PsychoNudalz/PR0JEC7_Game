using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInteractableScript : InteractableScript
{
    public TrapScript trapScript;

    private void Start()
    {
        if (trapScript == null)
        {
            trapScript = GetComponent<TrapScript>();
        }
    }

    public override void activate()
    {
        base.activate();
        trapScript.activeTrap();
    }

    public override void deactivate()
    {
        base.deactivate();
        trapScript.deactiveTrap();
    }




}
