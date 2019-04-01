using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckManager : MonoBehaviour
{
    public GameObject duckPrefab;

    private List<Duck> allDucks = new List<Duck>();
    private Vector2 topLeft = Vector2.zero;
    private Vector2 topRight = Vector2.zero;
    //private new Camera camera;

    public Text scoreText;
    private int score;

    private void Awake()
    {
        topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.farClipPlane));
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(
            Camera.main.pixelWidth, 
            Camera.main.pixelHeight / 2, 
            Camera.main.farClipPlane));
        
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(CreateDucks());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane)), 0.05f);
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(
                            Camera.main.pixelWidth,
                            Camera.main.pixelHeight,
                            Camera.main.farClipPlane)), 0.05f);
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
        // Random position
        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        float targetX = Random.Range(topLeft.x, topRight.x);
        float targetY = Random.Range(topLeft.y, topRight.y);

        //float distanceFromCamera = camera.nearClipPlane; // Change this value if you want
        //Vector3 topLeft = camera.ViewportToWorldPoint(new Vector3(0, 1, distanceFromCamera));
        //Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, distanceFromCamera));
        //Vector3 spawnPoint = Vector3.Lerp(topLeft, topRight, Random.value); // Get a random point between the topLeft and topRight point

        return new Vector3(targetX, targetY, 0);
    }

    private IEnumerator CreateDucks()
    {
        while (allDucks.Count < 20)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
            // Create and add the ducks
            GameObject newDuckObj = Instantiate(duckPrefab, GetPlanePosition(), Quaternion.identity, transform);
            Duck newDuck = newDuckObj.GetComponent<Duck>();

            // Setup the duck
            newDuck.mDuckManager = this;
            allDucks.Add(newDuck);

            yield return new WaitForSeconds(0.5f);
        }
    }
    
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

}
