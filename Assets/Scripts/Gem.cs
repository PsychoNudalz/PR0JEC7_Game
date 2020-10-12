using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour
{

    public GemColour colour;
    public GemCollection gemCollection;
    public Image image;
    public Sprite sprite;
    public Vector3 rotationAngle;
    public float rotationSpeed;

    public float floatSpeed;
    private bool goingUp = true;
    public float floatRate;
    private float floatTimer;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);

        floatTimer += Time.deltaTime;
            Vector3 moveDir = new Vector3(0.0f, 0.0f, floatSpeed);
            transform.Translate(moveDir);

            if (goingUp && floatTimer >= floatRate)
            {
                goingUp = false;
                floatTimer = 0;
                floatSpeed = -floatSpeed;
            }

            else if(!goingUp && floatTimer >= floatRate)
            {
                goingUp = true;
                floatTimer = 0;
                floatSpeed = +floatSpeed;
            }
    }

    private void OnTriggerEnter(Collider other){
       
        if(other.gameObject.CompareTag("Enemy")){
            gemCollection.CollectGem(colour);
            this.gameObject.SetActive(false);
            image.sprite = sprite;
        }
    }

}
