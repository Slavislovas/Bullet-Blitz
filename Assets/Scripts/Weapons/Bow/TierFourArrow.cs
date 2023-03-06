using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierFourArrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject childArrow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HIT");
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.GetComponentInParent<Zombie>().Health -= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Damage;
            Debug.Log(other.GetComponentInParent<Zombie>().Health);
            Vector3 bounds = other.bounds.max;
            ExplodeOnHit(bounds);
        }
    }

    private void ExplodeOnHit(Vector3 bounds)
    {
        Vector2 velocity = rb.velocity;
        velocity.x = 0;
        velocity.y = 1;
        Instantiate(childArrow, bounds, transform.rotation).GetComponent<Rigidbody2D>().velocity = velocity.normalized * 30;
        velocity.x = 1;
        velocity.y = 0;
        Instantiate(childArrow, bounds, transform.rotation).GetComponent<Rigidbody2D>().velocity = velocity.normalized * 30;
        velocity.x = -1;
        velocity.y = 0;
        Instantiate(childArrow, bounds, transform.rotation).GetComponent<Rigidbody2D>().velocity = velocity.normalized * 30;
        velocity.x = 0;
        velocity.y = -1;
        Instantiate(childArrow, bounds, transform.rotation).GetComponent<Rigidbody2D>().velocity = velocity.normalized * 30;
        Destroy(this.gameObject);
    }
}
