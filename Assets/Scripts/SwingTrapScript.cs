using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingTrapScript : MonoBehaviour
{
    public GameObject swingObject;
    public float swingSpeed = 10f;
    public float swingMaxAngle = 90f;
    public float offSetAngle = 0;
    public float offSetStartAngle = 0;
    private Quaternion initialRot;
    [SerializeField] float swingAngle = 0f;

    private void Awake()
    {
        initialRot = swingObject.transform.localRotation;
    }

    private void Update()
    {
        rotateSwingObject();
    }

    void rotateSwingObject()
    {
        swingAngle = Mathf.Sin(Time.time * swingSpeed+asinAngle(offSetStartAngle)) * (swingMaxAngle / 2f);
        print(swingAngle);
        print("After: " + swingAngle);
        swingAngle +=offSetAngle;
        swingObject.transform.rotation = Quaternion.AngleAxis( swingAngle, transform.right)*initialRot*transform.rotation;
    }

    float asinAngle(float angle)
    {
        float r = angle / (swingMaxAngle / 2f);
        //r -= Mathf.Floor(r);
        r = Mathf.Asin(r);
        return r;
    }
}
