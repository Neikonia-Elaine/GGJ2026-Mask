using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMachineInteractor : Interactable
{
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.StartClawMachineGame();
        // TODO 可以改成玩家走到抓娃娃机旁边再切换场景
    }
}