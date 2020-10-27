using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            if (gameManager.allGemsCollected()) {
                endingType.SetActive(true);
            }
        }
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
