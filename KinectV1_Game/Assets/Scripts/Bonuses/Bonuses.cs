using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public float spawnTime = .5f;
    public GameObject[] Bonus;

    void Start()
    {
        InvokeRepeating("SpawnCoins", spawnTime, spawnTime);
    }

    void SpawnCoins()
    {
        Transform currentSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform;
        Instantiate(Bonus[Random.Range(0, Bonus.Length)], currentSpawnPoint.position, currentSpawnPoint.rotation);

    }
}
