using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        Debug.Log("Planet took " + damage + " damage");
    }
}
