using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChickenMovementScript : MonoBehaviour
{
    [Header("Walking speed (run is double this)")]
    public float startSpeed = 2f;
    [Header("Path gameobject containing waypoints")]
    public Transform path;
    private float speed;
    private string currentAction = "Walk";
    private Transform[] pathPoints;
    private Animator animator; 
    private bool isMoving;
    private Transform target;
    private int currentWaypoint;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        isMoving = true;
        animator = GetComponent<Animator>();
        animator.SetTrigger(currentAction);
        pathPoints = path.GetComponent<EnemyPath>().GetPoints();
        target = pathPoints[1];
        this.transform.position = pathPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        //if moving look at next waypoint and move toward it at set speed
        this.transform.LookAt(target);
        if(isMoving){
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }

        //if next point reached
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
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
        speed = startSpeed;
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
        speed = startSpeed;
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
        speed = startSpeed * 2;
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
        speed = startSpeed;
        isMoving = false;
        animator.ResetTrigger(currentAction);
        yield return new WaitForSeconds(waitTime);
        currentAction = "Walk";
        animator.SetTrigger(currentAction);
        isMoving = true;
    }
}
