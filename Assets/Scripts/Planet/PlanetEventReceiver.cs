using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEventReceiver : MonoBehaviour
{
    public void PlanetHit()
    {
        Debug.Log("Planet hit! " + name);
    }
}
