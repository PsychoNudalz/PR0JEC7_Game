using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractableScript : InteractableScript
{
    public enum ButtonType
    {
        TOGGLE,
        ACTIVE,
        DEACTIVE
    }
    [Header("Boutton")]
    public ButtonType buttonType;
    public PlayerInteractionScript playerInteractionScript;
    public List<InteractableScript> interactTargets;
    public Animator animator;
    public MeshRenderer meshRenderer;
    [Header("Timer")]
    public float timer = 0f; //will not deactivate if timer is 0;
    bool timerStart = false;
    float timer_Now = 0;
    Coroutine currentCoroutine;

    private void Start()
    {
        updateButtonAnimation(interactableActive);

    }
    private void FixedUpdate()
    {
        if (timer >0 && timer_Now < timer)
        {
            timer_Now += Time.deltaTime;
            meshRenderer.material.SetFloat("_StepValue", timer_Now / timer);
        }
    }

    //Tigger Zone handling
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && getPlayerInteraction(other.gameObject))
        {
            playerInteractionScript.setFocus(this);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && getPlayerInteraction(other.gameObject))
        {
            playerInteractionScript.setFocus(null);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && getPlayerInteraction(other.gameObject))
        {
            if (playerInteractionScript.currentFocus == null)
            {
                playerInteractionScript.setFocus(this);
            }
        }
    }

    //Button Behavoiur 
    public void useButton()
    {
        print(name + " use");
        switch (buttonType)
        {
            case (ButtonType.TOGGLE):
                toggleActivationForList(interactTargets);
                break;
            case (ButtonType.ACTIVE):
                setActivationForList(interactTargets, true);
                break;
            case (ButtonType.DEACTIVE):
                setActivationForList(interactTargets, false);
                break;

        }
        updateButtonAnimation(interactableActive);


    }

    public override void activate()
    {
        try
        {
            StopCoroutine(currentCoroutine);

        } catch(System.Exception _)
        {

        }
        activateBehaviour();
        if (timer > 0f)
        {
            currentCoroutine = StartCoroutine(autoDeactivate());
        }
    }

    void activateBehaviour()
    {
        switch (buttonType)
        {
            case (ButtonType.TOGGLE):
                interactableActive = !interactableActive;
                break;
            case (ButtonType.ACTIVE):
                interactableActive = true;
                break;
            case (ButtonType.DEACTIVE):
                base.deactivate();
                break;

        }
        useButton();
    }


    //Other
    bool getPlayerInteraction(GameObject other)
    {
        playerInteractionScript = other.GetComponent<PlayerInteractionScript>();
        return playerInteractionScript != null;
    }

    void updateButtonAnimation(bool t)
    {
        if (t)
        {
            animator.SetBool("Active", true);
        }
        else
        {
            animator.SetBool("Active", false);

        }
    }

    public void setMaterialFrenel(int b)
    {
        meshRenderer.material.SetFloat("_ActiveFrenel", b);
        print(meshRenderer.material.GetFloat("_ActiveFrenel"));
    }

    //Timer
    IEnumerator autoDeactivate()
    {
        timer_Now = 0;
        yield return new WaitForSeconds(timer);
        activateBehaviour();
    }
}
