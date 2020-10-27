using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyLifeSystemScript : LifeSystemScript
{
    /*
    [SerializeField]
    private GameObject respawnPrefab;
    [SerializeField]
    private float respawnDelay = 5f;
    private EnemySpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();        
    }

    public override void DeathBehaviour()
    {
        if (deathGameObject != null)
        {
            Instantiate(deathGameObject, deathGameObject.transform.position, deathGameObject.transform.rotation).SetActive(true);
        }

        StartCoroutine(spawner.RespawnEnemyChicken(respawnPrefab, this.gameObject.GetComponent<ChickenAgent>().initialPosition, respawnDelay, this.gameObject.GetComponent<ChickenAgent>().waypointsToFollow));

        if (disableOnDeath)
        {
            gameObject.SetActive(false);
        }
        else if (destroyOnDeath)
        {
            Destroy(gameObject);
        }


    }

   */
}
