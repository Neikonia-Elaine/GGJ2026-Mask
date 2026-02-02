using System.Collections;
using System.Collections.Generic;
using Game.Runtime.Core;
using UnityEngine;

public class DairyInteractor : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();
        UIManager.Instance.Open<DairyPanel>();
        
    }
}
