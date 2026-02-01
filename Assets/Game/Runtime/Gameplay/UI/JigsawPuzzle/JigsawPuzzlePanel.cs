using System;
using System.Collections;
using System.Collections.Generic;
using Game.Runtime.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class JigsawPuzzlePanel : UIPanel
{
    [SerializeField] private float exitDelaySeconds = 5f;
    public Button closeButton;
    public List<PuzzlePiece> pieces;
    public float spawnRange = 300f;
    private int placedCount;

    public override void OnInit()
    {
        base.OnInit();
        closeButton?.GetComponent<Button>()?.onClick.AddListener(() => UIManager.Instance.Close<JigsawPuzzlePanel>());
    }

    public override void OnOpen(object data = null)
    {
        placedCount = 0;
        ScatterPieces();
    }

    private void ScatterPieces()
    {
        foreach (var piece in pieces)
        {
            piece.panel = this;
            piece.rect.anchoredPosition = Random.insideUnitCircle * spawnRange;
            piece.isPlaced = false;
            piece.canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnPiecePlaced(PuzzlePiece piece)
    {
        placedCount++;
        if (placedCount == pieces.Count)
        {
            Debug.Log("Puzzle Completed!");
            Game.Runtime.Core.EventHandler.CallFragmentCollectedEvent("JigsawPuzzle");
            Game.Runtime.Core.EventHandler.CallAnomalyCompletedEvent("JigsawPuzzle");
            StartCoroutine(ExitSceneLater());
        }
    }

    private IEnumerator ExitSceneLater()
    {
        yield return new WaitForSeconds(exitDelaySeconds);

        // 如果 ExitFoodTruckScene 内部会 LoadScene，Close 不写也行
        UIManager.Instance.Close<JigsawPuzzlePanel>();
        GameManager.Instance.ExitFoodTruckScene();
    }
}