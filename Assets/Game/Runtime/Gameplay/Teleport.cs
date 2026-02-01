using System;
using System.Collections;
using System.Collections.Generic;
using Game.Runtime.Core;
using Game.Runtime.Core.Attributes;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SceneName] public string sceneTo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) TeleportToScene();
    }

    public void TeleportToScene()
    {
        TransitionManager.Instance.TransitionTo(sceneTo);
    }
}