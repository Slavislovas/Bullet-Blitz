using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    float shootInputHorizontal;
    float shootInputVertical;
    float shootElapsedTime;
    Player player;
    
    public WeaponTierEnum weaponTier;
    public GameObject tierOneArrow;
    public GameObject tierTwoArrow;
    public GameObject tierThreeArrow;
    public GameObject tierFourArrow;
    public int arrowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateShootDirection();
    }

    private void FixedUpdate()
    {
        if (Time.time > shootElapsedTime)
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        if (shootInputHorizontal != 0 || shootInputVertical != 0)
        {
            switch (weaponTier)
            {
                case WeaponTierEnum.ONE:
                    Instantiate(tierOneArrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(shootInputHorizontal, shootInputVertical).normalized * arrowSpeed;
                    break;
                case WeaponTierEnum.TWO:
                    Instantiate(tierTwoArrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(shootInputHorizontal, shootInputVertical).normalized * arrowSpeed;
                    Instantiate(tierTwoArrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(shootInputHorizontal, shootInputVertical).normalized * arrowSpeed;
                    break;
                case WeaponTierEnum.THREE:
                    TierThreeBowFuncionality(tierThreeArrow);
                    break;
                case WeaponTierEnum.FOUR:
                    TierFourBowFuncionality(tierFourArrow);
                    break;            
            }
            shootElapsedTime = Time.time + player.AttackInterval;
        }
    }

    private void TierThreeBowFuncionality(GameObject arrow)
    {
        if (shootInputHorizontal == 1 && shootInputVertical == 1)
        {
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0).normalized * arrowSpeed;
        }
        else if (shootInputHorizontal == -1 && shootInputVertical == 1)
        {
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0).normalized * arrowSpeed;
        }
        else if (shootInputHorizontal == -1 && shootInputVertical == -1)
        {
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0).normalized * arrowSpeed;
        }
        else if (shootInputHorizontal == 1 && shootInputVertical == -1)
        {
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(1, -1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0).normalized * arrowSpeed;
        }
        else if (shootInputHorizontal == 1)
        {
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(1, -1).normalized * arrowSpeed;
        }
        else if (shootInputHorizontal == -1)
        {
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1).normalized * arrowSpeed;
        }
        else if (shootInputVertical == 1)
        {
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1).normalized * arrowSpeed;
        }
        else if (shootInputVertical == -1)
        {
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1).normalized * arrowSpeed;
            Instantiate(arrow, transform.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(1, -1).normalized * arrowSpeed;
        }
    }

    private void TierFourBowFuncionality(GameObject arrow)
    {
        TierThreeBowFuncionality(arrow);
    }

    private void CalculateShootDirection()
    {
        shootInputHorizontal = 0f;
        shootInputVertical = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            shootInputHorizontal = -1f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            shootInputHorizontal = 1f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            shootInputVertical = 1f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            shootInputVertical = -1f;
        }
    }
}
