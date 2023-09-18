using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int health;
    public int Damage => damage;
    public int Health => health;
}
