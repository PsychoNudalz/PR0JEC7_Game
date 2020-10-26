using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private Vector2 inputVector = new Vector2(0, 0);
    public float moveSpeed = 1f;
    public float moveSpeedMax = 10f;
    private Vector3 moveDr;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector3 rotDir;
    [SerializeField] bool grounded;
    public float jumpStrength;
    [SerializeField]LayerMask layerMask;


    [Header("Component")]
    private Rigidbody rb;
    public Camera camera1;
    public Animator animator;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        camera1 = FindObjectOfType<Camera>();
        grounded = true;
      

    }


    public void Move(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        moveDr = new Vector3(inputVector.x, 0, inputVector.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {

        if (context.performed && grounded) {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * (jumpStrength*1000));
            animator.SetBool("isFalling", true);
            grounded = false;
            animator.SetBool("Grounded", false);
        }
         
    }

    private void Update()
    {


        if (moveDr.magnitude >= 0.1f)
        {
            RotateWithCamera();
            float gravity = rb.velocity.y;
            rb.velocity = (rotDir.normalized * moveSpeed);
            if (rb.velocity.magnitude >= moveSpeedMax)
            {
                rb.velocity = rb.velocity.normalized * moveSpeedMax;
            }
            rb.velocity += new Vector3(0, gravity, 0);
        }
        animator.SetFloat("Speed", rb.velocity.magnitude);
        animator.transform.position = transform.position;
        animator.transform.rotation = transform.rotation;

        //Anson: Added to update falling animation
        grounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1.6f, layerMask);
        animator.SetBool("Grounded", grounded);
    }

    // dont delete this its my pride and joy :^D

    /* private void OnCollisionEnter(Collision collision)
     {
         if (collision.collider.CompareTag("Floor")) {
             grounded = true;

         }
         else if (!collision.collider.CompareTag("Floor") &&  rb.velocity.y <= 0.01 && rb.velocity.y >= -0.01) {
             grounded = true;

         }

     }

     private void OnCollisionExit(Collision collision)
     {
             grounded = false;

          if (collision.collider.CompareTag("Enviroment") && rb.velocity.y <= 0.01 && rb.velocity.y >= -0.01)
         {
             grounded = true;
         }
     }
     */


    private void RotateWithCamera()
    {

        float targetAngle = Mathf.Atan2(moveDr.x, moveDr.z) * Mathf.Rad2Deg + camera1.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        rotDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


    }

    public void RotateWithCamera_Force()
    {
        float targetAngle = Mathf.Atan2(moveDr.x, moveDr.z) * Mathf.Rad2Deg + camera1.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        rotDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

    }
}
