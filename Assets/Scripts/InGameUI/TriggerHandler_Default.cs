using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler_Default : MonoBehaviour
{
    public GameObject player;
    public GameObject nextTutorial;

    void onTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            this.gameObject.SetActive(false);
            nextTutorial.SetActive(true);
        }
    }
}
