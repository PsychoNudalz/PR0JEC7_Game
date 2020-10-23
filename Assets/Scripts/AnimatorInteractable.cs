using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Intereactive Object to activate an animator's animation
/// good for doors or bridges
/// </summary>
public class AnimatorInteractable : InteractableScript
{
    [Header("Animator")]
    public Animator animator;
    public string boolName = "Activate";

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }


    private void Start()
    {
        playAnimation();
    }

    public override void activate()
    {
        base.activate();
        playAnimation();
    }

    public override void deactivate()
    {
        base.deactivate();
        playAnimation();
    }

    void playAnimation()
    {
        animator.SetBool(boolName, interactableActive);
    }
}
