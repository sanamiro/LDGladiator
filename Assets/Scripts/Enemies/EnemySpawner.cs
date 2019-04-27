using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public Transform HeroPos;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy(this.transform);
        transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        SpawnEnemy(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(Transform SpawnPos)
    {
        GameObject newEnemy = Instantiate(EnemyPrefab, SpawnPos);
        newEnemy.GetComponent<EnemyController>().HeroPos = HeroPos;
    }

}
