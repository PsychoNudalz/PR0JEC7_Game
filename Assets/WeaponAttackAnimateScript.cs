using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackAnimateScript : MonoBehaviour
{
    [Header("States")]
    public float swingSpeed= 2f;

    [Header("Components")]
    public Transform weaponTransform;
    public Animator weaponAnimator;
    public Transform attackArea;

    [Header("Debugging")]
    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 originalPos;
    public bool swinging = false;

    private void Awake()
    {
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        if (swinging)
        {
            if ((weaponTransform.localPosition-endPos).magnitude < .2f)
            {
                print("Reached point");
                swinging = false;
            }

            Vector3 slerp = Vector3.Slerp(weaponTransform.localPosition, endPos, swingSpeed * Time.deltaTime);
            //weaponTransform.up = -(slerp - weaponTransform.position).normalized;
            weaponTransform.localPosition = slerp;
            //weaponTransform.up = transform.forward;
        }
        else
        {
            Vector3 slerp = Vector3.Slerp(weaponTransform.localPosition, originalPos , swingSpeed * Time.deltaTime);
            weaponTransform.localPosition = slerp;
            weaponTransform.up = transform.up;
        }
    }

    public void swingWeapon()
    {
        swinging = true;
        pickPos();
        weaponTransform.localPosition = startPos;
        weaponAnimator.SetTrigger("Swing");
        Vector3 dir = endPos-startPos;

        weaponTransform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+90);


        //weaponTransform.up = -(slerp - weaponTransform.position).normalized;
        //weaponTransform.forward = (endPos + transform.position - startPos + transform.position).normalized;
        //weaponTransform.up = attackArea.forward;
    }

    void pickPos()
    {
        float height = attackArea.transform.lossyScale.y/4f;
        float width = attackArea.transform.lossyScale.x / 4f;
        float depth = attackArea.transform.lossyScale.z/2f ;
        float randomHeight = Random.Range(-height, height);
        //float randomHeight = height;

        startPos = (new Vector3(width,randomHeight,-depth)+attackArea.localPosition);
        endPos = (new Vector3(-width*2,-randomHeight,-depth) + attackArea.localPosition);
    }
}
