using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class SkeletonAgent : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1f;
    [SerializeField]
    private float runSpeed = 2f;
    [Header("Skeleton waypoints to follow")]
    public Transform waypointsToFollow;
    [SerializeField]
    private GameObject enemyHealthBar;
    [SerializeField]
    private float chargePlayerDistance = 5f;
    private Image healthBarImage;
    private EnemyLifeSystemScript lifeSystem;
    private Transform[] waypoints;
    private NavMeshAgent skeletonAgent;
    private string currentAction = "Walk";
    private Animator animator;
    private bool isMoving = true;
    private Transform target;
    private int currentWaypoint;
    private Camera camera;
    private AudioSource rattlingBones;


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

        skeletonAgent = GetComponent<NavMeshAgent>();
        skeletonAgent.destination = target.position;
        lifeSystem = GetComponent<EnemyLifeSystemScript>();

        foreach(Image child in enemyHealthBar.GetComponentsInChildren<Image>())
        {
            if (child.gameObject.name.Equals("EnemyHealthbarImage")){
                healthBarImage = child;
            }
        }

        SetHealthBar();
        rotateTextToCamera();
        rattlingBones = GetComponent<AudioSource>();
        rattlingBones.Play();
    }

    void FixedUpdate() {

        SetHealthBar();
        rotateTextToCamera();
        //if next point reached
        if (Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            //Get waypoint script containing actions and perform idleAction
            SkeletonWaypoint targetScript = target.GetComponent<SkeletonWaypoint>();
            StartCoroutine(PerformWaypointAction(targetScript.GetIdleTime(), targetScript.GetIsRunning()));
            GetNextWaypoint();

        }
        if (isMoving) {
            skeletonAgent.destination = target.position;
            CheckChargePlayer();
        }
        
    }


    //Perform Idle animation for so many seconds before continuing to next waypoint at set speed
    IEnumerator PerformWaypointAction(float waitTime, bool isRunning)
    {
        if(waitTime > 0)
        {
            rattlingBones.Stop();
        }
        skeletonAgent.speed = walkSpeed;
        isMoving = false;
        animator.ResetTrigger(currentAction);
                       
        yield return new WaitForSeconds(waitTime);

        if (isRunning)
        {
            currentAction = "Run";
            skeletonAgent.speed = runSpeed;
        }
        else
        {
            currentAction = "Walk";
            skeletonAgent.speed = walkSpeed;
        }
        animator.SetTrigger(currentAction);
        isMoving = true;
        if (!rattlingBones.isPlaying)
        {
            rattlingBones.Play();
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
                    skeletonAgent.speed = runSpeed;
                    skeletonAgent.destination = collision.transform.position;
                    animator.SetTrigger(currentAction);
                }
            }
        }
        else
        {
            skeletonAgent.destination = target.position;
        }
        isMoving = true;
    }
}