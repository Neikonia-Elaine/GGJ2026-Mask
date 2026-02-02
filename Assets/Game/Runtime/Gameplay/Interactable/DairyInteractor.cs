using System.Collections;
using System.Collections.Generic;
using Game.Runtime.Core;
using UnityEngine;

public class DairyInteractor : Interactable
{
    public override void Interact()
    {
        base.Interact();
        UIManager.Instance.Open<DairyPanel>();
    }
}
