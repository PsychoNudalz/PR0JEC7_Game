using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public IEnumerator RespawnEnemyChicken(GameObject prefab, Vector3 position, float delay, Transform waypoints)
    {
        Debug.Log("Respawning..." + prefab.ToString() + position.ToString() + delay + waypoints.ToString());
        yield return new WaitForSecondsRealtime(delay);
        Debug.Log("Done");
        GameObject respawn = Instantiate(prefab, position, Quaternion.identity);
        respawn.GetComponent<ChickenAgent>().waypointsToFollow = waypoints;

    }

    public void SpawnChicken(GameObject prefab, Vector3 position, float delay, Transform waypoints)
    {
        
    }
     
}
