using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastDamageScript : DamageScript
{


    [Header("Component")]
    public Transform box;


    public override void dealDamage()
    {
        LifeSystemScript ls;

        attackedTargets = new List<LifeSystemScript>();
        RaycastHit[] hits = Physics.BoxCastAll(box.position, box.lossyScale / 2, box.forward, box.rotation, 100f, layerMask);
        foreach (RaycastHit h in hits)
        {
            Collider c = h.collider;
            if (tagList.Contains(c.tag)&& c.GetComponentInParent<LifeSystemScript>()!=null)
            {
                ls = c.GetComponentInParent<LifeSystemScript>();
                attackedTargets.Add(ls);
            }
            print(tagList.Contains(c.tag));
        }
        base.dealDamage();
    }
}
