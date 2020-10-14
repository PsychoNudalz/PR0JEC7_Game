using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController_AnsonRigidBody : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 inputVector = new Vector2(0, 0);
    public float moveSpeed = 1f;
    public float moveSpeedMax = 10f;
    private Vector3 moveDr;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector3 rotDir;
    public Camera camera1;
    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera1 = FindObjectOfType<Camera>();

    }


    public void Move(InputAction.CallbackContext context)
    {

        inputVector = context.ReadValue<Vector2>();
        moveDr = new Vector3(inputVector.x, 0, inputVector.y);
    }

    private void Update()
    {
        if (moveDr.magnitude >= 0.1f)
        {
            RotateWithCamera();
            rb.velocity += (rotDir.normalized * moveSpeed * Time.deltaTime);
            if (rb.velocity.magnitude >= moveSpeedMax)
            {
                rb.velocity = rb.velocity.normalized * moveSpeedMax;
            }
        }
        animator.SetFloat("Speed", rb.velocity.magnitude);
        animator.transform.position = transform.position;
        animator.transform.rotation = transform.rotation;

    }
    private void RotateWithCamera()
    {

        float targetAngle = Mathf.Atan2(moveDr.x, moveDr.z) * Mathf.Rad2Deg + camera1.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        rotDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


    }
}
