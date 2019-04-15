using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    //Spawn points object for the health pick ups
    public Transform[] SpawnPoints;
    //the spawn time between each pick up
    public float spawnTime = .5f;
    //Bonus game object for the pick ups (health)
    public GameObject[] Bonus;

    void Start()
    {   //spawns the pick up including the spawn time
        InvokeRepeating("SpawnCoins", spawnTime, spawnTime);
    }

    void SpawnCoins()
    {   
        //spawns them at random from the spawn points given and rotates the object creatine a falling effect
        Transform currentSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform;
        Instantiate(Bonus[Random.Range(0, Bonus.Length)], currentSpawnPoint.position, currentSpawnPoint.rotation);

    }
}
