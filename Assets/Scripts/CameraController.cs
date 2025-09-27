using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;

    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector3(target.position.x * offset.x, target.position.y * offset.y, -10f);
    }
}