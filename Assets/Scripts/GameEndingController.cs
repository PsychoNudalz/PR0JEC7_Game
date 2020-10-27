﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndingController : MonoBehaviour
{
    GameObject player;
    GameManager gameManager;
    public GameObject endingType;


    private void Awake()
    {
        player = FindObjectOfType<PlayerMasterHandlerScript>().gameObject;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TriggerOnDeath() {
        endingType = FindObjectOfType<Defeated_Flag>().gameObject;
        endingType.GetComponentInChildren<TMP_Text>().text = "Coins Collected: " + gameManager.coinsCollected;
        endingType.SetActive(true);

    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            if (gameManager.allGemsCollected()) {
                endingType.GetComponentInChildren<TMP_Text>().text = "Coins Collected: " + gameManager.coinsCollected;
                endingType.SetActive(true);
            }
        }
    }

    public void NextLevelOnClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RetryOnClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainOnClick() {
        SceneManager.LoadScene("StartMenu");
    }

    public void ExitOnClick() {
        Application.Quit();
    }
}
