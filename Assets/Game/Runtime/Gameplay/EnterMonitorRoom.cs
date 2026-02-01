using System.Collections;
using System.Collections.Generic;
using Game.Runtime.Core;
using UnityEngine;

public class EnterMonitorRoom : Interactable
{
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.OpenMonitorRoomScene();
    }
}