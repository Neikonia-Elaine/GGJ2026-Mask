using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class CameraBounds : MonoBehaviour
{
    private PolygonCollider2D Collider { get; set; }

    private void Awake()
    {
        Collider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        CameraManager.Instance?.SetConfiner(Collider);
    }

    private void OnDisable()
    {
        CameraManager.Instance?.ClearConfiner(Collider);
    }
}