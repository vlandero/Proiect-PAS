using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private SunDamage sunDamage;

    public void TakeDamage(int damage)
    {
        sunDamage.TakeDamage(damage);
    }
}
