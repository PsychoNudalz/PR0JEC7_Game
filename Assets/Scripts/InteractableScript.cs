using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableScript : MonoBehaviour
{
    public bool interactableActive = true;

    public virtual void activate()
    {
        interactableActive = true;
    }

    public virtual void deactivate()
    {
        interactableActive = false;
    }

    public virtual void toggleActivate()
    {
        if (interactableActive)
        {
            deactivate();
        }
        else
        {
            activate();
        }
    }

    public void setActivationForList(List<InteractableScript> interactableScripts, bool b)
    {
        foreach (InteractableScript i in interactableScripts)
        {
            if (b)
            {
                i.activate();
            }
            else
            {
                i.deactivate();
            }
        }
    }
    public void toggleActivationForList(List<InteractableScript> interactableScripts)
    {
        foreach (InteractableScript i in interactableScripts)
        {
            i.toggleActivate();
        }
    }

}
