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
    public AnimatorController animController;
    public Animator animator;
    public UseableWeaponController MeleeWeapon;
    public UseableWeaponController RangedWeapon;
    public EnemyType enemyType = EnemyType.None;

    [Header("Enemy Values")]
    public float MaxHealth;
    public float CurrentHealth;
    public float previousHealth;
    public float DamageValue;
    public float Speed;
    public float AttackCD;

    private UseableWeaponController weaponCollision;
    private bool isAttacking = false;
    private bool isRunning = false;
    private bool looksRight = false;
    private bool hasBeenSetUp = false;

    // Update is called once per frame
    void Update()
    {
        if (enemyType != EnemyType.None && !hasBeenSetUp)
            CustomEnemy(enemyType);
        MoveEnemy();

        if (navAgent.destination.x - transform.position.x > 0 && !looksRight)
        {
            this.transform.Rotate(new Vector3(0, 1, 0), 180);
            looksRight = true;
        }
        if (navAgent.destination.x - transform.position.x < 0 && looksRight)
        {
            this.transform.Rotate(new Vector3(0, 1, 0), 180);
            looksRight = false;
        }
        if(previousHealth != CurrentHealth) // check if health lost to launch sound
        {
            if (enemyType == EnemyType.Light)
            {
                AudioManager.instance.Play("ig ennemy 1 get hit");
            }
            else if (enemyType == EnemyType.Medium)
            {
                AudioManager.instance.Play("ig ennemy 2 get hit");
            }
            else if (enemyType == EnemyType.Heavy)
            {
                AudioManager.instance.Play("ig ennemy 3 get hit");
            }
            else if (enemyType == EnemyType.Ranged)
            {
                AudioManager.instance.Play("ig ennemy 4 get hit");
            }
        }
        previousHealth = CurrentHealth;
    }

    private void MoveEnemy()
    {
        navAgent.SetDestination(HeroPos.position);
        if (!isRunning && !isAttacking)
        {
            animator.SetTrigger("run_cycle");
            isRunning = true;
        }

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance /*(navAgent.destination - transform.position).magnitude <= 2.0f*/ && !isAttacking)
        {
            StartCoroutine(AttackPlayer());
            if(enemyType == EnemyType.Light)
            {
                AudioManager.instance.Play("ig ennemy 1 attack");
            }
            else if (enemyType == EnemyType.Medium)
            {
                AudioManager.instance.Play("ig ennemy 2 attack");
            }
            else if (enemyType == EnemyType.Heavy)
            {
                AudioManager.instance.Play("ig ennemy 3 attack");
            }
            else if (enemyType == EnemyType.Ranged)
            {
                AudioManager.instance.Play("ig ennemy 4 attack");
            }
        }
    }

    public override void OnDeath()
    {
        this.gameObject.SetActive(false);
        GameManager.OnEnemyKilled();
        if (enemyType == EnemyType.Light)
        {
            AudioManager.instance.Play("ig ennemy 1 fall");
            AudioManager.instance.Play("ig ennemy 1 die");
        }
        else if (enemyType == EnemyType.Medium)
        {
            AudioManager.instance.Play("ig ennemy 2 fall");
            AudioManager.instance.Play("ig ennemy 2 die");
        }
        else if (enemyType == EnemyType.Heavy)
        {
            AudioManager.instance.Play("ig ennemy 3 fall");
            AudioManager.instance.Play("ig ennemy 3 die");
        }
        else if (enemyType == EnemyType.Ranged)
        {
            AudioManager.instance.Play("ig ennemy 4 fall");
            AudioManager.instance.Play("ig ennemy 4 die");
        }
    }

    private IEnumerator AttackPlayer()
    {
        animator.SetTrigger("idle");
        isAttacking = true;
        isRunning = false;
        navAgent.speed = 0.0f;
        Vector3 dir = (navAgent.destination - transform.position).normalized;
        animController.attackAnimation();
        yield return new WaitForSecondsRealtime(0.25f);
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
                DamageValue = 4;
                MaxHealth = 30;
                CurrentHealth = MaxHealth;
                Speed = 3;
                AttackCD = 1;
                break;

            case EnemyType.Medium:
                DamageValue = 5;
                MaxHealth = 50;
                CurrentHealth = MaxHealth;
                Speed = 2;
                AttackCD = 2;
                break;

            case EnemyType.Heavy:
                DamageValue = 16;
                MaxHealth = 100;
                CurrentHealth = MaxHealth;
                Speed = 1;
                AttackCD = 4;
                break;

            case EnemyType.Ranged:
                DamageValue = 1;
                MaxHealth = 30;
                CurrentHealth = MaxHealth;
                Speed = 2;
                AttackCD = 2;
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
