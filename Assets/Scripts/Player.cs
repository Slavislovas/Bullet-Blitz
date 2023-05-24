using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : BaseCharacter
{
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    [SerializeField]
    public GameObject weapon;
    [SerializeField]
    Animator animator;
    bool invicible;
    public int experience = 0;
    int level = 1;
    int requiredXpForLevelUp = 10;
    UnityEngine.Object[] availableItems;
    LevelUpMenu levelUpMenu;
    WeaponUpgradeMenu weaponUpgradeMenu;
    bool healthRegenActivated;
    bool recentlyHit;
    public GameObject instantiatedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        instantiatedWeapon = Instantiate(weapon);
        instantiatedWeapon.transform.SetParent(rb.transform, false);
        invicible = false;
        availableItems = Resources.LoadAll("Items");
        levelUpMenu = GameObject.FindGameObjectWithTag("LevelUpMenu").GetComponent<LevelUpMenu>();
        weaponUpgradeMenu = GameObject.FindGameObjectWithTag("WeaponUpgradeMenu").GetComponent<WeaponUpgradeMenu>();
        healthRegenActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovementDirection();
        SetAnimatorParameters();
        HealthRegeneration();
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
            recentlyHit = true;
            StartCoroutine(InvFrames());
            StartCoroutine(ResetRecentlyHit());
        }

        if (collision.gameObject.tag.Equals("WeaponUpgrade"))
        {
            weaponUpgradeMenu.PauseGame();
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator InvFrames()
    {
        invicible = true;
        yield return new WaitForSeconds(1);
        invicible = false;

    }

    private IEnumerator ResetRecentlyHit()
    {
        yield return new WaitForSeconds(3);
        recentlyHit = false;
    }

    private void HealthRegeneration()
    {
        if (!recentlyHit && Health < MaxHealth)
        {
            StartCoroutine(AddHealth());
        }
    }

    private IEnumerator AddHealth()
    {
        yield return new WaitForSeconds(1);
        if (Health + HealthRegen > MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health += HealthRegen;
        }
    }

    internal void AddExperience(int xpReward)
    {
        this.experience += xpReward;
        if (experience >= requiredXpForLevelUp && availableItems.Length > 0)
        {
            experience = 0;
            requiredXpForLevelUp *= 1;
            level++;

            if (availableItems.Length == 1)
            {
                levelUpMenu.PauseGame((GameObject)availableItems[0],
                (GameObject)availableItems[0]);
                return;
            }

            var itemOneIndex = UnityEngine.Random.Range(0, availableItems.Length);
            var tempItemList = availableItems.Where(x => x != availableItems[itemOneIndex]).ToArray();
            var itemTwoIndex = UnityEngine.Random.Range(0, tempItemList.Length);
            levelUpMenu.PauseGame((GameObject) availableItems[itemOneIndex],
                (GameObject) tempItemList[itemTwoIndex]);

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

    public void RemoveItemFromList(GameObject item)
    {
        UnityEngine.Object itemObject = item;
        availableItems = availableItems.Where(x => x != itemObject).ToArray();
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
