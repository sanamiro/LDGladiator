using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : CharacterController
{
    public enum EnemyType
    {
        Light,
        Medium,
        Heavy,
        Ranged,
        None
    }

    public NavMeshAgent navAgent;
    public Transform HeroPos;
    public UseableWeaponController MeleeWeapon;
    public UseableWeaponController RangedWeapon;
    public EnemyType enemyType = EnemyType.None;

    [Header("Enemy Values")]
    public float MaxHealth;
    public float CurrentHealth;
    public float DamageValue;
    public float Speed;
    public float AttackCD;

    private UseableWeaponController weaponCollision;
    private bool isAttacking = false;
    private bool hasBeenSetUp = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType != EnemyType.None && !hasBeenSetUp)
            CustomEnemy(enemyType);
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        navAgent.SetDestination(HeroPos.position);
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance /*(navAgent.destination - transform.position).magnitude <= 2.0f*/ && !isAttacking)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    public override void OnDeath()
    {
        this.gameObject.SetActive(false);
        GameManager.OnEnemyKilled();
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        navAgent.speed = 0.0f;
        Vector3 dir = (navAgent.destination - transform.position).normalized;
        weaponCollision.StartUseWeapon(transform.position + dir + Vector3.up, dir);

        yield return new WaitForSeconds(1.5f * AttackCD);

        weaponCollision.StopUseWeapon();
        navAgent.speed = Speed;
        isAttacking = false;
    }

    private void CustomEnemy(EnemyType enemyType)
    {
        hasBeenSetUp = true;
        weaponCollision = MeleeWeapon;
        switch (enemyType)
        {
            case EnemyType.Light:
                DamageValue = 1;
                MaxHealth = 100;
                CurrentHealth = MaxHealth;
                Speed = 1;
                AttackCD = 1;
                break;

            case EnemyType.Medium:
                DamageValue = 1;
                MaxHealth = 100;
                CurrentHealth = MaxHealth;
                Speed = 1;
                AttackCD = 1;
                break;

            case EnemyType.Heavy:
                DamageValue = 1;
                MaxHealth = 100;
                CurrentHealth = MaxHealth;
                Speed = 1;
                AttackCD = 1;
                break;

            case EnemyType.Ranged:
                DamageValue = 1;
                MaxHealth = 100;
                CurrentHealth = MaxHealth;
                Speed = 1;
                AttackCD = 1;
                navAgent.stoppingDistance = 8;
                weaponCollision = RangedWeapon;
                break;

            default:
                break;
        }
    }

    #region GETTER/SETTER

    public override float Damage { get => DamageValue; }

    #endregion
}
