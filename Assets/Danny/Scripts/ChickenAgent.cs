using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using System;

public class ChickenAgent : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1f;
    [SerializeField]
    private float runSpeed = 2f;
    [Header("Chicken waypoints to follow")]
    public Transform waypointsToFollow;
    [SerializeField]
    private GameObject enemyHealthBar;
    [SerializeField]
    private float chargePlayerDistance = 5f;
    [SerializeField]
    private AudioSource chickenSound;
    [SerializeField]
    private AudioSource chickenRunningSound;
    [SerializeField]
    private AudioSource chickenIdleSound;
    private Image healthBarImage;
    private EnemyLifeSystemScript lifeSystem;
    private Transform[] waypoints;
    private NavMeshAgent chickenAgent;
    private string currentAction = "Walk";
    private Animator animator;
    private bool isMoving = true;
    private Transform target;
    private int currentWaypoint;
    private Camera camera;
    public Vector3 initialPosition;


    void Awake()
    {
        
        initialPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
        lifeSystem = GetComponent<EnemyLifeSystemScript>();
        chickenAgent = GetComponent<NavMeshAgent>();
        animator.SetTrigger(currentAction);
        try
        {
            waypoints = new Transform[waypointsToFollow.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = waypointsToFollow.GetChild(i);
            }
        target = waypoints[0];
        } catch (System.Exception e)
        {

        }
        chickenAgent.destination = target.position;

        foreach (Image child in enemyHealthBar.GetComponentsInChildren<Image>())
        {
            if (child.gameObject.name.Equals("EnemyHealthbarImage")){
                healthBarImage = child;
            }
        }
        
            if (!chickenSound.isPlaying)
                chickenSound.Play();
       
        
    }

    void FixedUpdate() {

        SetHealthBar();
        rotateTextToCamera();
        //if next point reached
        if (Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            //Get waypoint script containing actions and perform idleAction
            ChickenWaypoint targetScript = target.GetComponent<ChickenWaypoint>();
            StartCoroutine(PerformWaypointAction(targetScript.GetIdleAction(), targetScript.GetIdleTime(), targetScript.GetIsRunning()));
            GetNextWaypoint();

        }
        if (isMoving) {
            chickenAgent.destination = target.position;
            CheckChargePlayer();
        }
        
    }


    //Perform Idle animation for so many seconds before continuing to next waypoint at set speed
    IEnumerator PerformWaypointAction(IdleAction idleAction, float waitTime, bool isRunning)
    {
        if(waitTime > 0)
        {
            chickenSound.Stop();
            chickenIdleSound.Play();
        }
        chickenAgent.speed = walkSpeed;
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
        chickenIdleSound.Stop();
        if (isRunning)
        {
            if (!chickenRunningSound.isPlaying)
            {
                chickenRunningSound.Play();
            }
            currentAction = "Run";
            chickenAgent.speed = runSpeed;
        }
        else
        {
            currentAction = "Walk";
            chickenAgent.speed = walkSpeed;
        }
        animator.SetTrigger(currentAction);
        isMoving = true;
        if (!chickenSound.isPlaying)
        {
            chickenSound.Play();
        }
        
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

    private void SetHealthBar()
    {
        
        float fillAmount = (float) lifeSystem.Health_Current / (float) lifeSystem.Health_Max;
        healthBarImage.fillAmount = fillAmount;
    }

    void rotateTextToCamera()
    {
        if (camera == null)
        {
            camera = FindObjectOfType<Camera>();
        }


        Vector3 dir = camera.transform.position - transform.position;
        enemyHealthBar.transform.forward = -dir;
    }

    void CheckChargePlayer()
    {
        isMoving = false;
        Collider[] collisions = Physics.OverlapSphere(transform.position, chargePlayerDistance);
        if (collisions.Length > 0)
        {
            foreach (Collider collision in collisions)
            {
                if (collision.CompareTag("Player"))
                {
                    animator.ResetTrigger(currentAction);
                    currentAction = "Run";
                    chickenAgent.speed = runSpeed;
                    chickenAgent.destination = collision.transform.position;
                    animator.SetTrigger(currentAction);
                    if (!chickenRunningSound.isPlaying)
                    {
                        chickenRunningSound.Play();
                    }
                }
            }
        }
        else
        {
            chickenAgent.destination = target.position;
        }
        isMoving = true;
    }

}