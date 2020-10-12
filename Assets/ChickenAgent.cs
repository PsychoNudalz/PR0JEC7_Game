using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChickenAgent : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1f;
    [SerializeField]
    private float runSpeed = 2f;
    public Transform path;
    private NavMeshAgent playerAgent;
    private string currentAction = "Walk";
    private Transform[] pathPoints;
    private Animator animator; 
    private bool isMoving;
    private Transform target;
    private int currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger(currentAction);
        pathPoints = path.GetComponent<EnemyPath>().GetPoints();
        target = pathPoints[1];
        playerAgent = GetComponent<NavMeshAgent>();
        playerAgent.destination = target.position;
    }

void Update(){

    //if next point reached
        if (Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            //Check which action to perform and how long for before continuing.
            EnemyPathPoint targetScript = target.GetComponent<EnemyPathPoint>();
            switch (targetScript.action)
            {
                case Action.Eat:
                    StartCoroutine(Eat(targetScript.waitTime));
                    break;
                case Action.Look:
                    StartCoroutine(Look(targetScript.waitTime));
                    break;
                case Action.Run:
                    StartCoroutine(Run(targetScript.waitTime));
                    break;
                default:
                    StartCoroutine(Walk(targetScript.waitTime));
                    break;
            }
             GetNextWaypoint();
             
        }
        if(isMoving){
            playerAgent.destination = target.position;
        }
       
    }

    //Get next waypoint and reset to first if last waypoint reached.
    private void GetNextWaypoint()
    {
        if (currentWaypoint >= pathPoints.Length - 1)
        {
            currentWaypoint = 0;
        }
        else
        {
            currentWaypoint++;
        }
        target = pathPoints[currentWaypoint];
    }

    //Perform eat animation for so many seconds before continuing to next waypoint
    IEnumerator Eat(float waitTime) 
    {
        playerAgent.speed = walkSpeed;
        isMoving = false;
        animator.ResetTrigger(currentAction);
        currentAction = "Eat";
        animator.SetTrigger(currentAction);
        yield return new WaitForSeconds(waitTime);
        animator.ResetTrigger(currentAction);
        currentAction = "Walk";
        animator.SetTrigger(currentAction);
        isMoving = true;
    }

    //Perform look animation for so many seconds before continuing to next waypoint
     IEnumerator Look(float waitTime) 
    {
        playerAgent.speed = walkSpeed;
        isMoving = false;
        animator.ResetTrigger(currentAction);
        currentAction = "Turn Head";
        animator.SetTrigger(currentAction);
        yield return new WaitForSeconds(waitTime);
        animator.ResetTrigger(currentAction);
        currentAction = "Walk";
        animator.SetTrigger(currentAction);
        isMoving = true;
    }

    //Perform idle animation for so many seconds before continuing to next waypoint at run speed
    IEnumerator Run(float waitTime) 
    {
        playerAgent.speed = runSpeed;
        isMoving = false;
        animator.ResetTrigger(currentAction);
        yield return new WaitForSeconds(waitTime);
        currentAction = "Run";
        animator.SetTrigger(currentAction);
        isMoving = true;
    }
    //Perform idle animation for so many seconds before continuing to next waypoint
        IEnumerator Walk(float waitTime) 
    {
        playerAgent.speed = walkSpeed;
        isMoving = false;
        animator.ResetTrigger(currentAction);
        yield return new WaitForSeconds(waitTime);
        currentAction = "Walk";
        animator.SetTrigger(currentAction);
        isMoving = true;
    }

}
