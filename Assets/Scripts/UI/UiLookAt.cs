using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLookAt : MonoBehaviour
{
    private RectTransform canvas;
    private void Start()
    {
        canvas = GetComponent<RectTransform>();
    }
    private void LateUpdate()
    {
        transform.LookAt(canvas.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
