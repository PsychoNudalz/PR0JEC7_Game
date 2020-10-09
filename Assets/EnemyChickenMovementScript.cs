using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChickenMovementScript : MonoBehaviour
{

    public float speed = 10f;
    public Transform path;

    private Transform[] pathPoints;
    private Animator animator; 

    private Transform target;
    private int currentWaypoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        animator.SetTrigger("Walk");
        pathPoints = path.GetComponent<EnemyPath>().GetPoints();
        target = pathPoints[1];
        this.transform.position = pathPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.LookAt(target);

        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }

        /*
        if(Input.GetKey(KeyCode.Space)){
            animator.SetTrigger("Walk");
        }
        else{
            animator.ResetTrigger("Walk");
        }
        */
    }

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
}
