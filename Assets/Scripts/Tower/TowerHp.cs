using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHp : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    public Canvas hpCanvas;

    private void Start()
    {
        UpdateHp(1);
    }
    public void UpdateHp(float ratio)
    {
        hpBar.fillAmount = ratio;
    }
}
