using System.Collections;
using System.Collections.Generic;
using Game.Runtime.Core;
using UnityEngine;

public class FoodTruckInteractor : Interactable
{
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.OpenFoodTruckScene();
    }
}