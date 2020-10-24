﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerInteractionScript : MonoBehaviour
{

    public InteractableScript currentFocus;
    // Start is called before the first frame update
    

    public void useInteractable()
    {
        if (currentFocus != null)
        {
            currentFocus.activate();
        }
    }

    public void setFocus(InteractableScript i)
    {
        if (currentFocus != null && currentFocus.TryGetComponent(out ButtonInteractableScript b))
        {
            b.setMaterialFrenel(0);
        }
        currentFocus = i;
        if (currentFocus != null && currentFocus.TryGetComponent(out ButtonInteractableScript b2))
        {
            b2.setMaterialFrenel(1);
        }
    }
    
}