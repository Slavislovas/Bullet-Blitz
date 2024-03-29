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
    int requiredXpForLevelUp = 20;
    UnityEngine.Object[] availableItems;
    LevelUpMenu levelUpMenu;
    WeaponUpgradeMenu weaponUpgradeMenu;
    GameOverMenu gameOverMenu;
    public bool recentlyHit;
    public GameObject instantiatedWeapon;
    private GameObject UICanvas;
    private UI uiMenu;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        instantiatedWeapon = Instantiate(weapon);
        instantiatedWeapon.transform.SetParent(rb.transform, false);
        invicible = false;
        availableItems = Resources.LoadAll("Items");
        levelUpMenu = GameObject.FindGameObjectWithTag("LevelUpMenu").GetComponent<LevelUpMenu>();
        gameOverMenu = GameObject.FindGameObjectWithTag("GameOverMenuCanvas").GetComponent<GameOverMenu>();
        weaponUpgradeMenu = GameObject.FindGameObjectWithTag("WeaponUpgradeMenu").GetComponent<WeaponUpgradeMenu>();
        weaponUpgradeMenu.player = this;
        UICanvas = GameObject.FindGameObjectWithTag("UICanvas");
        uiMenu = UICanvas.GetComponent<UI>();
        uiMenu.SetMaxHealth(MaxHealth);
        uiMenu.SetMaxXP(requiredXpForLevelUp);
        uiMenu.SetLevel(level);
        StartCoroutine(RegenerateHealth());
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
            Health -= damage;
            uiMenu.SetHealth(Health);
            if (Health <= 0)
            {
                gameOverMenu.PauseGame();
            }
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
        yield return new WaitForSeconds(5);
        recentlyHit = false;
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitUntil(() => !recentlyHit);
            if (Health + HealthRegen > MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health += HealthRegen;
            }
            uiMenu.SetHealth(Health);
            yield return new WaitForSeconds(1);
        }
    }

    internal void AddExperience(int xpReward)
    {
        experience += xpReward;
        uiMenu.SetXP(experience);
        if (experience >= requiredXpForLevelUp && availableItems.Length > 0)
        {
            experience = 0;
            requiredXpForLevelUp *= 1;
            level++;

            uiMenu.SetMaxXP(requiredXpForLevelUp);
            uiMenu.SetXP(experience);
            uiMenu.SetLevel(level);

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

        if (Health + item.HealthFlatIncrease > MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health += item.HealthFlatIncrease;
        }

        if (item.AttackIntervalIncrease != 0)
        {
            AttackInterval = AttackInterval * (1 + item.AttackIntervalIncrease);
        }

        if (item.AttackIntervalDecrease != 0)
        {
            AttackInterval = AttackInterval * (1 - item.AttackIntervalDecrease);
        }

        Damage += item.DamageIncrease;
        Damage -= item.DamageDecrease;

        if (item.MovementSpeedPercentileIncrease != 0)
        {
            MovementSpeed = MovementSpeed * (1 + item.MovementSpeedPercentileIncrease);
        }

        if (item.MovementSpeedPercentileDecrease != 0)
        {
            MovementSpeed = MovementSpeed * (1 - item.MovementSpeedPercentileDecrease);
        }

        HealthRegen += item.HealthRegenIncrease;
        HealthRegen -= item.HealthRegenDecrease;

        LifeSteal += item.LifeStealIncrease;
        LifeSteal -= item.LifeStealDecrease;

        uiMenu.SetMaxHealth(MaxHealth);
        uiMenu.SetHealth(Health);
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
