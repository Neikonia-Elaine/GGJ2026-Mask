using UnityEngine;
using Game.Runtime.Core;
using Game.Runtime.Data;

public class AnomalyCompletedListener : MonoBehaviour
{
    [SerializeField] private AnomalyDatabase db;

    private void OnEnable()
    {
        EventHandler.AnomalyCompletedEvent += OnAnomalyCompleted;
    }

    private void OnDisable()
    {
        EventHandler.AnomalyCompletedEvent -= OnAnomalyCompleted;
    }

    private void OnAnomalyCompleted(string anomalyKey)
    {
        if (db == null) return;

        if (!System.Enum.TryParse(anomalyKey, out AnomalyName name)) return;

        var so = db.Find(name);
        if (so == null) return;

        if (!string.IsNullOrEmpty(so.completionHintText))
            UIManager.Instance.Open<FragmentCompletedTextPanel>(so.completionHintText);
    }
}
