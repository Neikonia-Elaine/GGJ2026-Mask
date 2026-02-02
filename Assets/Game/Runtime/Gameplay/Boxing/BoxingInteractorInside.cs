using System.Collections;
using Game.Runtime.Core;
using UnityEngine;

public class BoxingInteractorInside : Interactable
{
    public override void Interact()
    {
        Debug.Log("BoxingInteractorInside Interact");
        base.Interact();
        EventHandler.CallAnomalyCompletedEvent("Boxing");
        EventHandler.CallFragmentCollectedEvent("fragment_boxing");

        StartCoroutine(ExitAfterSeconds(4f));
    }

    private IEnumerator ExitAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameManager.Instance.ExitBoxingScene();
    }
}
