using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy {Chicken, Skeleton };
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject chickenPrefab;
    [SerializeField]
    private GameObject skeletonPrefab;

    public void SpawnEnemy(Enemy enemyType, Vector3 position, float delay, Transform waypoints)
    {
        if(enemyType.Equals(Enemy.Chicken)){
            StartCoroutine(RespawnChicken(position, delay, waypoints));
        }
        
    }

    public IEnumerator RespawnChicken(Vector3 position, float delay, Transform waypoints)
    {
        Debug.Log("Respawning Chicken..." + position + "  -  " + delay + "  -  " + waypoints);
        yield return new WaitForSecondsRealtime(delay);
        GameObject respawn = Instantiate(chickenPrefab, position, Quaternion.identity);
        respawn.GetComponent<ChickenAgent>().waypointsToFollow = waypoints;
        respawn.GetComponent<ChickenAgent>().enabled = true;
        respawn.GetComponent<ChickenAgent>().ResetChicken();
        Debug.Log("Done");
    }

     
}
