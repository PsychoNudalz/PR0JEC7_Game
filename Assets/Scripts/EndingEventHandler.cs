﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingEventHandler : MonoBehaviour
{
    GameManager gameManager;
    GameObject defeatedScreen;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    public void TriggerOnDeath() {
        defeatedScreen = this.gameObject.transform.GetChild(2).gameObject;
        defeatedScreen.GetComponentInChildren<TMP_Text>().text = "Coins Collected: " + gameManager.coinsCollected;
        defeatedScreen.SetActive(true);
    }

    public void RetryOnClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevelOnClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainOnClick() {
        SceneManager.LoadScene("StartMenu");
    }

    public void ExitOnClick() {
        Application.Quit();
    }
}
