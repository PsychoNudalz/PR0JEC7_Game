﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extends Interactable Script
/// links the trap script with the interactable script
/// allows other scripts like Button Interactable to control the trap
/// </summary>
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
