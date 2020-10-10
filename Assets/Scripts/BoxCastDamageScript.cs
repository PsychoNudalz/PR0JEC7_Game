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
        RaycastHit[] hits = Physics.BoxCastAll(box.position, box.lossyScale / 2, box.forward, box.rotation, 0f, layerMask);
        foreach (RaycastHit h in hits)
        {
            Collider c = h.collider;
            if (tagList.Contains(c.tag) && c.GetComponentInParent<LifeSystemScript>() != null)
            {
                addAttackedTargets(c.gameObject);
                applyLaunch(c.gameObject);
            }
            print(tagList.Contains(c.tag));
        }
        base.dealDamage();
    }
}
