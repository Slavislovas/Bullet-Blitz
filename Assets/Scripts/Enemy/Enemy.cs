using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseCharacter
{
    Transform target;
    NavMeshAgent agent;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("setDestination", 0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorParameters();
    }

    private void FixedUpdate()
    {
        
    }

    private void SetAnimatorParameters()
    {
        animator.SetFloat("Horizontal", agent.velocity.x);
        animator.SetFloat("Vertical", agent.velocity.y);
        animator.SetFloat("Magnitude", agent.velocity.magnitude);
    }

    private void setDestination()
    {
        agent.SetDestination(target.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}