using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Game.Runtime.Core;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("虚拟摄像机")] public CinemachineVirtualCamera vcam;
    [Header("Monitor camera")] public Camera monitorCam;
    private CinemachineConfiner2D confiner;

    protected override void Awake()
    {
        base.Awake();
        if (!vcam) vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        if (monitorCam)
        {
            DontDestroyOnLoad(monitorCam.gameObject);
        }
        confiner = vcam?.GetComponent<CinemachineConfiner2D>();
    }

    public void SetConfiner(PolygonCollider2D bounds)
    {
        if (!confiner) return;
        confiner.m_BoundingShape2D = bounds;
        confiner.InvalidateCache();
    }

    public void ClearConfiner(PolygonCollider2D bounds)
    {
        if (confiner && confiner.m_BoundingShape2D == bounds)
        {
            confiner.m_BoundingShape2D = null;
            confiner.InvalidateCache();
        }
    }
}