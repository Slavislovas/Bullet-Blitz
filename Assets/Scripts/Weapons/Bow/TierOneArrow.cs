using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierOneArrow : MonoBehaviour
{
    public Rigidbody2D rb;
    int damage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CalculateRotation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.GetComponentInParent<Enemy>().Health -= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Damage;
        }

        if (!other.gameObject.tag.Equals("Player") && !other.gameObject.tag.Equals("PlayerProjectile"))
        {
            Destroy(this.gameObject);
        }
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
