using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extends Life System Script
/// for player
/// </summary>
public class PlayerLifeSystemScript : LifeSystemScript
{
    [Header("Player Animator")]
    public Animator animator;
    public string deathTriggerName = "Death";

    public override void DeathBehaviour()
    {
        base.DeathBehaviour();
    }

    /// <summary>
    /// applies a delay so that that the animatorcan play the death animation, before disabling the player GameObject
    /// </summary>
    /// <returns></returns>
    public override IEnumerator delayDeathRoutine()
    {
        print(name + " death behaviour");
        animator.SetBool(deathTriggerName, IsDead);
        print(animator.GetBool(deathTriggerName));
        yield return new WaitForSeconds(delayDeath);
        DeathBehaviour();
    }


}
