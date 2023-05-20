using System;
using System.Collections;
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
    bool invicible;
    public int experience = 0;
    int level = 1;
    int requiredXpForLevelUp = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var instantiatedWeapon = Instantiate(weapon);
        instantiatedWeapon.transform.SetParent(rb.transform, false);
        invicible = false;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && !invicible)
        {
            int damage = collision.gameObject.GetComponent<Enemy>().Damage;
            this.Health -= damage;
            StartCoroutine(InvFrames());
        }
    }

    private IEnumerator InvFrames()
    {
        invicible = true;
        yield return new WaitForSeconds(1);
        invicible = false;

    }

    internal void AddExperience(int xpReward)
    {
        this.experience += xpReward;
        if (experience >= requiredXpForLevelUp)
        {
            experience = 0;
            requiredXpForLevelUp *= 2;
            level++;
            Debug.Log("LEVEL UP! Current level: " + level);
        }
    }

    internal void ApplyItemStats(Item item)
    {
        MaxHealth += item.MaxHpFlatIncrease;
        MaxHealth -= item.MaxHpFlatDecrease;

        if (item.MaxHpPercentileIncrease != 0)
        {
            MaxHealth = (int) Math.Ceiling(MaxHealth * (1 + item.MaxHpPercentileIncrease));
        }

        if (item.MaxHpPercentileDecrease != 0)
        {
            MaxHealth = (int) Math.Ceiling(MaxHealth * (1 - item.MaxHpPercentileIncrease));
        }

        if (Health + item.HealthFlatIncrease >= MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health += item.HealthFlatIncrease;
        }

        AttackInterval += item.AttackIntervalIncrease;
        AttackInterval -= item.AttackIntervalDecrease;

        Damage += item.DamageIncrease;
        Damage -= item.DamageDecrease;

        if (item.MovementSpeedPercentileIncrease != 0)
        {
            MovementSpeed = (int)Math.Ceiling(MovementSpeed * (1 + item.MovementSpeedPercentileIncrease));
        }

        if (item.MovementSpeedPercentileDecrease != 0)
        {
            MovementSpeed = (int)Math.Ceiling(MovementSpeed * (1 - item.MovementSpeedPercentileDecrease));
        }

        HealthRegen += item.HealthRegenIncrease;
        HealthRegen -= item.HealthRegenDecrease;

        LifeSteal += item.LifeStealIncrease;
        LifeSteal -= item.LifeStealDecrease;
    }

    internal void UpgradeWeapon()
    {
       Bow bow = weapon.GetComponent<Bow>();
        switch (bow.weaponTier)
        {
            case WeaponTierEnum.ONE:
                bow.weaponTier = WeaponTierEnum.TWO;
                break;
            case WeaponTierEnum.TWO:
                bow.weaponTier = WeaponTierEnum.THREE;
                break;
            case WeaponTierEnum.THREE:
                    bow.weaponTier = WeaponTierEnum.FOUR;
                break;
        }
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
