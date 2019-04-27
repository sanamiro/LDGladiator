using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum ActionState
    {
        Attack,
        AttackCooldown,
        Parry,
        None
    }

    public float Speed;
    public Animator Animator;
    public WeaponController WeaponCollision;
    public float AttackDuration;
    public float AttackCooldown;

    //Movement
    private Rigidbody rigidBody;
    private Vector2 velocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;

    //Attack
    private Plane planeXZ;
    private bool attackTriggered = false;
    private bool shieldTriggered = false;
    private ActionState state = ActionState.None;
    private float attackTimer;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        planeXZ = new Plane(Vector3.up, transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
        targetVelocity.x = Input.GetAxisRaw("Horizontal");
        targetVelocity.y = Input.GetAxisRaw("Vertical");
        targetVelocity.Normalize();
        if (Animator != null)
        {
            Animator.SetFloat("SpeedX", targetVelocity.x);
            Animator.SetFloat("SpeedY", targetVelocity.y);
        }

        if (Input.GetMouseButtonDown(0)) attackTriggered = true;
        shieldTriggered = Input.GetMouseButtonDown(0);

        if (state != ActionState.Attack)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float dist;
            if (planeXZ.Raycast(mouseRay, out dist))
            {
                transform.LookAt(mouseRay.GetPoint(dist));
            }
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = Speed;

        if (state == ActionState.Parry) currentSpeed /= 2;

        Move(targetVelocity * currentSpeed * Time.fixedDeltaTime);

        if (attackTimer > 0)
        {
            attackTimer -= Time.fixedDeltaTime;
        }
        switch(state)
        {
            case ActionState.None:
                if (shieldTriggered)
                {
                    state = ActionState.Parry;
                }
                else if (attackTriggered)
                {
                    attackTriggered = false;
                    state = ActionState.Attack;
                    attackTimer = AttackCooldown;
                    WeaponCollision.gameObject.SetActive(true);
                }
                break;
            case ActionState.Attack:
                if (attackTimer <= 0)
                {
                    state = ActionState.AttackCooldown;
                    attackTimer = AttackDuration;
                    WeaponCollision.gameObject.SetActive(false);
                }
                break;
            case ActionState.AttackCooldown:
                if (attackTimer <= 0)
                {
                    state = ActionState.None;
                }
                break;
            case ActionState.Parry:
                if (!shieldTriggered)
                {
                    state = ActionState.None;
                }
                break;
        }
        
    }

    private void Move(Vector2 targetSpeed)
    {
        rigidBody.velocity = new Vector3(targetSpeed.x, 0, targetSpeed.y);
    }
}