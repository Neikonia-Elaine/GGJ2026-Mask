using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (GameManager.Instance.IsGameplay) GameManager.Instance.player.transform.position = transform.position;
    }
}