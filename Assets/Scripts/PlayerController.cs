using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public Animator Animator;
    public WeaponController WeaponCollision;

    private Rigidbody rigidBody;
    private Vector2 velocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;
    private bool facingRight = true;
    private bool attack = false;

    private static Plane planeXZ;

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

        attack = Input.GetMouseButton(0);

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float dist;
        if (planeXZ.Raycast(mouseRay, out dist))
        {
            transform.LookAt(mouseRay.GetPoint(dist));
        }
    }

    private void FixedUpdate()
    {
        WeaponCollision.gameObject.SetActive(attack);
        
        Move(targetVelocity * Speed * Time.fixedDeltaTime);
    }

    private void Move(Vector2 targetSpeed)
    {
        rigidBody.velocity = new Vector3(targetSpeed.x, 0, targetSpeed.y);

        /*if (targetSpeed.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (targetSpeed.x < 0 && facingRight)
        {
            Flip();
        }*/
    }


    /*private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }*/
}
