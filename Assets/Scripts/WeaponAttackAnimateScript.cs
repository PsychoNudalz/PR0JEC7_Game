using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackAnimateScript : MonoBehaviour
{
    [Header("States")]
    public float swingSpeed = 2f;

    [Header("Components")]
    public Transform weaponTransform;
    public Animator weaponAnimator;
    public Transform attackArea;
    public ParticleSystem ps_bladeSparkles;

    [Header("Debugging")]
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 originalPos;
    private Quaternion originalRot;
    public bool swinging = false;
    private int swingDir = -1;
    private float timeNow;
    private bool bladeSparkles = true;

    private void Awake()
    {
        originalPos = weaponTransform.position - transform.position;
        originalRot = weaponTransform.localRotation;
    }

    private void Update()
    {
        if (swinging)
        {
            if (timeNow> (1f)|| weaponAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle"))
            {
                timeNow = 0;
                print("Reached point");
                swinging = false;
                
            }
            timeNow += Time.deltaTime;
            Vector3 slerp = Vector3.Slerp(weaponTransform.position, endPos, swingSpeed * Time.deltaTime);
            //weaponTransform.up = -(slerp - weaponTransform.position).normalized;
            weaponTransform.position = slerp;
            //weaponTransform.up = transform.forward;
        }
        else
        {
            Vector3 slerp = Vector3.Slerp(weaponTransform.position, transform.rotation * originalPos + transform.position, swingSpeed * Time.deltaTime);
            weaponTransform.position = slerp;
            if (weaponAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle") && !bladeSparkles)
            {
                ps_bladeSparkles.Stop();
                ps_bladeSparkles.Play();
                bladeSparkles = true;
            }
        }
    }

    public void swingWeapon()
    {
        swinging = true;
        pickPos();
        weaponTransform.position = startPos;
        weaponAnimator.SetTrigger("Swing");

        Vector3 dir = endPos - startPos;
        //weaponTransform.right = attackArea.forward;
        weaponTransform.rotation = Quaternion.Euler(0, attackArea.eulerAngles.y, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90+ attackArea.eulerAngles.y);
        bladeSparkles = false;

        //weaponTransform.up = -(slerp - weaponTransform.position).normalized;
        //weaponTransform.forward = (endPos + transform.position - startPos + transform.position).normalized;
        //weaponTransform.up = attackArea.forward;
    }

    void pickPos()
    {
        float height = attackArea.transform.lossyScale.y / 4f;
        float width = attackArea.transform.lossyScale.x / 5f;
        float depth = attackArea.transform.lossyScale.z / 2f;
        float randomHeight = Random.Range(-height, height);
        //float randomHeight = height;
        swingDir = -swingDir;
        //Vector3 offset = (attackArea.position - transform.position);
        Quaternion rotate = Quaternion.Euler(new Vector3(0, attackArea.eulerAngles.y, 0));
        startPos = rotate * (new Vector3(swingDir * width, randomHeight, -depth)) + attackArea.position;
        endPos = rotate * (new Vector3(swingDir * -width, -randomHeight, -depth)) + attackArea.position;
        //startPos = offset + transform.position;
        //endPos = offset + transform.position;

        Debug.DrawRay(startPos, endPos-startPos,Color.red,1f);
    }
}
