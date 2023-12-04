using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField]
    private GameObject enemyPrefab;
    private List<GameObject> enemyList;
    private readonly int enemyMaxCnt = 10;

    void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        enemyList = new List<GameObject>();
    }

    public void SpawnEnemy()
    {
        if(enemyList.Count == enemyMaxCnt)
        {
            //TODO: reuse instead of spawn
            return;
        }
        enemyList.Add(Instantiate(enemyPrefab, transform.position, Quaternion.identity));                
        // print("spawned new enemy number " + enemyList.Count);
    }
    void Start()
    {
        SpawnEnemy();
    }
}
