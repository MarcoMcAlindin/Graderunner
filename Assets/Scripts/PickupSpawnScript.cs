using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnScript : MonoBehaviour {

    public GameObject spawnPrefabA;

    public GameObject spawnPrefabB;

    public GameObject spawnPrefabC;

    // Use this for initialization
    void Start()
    {
        int rng = Random.Range(0, 10);

        switch (rng)
        {
            case 0:
                SpawnA();
                break;
            case 1:
                SpawnB();
                break;
            case 2:
                SpawnC();
                break;
        }
    }
    public void SpawnA()
    {
        GameObject pickup = (GameObject)Instantiate(spawnPrefabA,
        transform.position, Quaternion.identity);
        pickup.name = "PointsPickup";
    }

    public void SpawnB()
    {
        GameObject pickup = (GameObject)Instantiate(spawnPrefabB,
        transform.position, Quaternion.identity);
        pickup.name = "TimePickup";
    }

    public void SpawnC()
    {
        GameObject pickup = (GameObject)Instantiate(spawnPrefabC,
        transform.position, Quaternion.identity);
        pickup.name = "SpeedPickup";
    }
}
