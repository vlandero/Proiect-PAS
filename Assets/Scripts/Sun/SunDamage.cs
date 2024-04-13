using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDamage : MonoBehaviour
{
    private SunDamageVisual sunDamageVisual;
    private int hp;
    private int maxHp;

    private void Start()
    {
        sunDamageVisual = GetComponent<SunDamageVisual>();
        hp = maxHp = LevelBalanceManager.Instance.sunHp;
    }

    public void TakeDamage(int damage)
    {
        hp = Mathf.Max(0, hp - damage);
        sunDamageVisual.CalculatePixelatedBasedOnHp(hp, maxHp);
        if (hp == 0)
        {
            Debug.Log("Game Over!");
        }
    }


}
