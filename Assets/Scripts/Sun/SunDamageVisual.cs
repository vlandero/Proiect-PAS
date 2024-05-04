using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDamageVisual : MonoBehaviour
{
    [SerializeField] Material material;
    float pixelateSpeed = 15f;

    private float pixelateAmount;
    private float pixelateAmountTarget;

    private void Start()
    {
        pixelateAmountTarget = 0f;
        pixelateAmount = pixelateAmountTarget;
    }

    private void Update()
    {
        pixelateAmount = Mathf.Lerp(pixelateAmount, pixelateAmountTarget, Time.deltaTime * pixelateSpeed);

        material.SetFloat("_PixelatedVal", pixelateAmount);
    }

    public void CalculatePixelatedBasedOnHp(int hp, int maxHp)
    {
        pixelateAmountTarget = 1 - (float)hp / maxHp;
    }
}
