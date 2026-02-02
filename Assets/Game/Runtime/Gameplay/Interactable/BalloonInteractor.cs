using System.Collections;
using System.Collections.Generic;
using Game.Runtime.Core;
using UnityEngine;

public class BalloonInteractor : Interactable
{
    public override void Interact()
    {
        base.Interact();
        EventHandler.CallAnomalyCompletedEvent("Balloon");
        EventHandler.CallFragmentCollectedEvent("fragment_balloon");
    }
}