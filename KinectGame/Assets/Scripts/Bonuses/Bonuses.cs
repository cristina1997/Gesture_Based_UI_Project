using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public float spawnTime = .5f;
    public GameObject[] Coins;

    void Start()
    {
        InvokeRepeating("SpawnCoins", spawnTime, spawnTime);
    }

    void SpawnCoins()
    {
        Transform currentSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform;
        Instantiate(Coins[Random.Range(0, Coins.Length)], currentSpawnPoint.position, currentSpawnPoint.rotation);

    }
}
