using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : CharacterController
{
    public NavMeshAgent navAgent;
    public Transform HeroPos;
    public WeaponController WeaponCollision;
    public float DamageValue;

    private bool isAttacking = false;

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
        if ((navAgent.destination - transform.position).magnitude <= 2.0f && !isAttacking)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    public override void OnDeath()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        navAgent.speed = 0.0f;
        WeaponCollision.gameObject.SetActive(true);
        WeaponCollision.transform.position = (navAgent.destination - transform.position).normalized + transform.position;

        yield return new WaitForSeconds(1.5f);

        navAgent.speed = 2.0f;
        isAttacking = false;
        WeaponCollision.gameObject.SetActive(false);
    }

    #region GETTER/SETTER

    public override float Damage { get => DamageValue; }

    #endregion
}
