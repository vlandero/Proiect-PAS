using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private int speed;

    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
    }
}
