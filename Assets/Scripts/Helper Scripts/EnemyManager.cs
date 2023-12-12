using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private const float ZPositionRadiusRange = -1.5f;
    public Transform player; // Reference to the player's transform
    public Camera mainCamera; // Reference to the main camera
    public float xOffset = 1f; // Horizontal offset between the enemy manager and the game view
    private float leftBound; // Left boundary of the game view
    private float rightBound; // Right boundary of the game view

    private float rightBarrier; // The enemy should be spawn in front of the barrier
    public static EnemyManager instance;


    private int generatedFromPoolEnemies = 0; // Current number of spawned enemies
    public int maxEnemies = 5; // Maximum number of enemies to spawn
    public int maxPreSpawnEnemies = 5; // Maximum number of enemies to spawn in the beginning of the game
    public GameObject enemyPrefab; // Prefab of the enemy
    private int killedEnemyQuota; // number of enemy needed to kill to clear the level
    private float enemyHealth;

    private Queue<GameObject> enemyPool = new Queue<GameObject>(); // Queue to store enemy instances
    private List<GameObject> activeEnemies = new List<GameObject>(); // List to store active enemies

    public int KilledEnemyQuota { get => killedEnemyQuota; set => killedEnemyQuota = value; }
    public int GeneratedFromPoolEnemies { get => generatedFromPoolEnemies; set => generatedFromPoolEnemies = value; }

    private void Start()
    {
        GetEnemyInstance();
        enemyHealth = enemyPrefab.GetComponent<HealthScript>().health;
        // Calculate the left &right boundary of the game view based on the camera's position and orthographic size
        float halfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        leftBound = mainCamera.transform.position.x - halfWidth;
        rightBound = mainCamera.transform.position.x + halfWidth;

        // Find the Ground object and get its BoxCollider component
        GameObject groundObject = GameObject.Find("LevelColliders/RightRoadBarrierCollider");
        BoxCollider groundCollider = groundObject.GetComponent<BoxCollider>();

        rightBarrier = groundCollider.bounds.max.x - xOffset;

        // Spawn all enemies at random positions between the left and right boundaries
        for (int i = 0; i < maxPreSpawnEnemies; i++)
        {
            float randomX = Random.Range(leftBound + xOffset, rightBarrier - xOffset);
            float randomZ = player.position.z + Random.Range(-Mathf.Abs(ZPositionRadiusRange), Mathf.Abs(ZPositionRadiusRange));
            Vector3 spawnPosition = new Vector3(randomX, player.position.y, randomZ);
            RandomizeEnemyColor(Instantiate(enemyPrefab, spawnPosition, Quaternion.identity));
            // currentEnemies++;
        }

        killedEnemyQuota = maxPreSpawnEnemies + maxEnemies;
    }

    private void LateUpdate()
    {
        // Move the enemy manager horizontally to always be slightly out of the game view on the right side
        float halfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        leftBound = mainCamera.transform.position.x - halfWidth;
        float targetX = Mathf.Max(rightBarrier, leftBound - xOffset);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);

        // Update the enemy manager's position to follow the player and camera movement
        transform.position += (player.position - transform.position) * Time.deltaTime;

        // Check if we can spawn more enemies
        if (GeneratedFromPoolEnemies < maxEnemies)
        {
            // Check if the player has moved to the right side of the screen
            if (player.position.x < leftBound)
            {
                // Spawn an enemy from the side of the screen
                Vector3 spawnPosition = new Vector3(leftBound - xOffset, player.position.y, player.position.z);
                GetEnemyInstance().transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            }
        }
    }

    public GameObject GetEnemyInstance()
    {
        if (enemyPool.Count == 0)
        {
            // If the pool is empty, spawn more enemy
            GameObject enemyInstance = InstantiateEnemy();
            enemyInstance.SetActive(false);
            enemyPool.Enqueue(enemyInstance);
        }

        // Dequeue an enemy instance from the pool and set its position and rotation
        GameObject newEnemy = enemyPool.Dequeue();
        newEnemy.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        RandomizeEnemyColor(newEnemy);

        newEnemy.SetActive(true);

        activeEnemies.Add(newEnemy);

        // Increase the count of spawned enemies
        GeneratedFromPoolEnemies++;
        Debug.Log("GENERATED ENEMIES SO FAR: "
            + (maxPreSpawnEnemies + GeneratedFromPoolEnemies));

        return newEnemy;
    }

    private void RandomizeEnemyColor(GameObject newEnemy)
    {

        // Randomize the enemy's color
        Renderer enemyRenderer = newEnemy.GetComponentInChildren<Renderer>();
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = GetRandomColor();
        }
    }

    public void DestroyEnemy(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            ReturnEnemyInstance(enemy);
        }
    }

    private void ReturnEnemyInstance(GameObject enemy)
    {
        // Reset the position and rotation of the returned enemy instance
        enemy.transform.position = Vector3.zero;
        enemy.transform.rotation = Quaternion.identity;
        enemy.GetComponent<EnemyMovement>().enabled = true;
        enemy.layer = LayerMask.NameToLayer("Enemy");
        enemy.GetComponent<HealthScript>().health = enemyHealth;
        enemy.SetActive(false);
        enemy.GetComponent<HealthScript>().Revive();

        // Enqueue the returned enemy instance back into the pool
        enemyPool.Enqueue(enemy);
    }

    private GameObject InstantiateEnemy()
    {
        // Instantiate a new enemy instance from the prefab
        return Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // enemyList = new List<GameObject>();
        mainCamera = Camera.main;
    }

    // public void SpawnEnemy()
    // {
    //     if(enemyList.Count == enemyMaxCnt)
    //     {
    //         //TODO: reuse instead of spawning
    //         return;
    //     }
    //     enemyList.Add(Instantiate(enemyPrefab, transform.position, Quaternion.identity));                
    //     // print("spawned new enemy number " + enemyList.Count);
    // }
    // void Start()
    // {
    //     SpawnEnemy();
    // }

}
