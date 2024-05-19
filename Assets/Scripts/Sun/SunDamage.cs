using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDamage : MonoBehaviour
{
    private SunDamageVisual sunDamageVisual;
    private int hp;
    private int maxHp;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sunDamageSound;

    private void Start()
    {
        sunDamageVisual = GetComponent<SunDamageVisual>();
        hp = maxHp = LevelBalanceManager.Instance.sunHp;
        CanvasManager.instance.mainGui.sunHp.UpdateSunHp(hp);
    }

    public void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(sunDamageSound);
        hp = Mathf.Max(0, hp - damage);
        CanvasManager.instance.mainGui.sunHp.UpdateSunHp(hp);
        sunDamageVisual.CalculatePixelatedBasedOnHp(hp, maxHp);
        if (hp == 0)
        {
            CanvasManager.instance.ShowGameOverLoss();
        }
    }


}
