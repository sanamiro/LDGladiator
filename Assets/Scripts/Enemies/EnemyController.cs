using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform HeroPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    public void UpdateHeroPosition(Transform newPos)
    {
        HeroPos = newPos;
    }

    private void MoveEnemy()
    {
        navAgent.SetDestination(HeroPos.position);
    }


    #region GETTER/SETTER



    #endregion
}
