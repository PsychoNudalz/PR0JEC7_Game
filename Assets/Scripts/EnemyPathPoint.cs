using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //enum for actions
    public enum Action {Walk, Run, Eat, Look, Idle};

public class EnemyPathPoint : MonoBehaviour
{
    [Header("Action to perform at waypoint")]
    public Action action;
    [Header("Time to perform action for before continuing to next waypoint")]
    public float waitTime = 2f;
}
