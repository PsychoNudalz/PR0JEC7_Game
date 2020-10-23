using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CoinDistribution : MonoBehaviour
{
    public GameObject myPrefab;
    [SerializeField]
    private float levelSize;
    [SerializeField]
    private int numberOfCoins;

    private NavMeshHit myNavHit;
    private float maxDist = 0.8f;

    void Awake()
    {
        for(int i = 0; i < numberOfCoins; i++)
        {
            bool validPosition = false;
            while (!validPosition)
            {
                Vector3 position = new Vector3(Random.RandomRange(0 - levelSize, levelSize), 0.0f, Random.RandomRange(-levelSize, levelSize));
                Collider[] collisions = Physics.OverlapSphere(position, 2f);
                
                validPosition = true;
               
                foreach(Collider collider in collisions)
                {
                    if (collider.CompareTag("Coin"))
                    {
                        
                        validPosition = false;
                        break;
                    }
                }
                
                if (NavMesh.SamplePosition(position, out myNavHit, maxDist, 1 << NavMesh.GetAreaFromName("Walkable")) && validPosition)
                {
                    GameObject coin = Instantiate(myPrefab, myNavHit.position, Quaternion.identity);
                    coin.transform.parent = this.transform;
                }
                else
                {
                    validPosition = false;
                }
            }
        }
        Debug.Log( FindObjectsOfType <Coin> ().Length + " Coins Created");
    }
}
