using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    private enum ActionState
    {
        Attack,
        AttackCooldown,
        Parry,
        None
    }

    public float BaseSpeed;
    public Animator Animator;
    public AnimatorController animController;
    public UseableWeaponController WeaponCollision;
    public float AttackDuration;
    public float AttackCooldown;
    
    public float shieldSize = 100;

    //Movement
    private Rigidbody rigidBody;
    private Vector2 velocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;

    //Attack
    private EquipmentInfo equipment = new EquipmentInfo()
    { //Default value
        swordLevel = 0,
        armorLevel = 0,
        sandalLevel = 0,
        capeLevel = 0
    };

    private Plane planeXZ;
    private bool attackTriggered = false;
    private bool shieldTriggered = false;
    private bool hasJoystick = false;
    private bool looksRight = false;
    private bool isRunning = false;
    private float triggerint = 0;
    private ActionState state = ActionState.None;
    private float attackTimer;
    private int comboState = 0;

    private MouseManager mouseManager;

    [Space]
    [Header("Sons")]
    public AudioClip[] sonAttack = new AudioClip[5];
    public AudioClip[] sonGetHit = new AudioClip[4];
    public AudioClip[] sonFootStep = new AudioClip[4];
    public AudioClip[] sonShieldGetHit = new AudioClip[5];
    public AudioClip[] sonShieldUp = new AudioClip[2];
    public AudioClip sonDeath;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        mouseManager = GetComponent<MouseManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        planeXZ = new Plane(Vector3.up, transform.position);
        if (Input.GetJoystickNames().Length != 0 || Input.GetJoystickNames().GetValue(0).ToString() != "")
        {
            Debug.Log("Joystick detected");
            hasJoystick = true;
            GetComponent<MouseManager>().hasJoystick = true;
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (!hasJoystick)
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
            shieldTriggered = Input.GetMouseButton(1);

            if (Input.GetAxisRaw("Horizontal") < 0 && !looksRight && !!shieldTriggered)
            {
                this.transform.Rotate(new Vector3(0, 1, 0), 180);
                looksRight = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && looksRight && !shieldTriggered)
            {
                this.transform.Rotate(new Vector3(0, 1, 0), 180);
                looksRight = false;
            }

            if (targetVelocity.magnitude == 0 && isRunning)
            {
                animController.changeRunningAnimation();
                isRunning = false;
            }
            else if (targetVelocity.magnitude != 0 && !isRunning)
            {
                animController.changeRunningAnimation();
                isRunning = true;
            }
        }

        else
        {
            targetVelocity.x = -Input.GetAxisRaw("Horizontal");
            targetVelocity.y = -Input.GetAxisRaw("Vertical");
            targetVelocity.Normalize();
            if (Animator != null)
            {
                Animator.SetFloat("SpeedX", targetVelocity.x);
                Animator.SetFloat("SpeedY", targetVelocity.y);
            }

            if (Input.GetAxisRaw("Horizontal") < 0 && !looksRight && !shieldTriggered)
            {
                this.transform.Rotate(new Vector3(0, 1, 0), 180);
                looksRight = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && looksRight && !shieldTriggered)
            {
                this.transform.Rotate(new Vector3(0, 1, 0), 180);
                looksRight = false;
            }

            if (Input.GetAxis("RT1") >= 0.9f && triggerint == 0)
                triggerint++;

            if (Input.GetButtonDown("A1") || Input.GetButtonDown("X1") || triggerint == 1) attackTriggered = true;

            shieldTriggered = (Input.GetButton("B1") || Input.GetButton("Y1") || Input.GetAxis("LT1") <= -0.9f);

            if (Input.GetAxis("RT1") <= 0.1f && triggerint == 2)
                triggerint = 0;
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = BaseSpeed * (1 + equipment.SpeedBonus);

        if (state == ActionState.Parry || state == ActionState.Attack) currentSpeed /= 2;

        Move(targetVelocity * currentSpeed * Time.fixedDeltaTime);

        if (attackTimer > 0)
        {
            attackTimer -= Time.fixedDeltaTime;
        }
        
        switch (state)
        {
            case ActionState.None:
                
                if (shieldTriggered)
                {
                    state = ActionState.Parry;
                    animController.blockAnimation();
                }
                else if (attackTimer <= 0 && attackTriggered)
                {
                    if (triggerint == 1)
                        triggerint++;
                    comboState = 1;
                    attackTriggered = false;
                    state = ActionState.Attack;
                    attackTimer = AttackCooldown;
                    WeaponCollision.StartUseWeapon(WeaponCollision.transform.position, Vector3.zero);
                    animController.attackAnimation();
                    launchRandomSound("ig player attack");
                }
                break;
            case ActionState.Attack:
                if (attackTimer <= 0)
                {
                    state = ActionState.None;
                    attackTimer = AttackDuration;
                    WeaponCollision.StopUseWeapon();

                }
                break;
            /*case ActionState.AttackCooldown:  /// For combo
                if (attackTimer <= 0)
                {
                    if (attackTriggered)
                    {
                        comboState++;
                        if (comboState >= 4)
                        {
                            comboState = 1;
                        }
                        attackTriggered = false;
                        state = ActionState.Attack;
                        attackTimer = AttackCooldown;
                        WeaponCollision.gameObject.SetActive(true);
                        if (Animator != null)
                            Animator.SetInteger("ComboState", comboState);
                    }
                    else
                    {
                        state = ActionState.None;
                        comboState = 0;
                    }
                }
                break;*/
            case ActionState.Parry:
                if (!shieldTriggered)
                {
                    state = ActionState.None;
                }
                break;
        }

    }

    public override void OnDamaged(WeaponController weapon)
    {
        if (state == ActionState.Parry)
        {
            Vector3 attackerDir = weapon.Owner.transform.position - transform.position;
            float angleFromShield = Vector2.Angle(new Vector2(attackerDir.x, attackerDir.z), mouseManager.MouseDir);

            Debug.DrawRay(transform.position, attackerDir * 10, Color.red, 1);
            
            if (angleFromShield < shieldSize / 2) //Hit the shield
                return;
        }
        if (base.Health > 0)
        {
            launchRandomSound("ig player get hit");
        }
        base.OnDamaged(weapon);
    }

    public override void OnDeath()
    {
        AudioManager.instance.Play("ig player die");
        GameManager.OnPlayerDie();
    }

    private void Move(Vector2 targetSpeed)
    {
        rigidBody.velocity = new Vector3(targetSpeed.x, 0, targetSpeed.y);
    }

    private void launchRandomSound(string nomDuSon)
    {
        AudioManager.instance.Play(nomDuSon);
    }

    #region GETTER/SETTER

    protected override float Armor { get => equipment.Armor; }
    public override float Damage { get => equipment.SwordDamage; }
    public EquipmentInfo Equipment { get => equipment; set => equipment = value; }

    #endregion
}