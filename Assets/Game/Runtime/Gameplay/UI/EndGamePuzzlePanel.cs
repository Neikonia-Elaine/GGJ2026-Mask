using System.Collections;
using System.Collections.Generic;
using Game.Runtime.Core;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EndGamePuzzlePanel : UIPanel, IPuzzlePanel
{
    [SerializeField] private float exitDelaySeconds = 5f;
    public Button closeButton;
    public List<PuzzlePiece> pieces;
    public float spawnRange = 300f;

    private int placedCount;

    public override void OnInit()
    {
        base.OnInit();
        closeButton?.onClick.AddListener(() => UIManager.Instance.Close<EndGamePuzzlePanel>());
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
            if (piece == null) continue;

            piece.panel = this;
            piece.correctPosition = piece.rect.anchoredPosition;
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
            StartCoroutine(ExitSceneLater());
        }
    }

    private IEnumerator ExitSceneLater()
    {
        yield return new WaitForSeconds(exitDelaySeconds);
        UIManager.Instance.Close<EndGamePuzzlePanel>();
    }
}
