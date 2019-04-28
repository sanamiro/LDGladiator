using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public Transform HeroPos;

    public void SpawnEnemy(Transform SpawnPos, EnemyController.EnemyType EnemyType)
    {
        GameObject newEnemy = Instantiate(EnemyPrefab, SpawnPos);
        newEnemy.GetComponent<EnemyController>().HeroPos = HeroPos;
        newEnemy.GetComponent<EnemyController>().enemyType = EnemyType;
    }

}
