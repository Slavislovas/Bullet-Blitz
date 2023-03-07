using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : BaseCharacter
{
    Transform target;
    Vector2 moveDirection;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovementDirection();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CalculateMovementDirection()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        moveDirection = direction;
    }

    private void FollowPlayer()
    {
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * MovementSpeed;
    }
}
