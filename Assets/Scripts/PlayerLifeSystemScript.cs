using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystemScript : LifeSystemScript
{
    [Header("Player Animator")]
    public Animator animator;
    public string deathTriggerName = "Death";

    public override void DeathBehaviour()
    {
        
        base.DeathBehaviour();
    }
    public override IEnumerator delayDeathRoutine()
    {
        print(name + " death behaviour");
        animator.SetBool(deathTriggerName, IsDead);
        print(animator.GetBool(deathTriggerName));
        yield return new WaitForSeconds(delayDeath);
        DeathBehaviour();
    }


}
