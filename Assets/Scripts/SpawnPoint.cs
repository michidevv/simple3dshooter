using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemyPrefabs = new List<Enemy>();
        
    [SerializeField]
    private float spawnDelay = 10f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0f, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        int index = Random.Range(0, enemyPrefabs.Count);
        Enemy enemy = Instantiate(enemyPrefabs[index], transform.position, 
            Quaternion.identity) as Enemy;
        enemy.Target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
