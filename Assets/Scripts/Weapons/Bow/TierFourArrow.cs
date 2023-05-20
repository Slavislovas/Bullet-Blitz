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
        Debug.Log(rb.velocity.x + "   " + rb.velocity.y);
        CalculateRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.GetComponentInParent<Enemy>().Health -= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Damage;
            ExplodeOnHit(other);        
        }

        //Destroy when hitting collidable terrain, environment objects etc.sdd
        if (!other.gameObject.tag.Equals("Player") && !other.gameObject.tag.Equals("PlayerProjectile"))
        {
            Destroy(this.gameObject);
        }
    }

    private void ExplodeOnHit(Collider2D collider)
    {
        Vector3 colliderCenter = collider.bounds.center;
        Vector3 colliderSize = collider.bounds.size;
        Vector2 velocity = rb.velocity;
        velocity.x = 0;
        velocity.y = 1;
        Instantiate(childArrow, new Vector3(colliderCenter.x, (colliderCenter.y + (colliderSize.y / 2) + 0.5f)), transform.rotation).GetComponent<Rigidbody2D>().velocity = velocity.normalized * 30;
        velocity.x = 1;
        velocity.y = 0;
        Instantiate(childArrow, new Vector3((colliderCenter.x + (colliderSize.x / 2) + 0.5f), colliderCenter.y), transform.rotation).GetComponent<Rigidbody2D>().velocity = velocity.normalized * 30;
        velocity.x = -1;
        velocity.y = 0;
        Instantiate(childArrow, new Vector3((colliderCenter.x - (colliderSize.x / 2) - 0.5f), colliderCenter.y), transform.rotation).GetComponent<Rigidbody2D>().velocity = velocity.normalized * 30;
        velocity.x = 0;
        velocity.y = -1;
        Instantiate(childArrow, new Vector3(colliderCenter.x, (colliderCenter.y - (colliderSize.y / 2) - 0.5f)), transform.rotation).GetComponent<Rigidbody2D>().velocity = velocity.normalized * 30;
    }

    private void CalculateRotation()
    {
        //Vertical or horizontal arrows
        if (rb.velocity.x == 0 && rb.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        if (rb.velocity.x < 0 && rb.velocity.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (rb.velocity.x == 0 && rb.velocity.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        //Diagonal arrows
        if (rb.velocity.x < 0 && rb.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 135);
        }

        if (rb.velocity.x < 0 && rb.velocity.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 225);
        }

        if (rb.velocity.x > 0 && rb.velocity.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 315);
        }

        if (rb.velocity.x > 0 && rb.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
    }
}
