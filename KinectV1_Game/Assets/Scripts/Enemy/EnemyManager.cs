using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    //sets time delay
    float timeDelay = 30f;
    //sets the increment
    float increment = 10f;

    // left enemies variables
    //list of all left enemies 
    // sets bottomleft centre left to vector2
    public GameObject leftEnemyPrefab;
    private List<EnemyLT> allEnemiesLT = new List<EnemyLT>();
    private Vector2 bottomLeft, centerLeft = Vector2.zero;

    //right enemies variables
    //list of right enemies
    //sets bottom right and centre right for future use
    public GameObject rightEnemyPrefab;
    private List<EnemyRT> allEnemiesRT = new List<EnemyRT>();
    private Vector2 bottomRight, centerRight = Vector2.zero;

    private void Awake()
    {
        // sets the center left , bottom left , center right , bottom right positions on the camera

        centerLeft = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 1500, Camera.main.pixelHeight / 2 - 140, Camera.main.farClipPlane));
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 1500, Camera.main.pixelHeight / 2 - 180, Camera.main.farClipPlane));

        centerRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 200, Camera.main.pixelHeight / 2 - 140, Camera.main.farClipPlane));
        bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 200, Camera.main.pixelHeight / 2 - 180, Camera.main.farClipPlane));
    }

    // Start is called before the first frame update
    void Start()
    {   
        //creates the enemies 
        StartCoroutine(CreateEnemies());
    }

    void Update()
    {
        //sets the time delay to time
        timeDelay += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        // draws the spawn points for the enemies on the screen for spawning 
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 1500, Camera.main.pixelHeight / 2 - 140, Camera.main.farClipPlane)), 0.5f);
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 1500, Camera.main.pixelHeight / 2 - 180, Camera.main.farClipPlane)), 0.5f);

        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 200, Camera.main.pixelHeight / 2 - 140, Camera.main.farClipPlane)), 0.5f);
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 200, Camera.main.pixelHeight / 2 - 180, Camera.main.farClipPlane)), 0.5f);

    }

    public Vector3 GetPlanePosition()
    {

        // Random position
        float targetX = Random.Range(bottomLeft.x, centerLeft.x);
        float targetY = Random.Range(bottomLeft.y, centerLeft.y);

        return new Vector3(targetX, targetY, 0);
    }


    public Vector3 GetPlanePositionLeft()
    {

        // Random position
        float targetX = Random.Range(bottomLeft.x, centerLeft.x);
        float targetY = Random.Range(bottomLeft.y, centerLeft.y);

        return new Vector3(targetX, targetY, 0);
    }


    public Vector3 GetPlanePositionRight()
    {

        // Random position
        float targetX = Random.Range(bottomRight.x, centerRight.x);
        float targetY = Random.Range(bottomRight.y, centerRight.y);

        return new Vector3(targetX, targetY, 0);
    }

    private IEnumerator CreateEnemies()
    {
        while (true)
        {

            // Create and add the enemies
            // Left
            GameObject newEnemyObjLeft = Instantiate(leftEnemyPrefab, GetPlanePositionLeft(), Quaternion.identity, transform);
            EnemyLT newEnemyLeft = newEnemyObjLeft.GetComponent<EnemyLT>();

            // Right
            // Create and add the enemies
            GameObject newEnemyObjRight = Instantiate(rightEnemyPrefab, GetPlanePositionRight(), Quaternion.identity, transform);
            EnemyRT newEnemyRight = newEnemyObjRight.GetComponent<EnemyRT>();

            // Set up Enemies
            // Left
            newEnemyLeft.mEnemyManager = this;
            allEnemiesLT.Add(newEnemyLeft);

            // Right
            newEnemyRight.mEnemyManager = this;
            allEnemiesRT.Add(newEnemyRight);

            yield return new WaitForSeconds(timeDelay);
            timeDelay -= increment;
        }
    }
}
