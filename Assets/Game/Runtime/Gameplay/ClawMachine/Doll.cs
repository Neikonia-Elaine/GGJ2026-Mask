using System;
using System.Collections;
using UnityEngine;
using Game.Runtime.Core;

public class Doll : MonoBehaviour
{
    public int dollId;

    private SpriteRenderer sr;
    private Collider2D col;
    private Transform oldParent;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        oldParent = transform.parent;
    }

    public void OnGrabbed()
    {
        if (sr) sr.sortingOrder = 5;
        if (col) col.enabled = false;
    }

    public void OnForceDropped()
    {
        transform.SetParent(oldParent);
        if (col) col.enabled = true;
    }

    public void PlayDrop(float exitY)
    {
        StartCoroutine(Drop(exitY));
    }

    private IEnumerator Drop(float exitY)
    {
        while (transform.position.y > exitY)
        {
            transform.position += Vector3.down * (4 * Time.deltaTime);
            yield return null;
        }
    }

    public void OnReleasedAtExit()
    {
        // 成功奖励逻辑
        Game.Runtime.Core.EventHandler.CallItemCollectedEvent("Doll");
        Destroy(gameObject);
    }
}