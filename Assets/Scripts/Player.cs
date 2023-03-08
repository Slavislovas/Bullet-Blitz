using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    [SerializeField]
    GameObject weapon;
    [SerializeField]
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var instantiatedWeapon = Instantiate(weapon);
        instantiatedWeapon.transform.SetParent(rb.transform, false);
        var camera = GameObject.FindWithTag("MainCamera");
        camera.transform.SetParent(rb.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovementDirection();
        SetAnimatorParameters();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * MovementSpeed, vertical * MovementSpeed);
    }

    private void SetAnimatorParameters()
    {
        animator.SetFloat("Horizontal", rb.velocity.x);
        animator.SetFloat("Vertical", rb.velocity.y);
        animator.SetFloat("Magnitude", rb.velocity.magnitude);
    }

    private void CalculateMovementDirection()
    {
        horizontal = 0;
        vertical = 0;

        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            vertical = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1f;
        }
    }
}
