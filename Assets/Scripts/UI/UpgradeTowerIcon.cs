using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerIcon : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite lockSprite;
    [SerializeField] private Sprite upgradeSprite;

    public void SetLock()
    {
        image.sprite = lockSprite;
    }

    public void SetUpgrade()
    {
        image.sprite = upgradeSprite;
    }
}
