using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChickenAgent : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1f;
    [SerializeField]
    private float runSpeed = 2f;
    [Header("Chicken waypoints to follow")]
    public Transform waypointsToFollow;
    private Transform[] waypoints;
    private NavMeshAgent playerAgent;
    private string currentAction = "Walk";
    private Animator animator;
    private bool isMoving;
    private Transform target;
    private int currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger(currentAction);
            waypoints = new Transform[waypointsToFollow.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = waypointsToFollow.GetChild(i);
            }
        target = waypoints[0];
        playerAgent = GetComponent<NavMeshAgent>();
        playerAgent.destination = target.position;
    }

    void Update() {

        //if next point reached
        if (Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            //Get waypoint script containing actions and perform idleAction
            ChickenWaypoint targetScript = target.GetComponent<ChickenWaypoint>();
            StartCoroutine(PerformWaypointAction(targetScript.GetIdleAction(), targetScript.GetIdleTime(), targetScript.GetIsRunning()));
            GetNextWaypoint();

        }
        if (isMoving) {
            playerAgent.destination = target.position;
        }

    }

    //Perform Idle animation for so many seconds before continuing to next waypoint at set speed
    IEnumerator PerformWaypointAction(IdleAction idleAction, float waitTime, bool isRunning)
    {
        playerAgent.speed = walkSpeed;
        isMoving = false;
        animator.ResetTrigger(currentAction);
        switch (idleAction)
        {
            case IdleAction.Eat:
                currentAction = "Eat";
                animator.SetTrigger(currentAction);
                break;
            case IdleAction.Look:
                currentAction = "Turn Head";
                animator.SetTrigger(currentAction);
                break;
            default:
                animator.ResetTrigger(currentAction);
                break;
        }
        yield return new WaitForSeconds(waitTime);
        animator.ResetTrigger(currentAction);
        if (isRunning)
        {
            currentAction = "Run";
            playerAgent.speed = runSpeed;
        }
        else
        {
            currentAction = "Walk";
            playerAgent.speed = walkSpeed;
        }
        animator.SetTrigger(currentAction);
        isMoving = true;
    }

    //Get next waypoint and reset to first if last waypoint reached.
    private void GetNextWaypoint()
    {
        if (currentWaypoint >= waypoints.Length - 1)
        {
            currentWaypoint = 0;
        }
        else
        {
            currentWaypoint++;
        }
        target = waypoints[currentWaypoint];
    }
}