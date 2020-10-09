using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GemCollection gemCollection;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("Collision");
        if(other.gameObject.CompareTag("Player")){
            gemCollection.collectGem();
            this.gameObject.SetActive(false);
        }
    }
}
