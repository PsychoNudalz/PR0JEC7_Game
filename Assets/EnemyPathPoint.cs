using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathPoint : MonoBehaviour
{
    public string action = "Continue";
    // Start is called before the first frame update

    public string GetAction(){
        return this.action;
    }
}
