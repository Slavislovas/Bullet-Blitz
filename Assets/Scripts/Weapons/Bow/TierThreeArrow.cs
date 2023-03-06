using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierThreeArrow : MonoBehaviour
{
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
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.GetComponentInParent<Zombie>().Health -= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Damage;
            Debug.Log(other.GetComponentInParent<Zombie>().Health);
            Destroy(this.gameObject);
        }
    }
}
