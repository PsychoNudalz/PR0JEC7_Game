using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum for actions
public enum IdleAction {Eat, Look, Idle};

public class ChickenWaypoint : MonoBehaviour
{

    [Header("Time to perform idle animation")]
    [SerializeField]
    private float idleTime;
    [Header("Idle action to perform at waypoint")]
    [SerializeField]
    private IdleAction idleAction;
    [Header("Is chicken running to next waypoint?")]
    [SerializeField]
    private bool isRunning;

    public float GetIdleTime()
    {
        return idleTime;
    }

    public IdleAction GetIdleAction()
    {
        return idleAction;
    }

    public bool GetIsRunning()
    {
        return isRunning;
    }
}
