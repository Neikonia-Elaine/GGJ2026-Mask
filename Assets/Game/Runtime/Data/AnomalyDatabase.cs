using System.Collections.Generic;
using UnityEngine;
using Game.Runtime.Data;

[CreateAssetMenu(fileName = "AnomalyDatabase", menuName = "Anomaly/Database")]
public class AnomalyDatabase : ScriptableObject
{
    public List<AnomalySO> anomalies = new();

    public AnomalySO Find(AnomalyName name)
    {
        for (int i = 0; i < anomalies.Count; i++)
            if (anomalies[i] != null && anomalies[i].anomalyName == name)
                return anomalies[i];
        return null;
    }
}
