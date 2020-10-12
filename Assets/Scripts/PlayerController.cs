using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 inputVector = new Vector2(0,0);
    private float moveSpeed = 10f;
    public CharacterController controller;
    private Vector3 moveDr;
    public float turnSmoothTime =0.1f;
    float turnSmoothVelocity;
    Vector3 rotDir;
    public Camera camera1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }


    public void Move(InputAction.CallbackContext context) {

        inputVector = context.ReadValue<Vector2>();
        moveDr = new Vector3(inputVector.x, 0, inputVector.y);
    }

    private void Update()
    {
        if (moveDr.magnitude >= 0.1f)
        {
            RotateWithCamera();
            controller.Move(rotDir.normalized * moveSpeed * Time.deltaTime);
            
        }

    }
    private void RotateWithCamera() {
       
            float targetAngle = Mathf.Atan2(moveDr.x, moveDr.z) * Mathf.Rad2Deg +camera1.transform.eulerAngles.y;
            float angle =Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            rotDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        

    }
        
}
