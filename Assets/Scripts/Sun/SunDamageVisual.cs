using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDamageVisual : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float pixelateSpeed = .7f;

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

        if (Input.GetKeyDown(KeyCode.L))
        {
            pixelateAmountTarget = .7f;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            pixelateAmountTarget = 0f;
        }

        material.SetFloat("_PixelatedVal", pixelateAmount);
    }
}
