using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

    // left duck variables
    //public GameObject leftEnemyPrefab;
    //private List<EnemyLT> allDucksLT = new List<EnemyLT>();
    //private Vector2 bottomLeft, centerLeft = Vector2.zero;


    //// right duck variables
    //public GameObject rightEnemyPrefab;
    //private List<EnemyRT> allDucksRT = new List<EnemyRT>();
    //private Vector2 bottomRight, centerRight = Vector2.zero;

    public GameObject enemyPrefab;
    private List<Enemy> allEnemies = new List<Enemy>();
    private Vector2 bottomLeft = Vector2.zero;
    private Vector2 topRight = Vector2.zero;
    public Transform[] spawnPoints;
    //private new Camera camera;
    
    private void Awake()
    {

        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 600, Camera.main.pixelHeight / 2 - 100, Camera.main.farClipPlane));
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 600, Camera.main.pixelHeight / 2, Camera.main.farClipPlane));
    }

    // Start is called before the first frame update
    void Start()
    {
        //score = 0;
        //UpdateScore();
        StartCoroutine(CreateEnemies());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 600, Camera.main.pixelHeight / 2 - 100, Camera.main.farClipPlane)), 0.5f);
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 600, Camera.main.pixelHeight / 2, Camera.main.farClipPlane)), 0.5f);

        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 150, Camera.main.pixelHeight / 2 - 100, Camera.main.farClipPlane)), 0.5f);
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 150, Camera.main.pixelHeight / 2, Camera.main.farClipPlane)), 0.5f);

    }

    //private void Spawn()
    //{
    //    float distanceFromCamera = camera.nearClipPlane; // Change this value if you want
    //    Vector3 topLeft = camera.ViewportToWorldPoint(new Vector3(0, 1, distanceFromCamera));
    //    Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, distanceFromCamera));
    //    Vector3 spawnPoint = Vector3.Lerp(topLeft, topRight, Random.value); // Get a random point between the topLeft and topRight point
    //    GameObject clone = Instantiate(prefab, spawnPoint, Quaternion.identity);
    //}

    public Vector3 GetPlanePosition()
    {

        //float distanceFromCamera = camera.nearClipPlane; // Change this value if you want
        //Vector3 topLeft = camera.ViewportToWorldPoint(new Vector3(0, 1, distanceFromCamera));
        //Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, distanceFromCamera));
        //Vector3 spawnPoint = Vector3.Lerp(topLeft, topRight, Random.value); // Get a random point between the topLeft and topRight point

        // Random position
        float targetX = Random.Range(bottomLeft.x, topRight.x);
        float targetY = Random.Range(bottomLeft.y, topRight.y);

        return new Vector3(targetX, targetY, 0);  
    }

    private IEnumerator CreateEnemies()
    {
        while (allEnemies.Count < 2)
        {

            // Create and add the enemy
            GameObject newEnemyObj = Instantiate(enemyPrefab, GetPlanePosition(), Quaternion.identity, transform);
            Enemy newEnemy = newEnemyObj.GetComponent<Enemy>();

            // Setup the enemy
            newEnemy.mEnemyManager = this;
            allEnemies.Add(newEnemy);

            yield return new WaitForSeconds(2f);
        }
    }

}