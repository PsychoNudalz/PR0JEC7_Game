using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [Header("States")]
    [SerializeField] Vector2 damageRange;
    [SerializeField] float timeBetweenAttack;
    [Header("Target")]
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected List<string> tagList;
    [Header("Debug")]
    [SerializeField] protected List<LifeSystemScript> attackedTargets = new List<LifeSystemScript>();
    [SerializeField] float timeBetweenAttack_TimeNow = 0;

    private void Awake()
    {
        if (damageRange.x > damageRange.y)
        {
            float i = damageRange.x;
            damageRange.x = damageRange.y;
            damageRange.y = i;
        }
    }

    private void FixedUpdate()
    {
        if (timeBetweenAttack_TimeNow > 0)
        {
            timeBetweenAttack_TimeNow -= Time.deltaTime;
        }
    }

    public virtual void dealDamageToTarget(LifeSystemScript ls)
    {
        ls.takeDamage(Random.Range(damageRange.x, damageRange.y));
    }
    public virtual void dealDamage()
    {
        if (timeBetweenAttack_TimeNow > 0)
        {
            return;
        }
        foreach(LifeSystemScript ls in attackedTargets)
        {
            dealDamageToTarget(ls);
        }
        attackedTargets = new List<LifeSystemScript>();

        timeBetweenAttack_TimeNow = timeBetweenAttack;
    }


}
