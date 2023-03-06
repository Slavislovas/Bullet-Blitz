using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public int Health;
    public int Damage;
    public float AttackInterval; // 1 attack per AttackInterval seconds
    public float MovementSpeed;
    public float HealthRegen;
    public float LifeSteal;
}
