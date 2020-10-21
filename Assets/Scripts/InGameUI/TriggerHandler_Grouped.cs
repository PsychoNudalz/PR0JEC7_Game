using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler_Grouped : MonoBehaviour
{
    public GameObject player;
    public GameObject[] currentTutorialGroup, nextTutorialGroup;

    void onTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            this.gameObject.SetActive(false);
            foreach (var item in currentTutorialGroup) {
                item.SetActive(false);
            }
            this.gameObject.SetActive(false);
            foreach (var item in nextTutorialGroup) {
                item.SetActive(true);
            }
        }
    }
}
