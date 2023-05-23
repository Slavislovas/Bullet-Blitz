using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string description;

    public int MaxHpFlatIncrease;
    public int MaxHpFlatDecrease;

    public int HealthFlatIncrease;
    public int HealthFlatDecrease;

    public float MaxHpPercentileIncrease;
    public float MaxHpPercentileDecrease;

    public float AttackIntervalIncrease;
    public float AttackIntervalDecrease;

    public int DamageIncrease;
    public int DamageDecrease;

    public float MovementSpeedPercentileIncrease;
    public float MovementSpeedPercentileDecrease;

    public int HealthRegenIncrease;
    public int HealthRegenDecrease;

    public float LifeStealIncrease;
    public float LifeStealDecrease;
}
