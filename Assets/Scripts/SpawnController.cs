using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] spawnPoint;
    public GameObject[] vehicles;
    [SerializeField] int laneSpeed = 30;
    float laneSpeedMultiplier = 1.33f;

    void Start()
    {
        InvokeRepeating("Spawner", 0, .5f);
        InvokeRepeating("Spawner", 1, .5f);
    }

    void Spawner()
    {
        int vehicleIndex = Random.Range(0, vehicles.Length);
        int spawnIndex = Random.Range(0, spawnPoint.Length);

    // the fast lanes are faster!
    if(spawnIndex == 1 || spawnIndex == 3) {
        vehicles[vehicleIndex].GetComponent<MoveForwards>().speed = laneSpeed * laneSpeedMultiplier;
    } else {
        vehicles[vehicleIndex].GetComponent<MoveForwards>().speed = laneSpeed;
    }
        Instantiate(vehicles[vehicleIndex], spawnPoint[spawnIndex].transform.position, spawnPoint[spawnIndex].transform.rotation);
    }
}
